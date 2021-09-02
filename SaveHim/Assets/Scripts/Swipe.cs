using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swipe : MonoBehaviour
{
    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;

    public float swipeRange;

    public direction currentDirection;
    public enum direction {none,left,right,up,down}

    void Update()
    {
        slowSwipe();
    }

    public void slowSwipe()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            startTouchPosition = Input.GetTouch(0).position;
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            endTouchPosition = Input.GetTouch(0).position;

            Vector2 Distance = endTouchPosition - startTouchPosition;

            if (Distance.x < -swipeRange)
            {
                currentDirection = direction.left;
            }
            else if (Distance.x > swipeRange)
            {
                currentDirection = direction.right;
            }
            else if (Distance.y > swipeRange)
            {
                currentDirection = direction.up;
            }
            else if (Distance.y < -swipeRange)
            {
                currentDirection = direction.down;
            }

            Invoke("Reset",0.25f);
        }
    }

    void Reset()
    {
        currentDirection = direction.none;
    }
}
