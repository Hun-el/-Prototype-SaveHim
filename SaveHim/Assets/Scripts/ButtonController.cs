using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    public void StartLevel()
    {
        Destroy(transform.root.gameObject);
    }

    public void destroyMe(float time)
    {
        Destroy(this.transform.root.gameObject,time);
    }
}
