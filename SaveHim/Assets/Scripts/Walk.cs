using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walk : MonoBehaviour
{
    public bool walk;
    RandomHuman randomHuman;
    Vector3 target;
    Animator animator;

    private void Start() {
        if(!walk)
        {
            Destroy(this);
        }
        target = transform.position;
        randomHuman = FindObjectOfType<RandomHuman>();
        animator = GetComponent<Animator>();
        InvokeRepeating("RandomWalk",Random.Range(0,10f),Random.Range(10f,20f));
    }

    private void Update() {
        if(Vector3.Distance(transform.position , target) > 0.5f)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, 0.5f * Time.deltaTime);
            animator.SetBool("walk",true);
        }
        else
        {
            target = transform.position;
            animator.SetBool("walk",false);
        }
    }

    void RandomWalk()
    {
        Vector3 min = randomHuman.m_Min;
        Vector3 max = randomHuman.m_Max;

        float randomX = Random.Range(min.x + 1f , max.x - 1f);
        float randomZ = Random.Range(min.z + 1f , max.z - 1f);

        target = new Vector3(randomX , transform.position.y , randomZ);

        transform.LookAt(target);
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.transform.root.tag == "Man" || other.transform.root.tag == "Woman")
        {   
            if(other.transform.root.gameObject != gameObject)
            {
                target = transform.position;
            }
        }
    }
}
