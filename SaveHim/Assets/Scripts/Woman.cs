using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Woman : MonoBehaviour
{
    Rigidbody Rbody;

    Vector3 targetPos;

    [HideInInspector] public bool injured;
    bool save,swipeDown;

    Swipe swipe;

    RandomHuman randomGenerate;

    private void Awake() {
        randomGenerate = FindObjectOfType<RandomHuman>();
        Rbody = transform.GetChild(1).GetComponent<Rigidbody>();
        swipe = FindObjectOfType<Swipe>();
    }
    
    IEnumerator SaveHim()
    {
        yield return new WaitForSeconds(0.75f);
        save = true;
    }

    private void Update() {
        #if UNITY_ANDROID && !UNITY_EDITOR
        if(save && !swipeDown && swipe.currentDirection == Swipe.direction.down)
        {
            swipeDown = true;
        }
        #endif

        #if UNITY_STANDALONE || UNITY_EDITOR
        if(save && !swipeDown)
        {
            swipeDown = true;
        }
        #endif
    }

    private void FixedUpdate() 
    {
        if(swipeDown && Vector3.Distance(targetPos , transform.GetChild(1).position) > 2.75f)
        {
            Rbody.velocity = (targetPos - transform.GetChild(1).position) * 0.75f;
        }
        else if(swipeDown)
        {
            Vector3 v = GameObject.FindWithTag("SaveArea").transform.position;
            targetPos = new Vector3(v.x,v.y + 4f,v.z);
        }
    }

    public void injury()
    {
        Vector3 v = GameObject.FindWithTag("SaveArea").transform.position;
        targetPos = new Vector3(transform.position.x,v.y + 4f,transform.position.z);

        injured = true;
        Destroy(GetComponent<Walk>());

        StartCoroutine(SaveHim());
    }
}
