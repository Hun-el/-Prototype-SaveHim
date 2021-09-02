using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    GameManager gameManager;
    public GameObject pauseCanvasPrefab;
    GameObject pauseClone;

    private void Awake() {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update() {
        if(pauseClone == null && gameManager.pause == true)
        {
            gameManager.pause = false;
        }
    }

    public void PauseGame()
    {
        gameManager.pause = true;
        pauseClone = Instantiate(pauseCanvasPrefab);
    }
}
