using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Ball : MonoBehaviour
{
    GameManager gameManager;

    AudioSource audioSource;
    public AudioClip throwSound;

    HealthSystem healthSystem;

    Rigidbody Rbody;

    public LayerMask Targetlayer;


    [HideInInspector]public float Ballspeed;
    bool stick,throwed;
    
    private void Start() {
        Rbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        gameManager = FindObjectOfType<GameManager>();
        healthSystem = FindObjectOfType<HealthSystem>();
    }

    private void Update() {
        if(gameManager.start && !gameManager.gameover && !gameManager.pause)
        {
            #if UNITY_ANDROID && !UNITY_EDITOR
            if(Input.GetTouch(0).phase == TouchPhase.Began)
            {
                if(!throwed && !stick)
                {
                    LaunchThrow();
                }
                else if(stick)
                {
                    checkInjured();
                }
            }
            #endif

            #if UNITY_STANDALONE || UNITY_EDITOR
            if(Input.GetMouseButtonDown(0))
            {
                if(!throwed && !stick)
                {
                    LaunchThrow();
                }
                else if(stick)
                {
                    checkInjured();
                }
            }
            #endif
        }
    }

    void checkInjured()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(camRay,out hit,100f,Targetlayer))
        {
            if(hit.transform.root.GetComponent<Man>())
            {
                if(hit.transform.root.GetComponent<Man>().injured)
                {
                    Destroy(hit.transform.root.GetComponent<Man>());

                    gameManager.SpawnBall();

                    Destroy(this);
                }
            }
        }
    }

    void LaunchThrow()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(camRay,out hit,100f,Targetlayer))
        {
            if(hit.transform.root.GetComponent<Man>() || hit.transform.root.GetComponent<Woman>())
            {
                Vector3 Vo = CalculateVelocity(hit.point , transform.position , Ballspeed);

                transform.rotation = Quaternion.LookRotation(Vo);

                Rbody.velocity = Vo;

                throwed = true;

                audioSource.PlayOneShot(throwSound);
            }
        }
    }

    Vector3 CalculateVelocity(Vector3 target , Vector3 origin, float time)
    {
        //define the distance x and y first
        Vector3 distance = target - origin;
        Vector3 distance_x_z = distance;
        distance_x_z.Normalize();
        distance_x_z.y = 0;

        //creating a float that represents our distance 
        float sy = distance.y;
        float sxz = distance.magnitude;

        //calculating initial x velocity
        //Vx = x / t
        float Vxz = sxz / time;

        ////calculating initial y velocity
        //Vy0 = y/t + 1/2 * g * t
        float Vy = sy / time + 0.5f * Mathf.Abs(Physics.gravity.y) * time;

        Vector3 result = distance_x_z * Vxz;
        result.y = Vy;

        return result;
    }

    private void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Human") && !stick)
        {
            gameObject.tag = "Untagged";
            transform.parent = other.transform;

            stick = true;
            
            Destroy(other.transform.root.gameObject.GetComponent<BoxCollider>());
            Destroy(other.transform.root.gameObject.GetComponent<Animator>());
            Destroy(Rbody);

            if(other.transform.root.gameObject.tag == "Man")
            {
                other.transform.root.GetComponent<Man>().injury();
            }
            else
            {
                other.transform.root.GetComponent<Woman>().injury();
            }
        }
        if(other.gameObject.layer == LayerMask.NameToLayer("Default") && !stick)
        {
            gameObject.tag = "Untagged";

            stick = true;
            Destroy(Rbody);

            Camera.main.GetComponent<DOTweenAnimation>().DORestart();
            healthSystem.Decrease();

            if(healthSystem.Health > 0)
            {
                gameManager.SpawnBall();
            }

            Destroy(this.gameObject);
        }
    }
}
