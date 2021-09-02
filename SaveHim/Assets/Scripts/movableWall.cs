using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movableWall : MonoBehaviour
{
    [Range(0.5f,2f)][SerializeField] float speed;
    [HideInInspector] Vector3 point1 , point2;
    Vector3 target;

    private void Update() {
        if(Vector3.Distance(transform.position , target) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        }
        else
        {
            if(Vector3.Distance(transform.position , point1) > 1f)
            {
                target = point1;
            }
            else
            {
                target = point2;
            }
        }
    }

    public void SetTargets(Vector3 _point1,Vector3 _point2)
    {
        point1 = _point1;
        point2 = _point2;

        if(Vector3.Distance(transform.position , _point1) > 1f)
        {
            target = point1;
        }
        else
        {
            target = point2;
        }
    }
}
