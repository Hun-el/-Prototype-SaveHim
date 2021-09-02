using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Man : MonoBehaviour
{
    GameManager gameManager;
    HealthSystem healthSystem;

    [HideInInspector] public bool injured;

    private void Awake() {
        gameManager = FindObjectOfType<GameManager>();
        healthSystem = FindObjectOfType<HealthSystem>();
    }

    public void injury()
    {
        Camera.main.GetComponent<DOTweenAnimation>().DORestart();

        injured = true;
        Destroy(GetComponent<Walk>());
            
        healthSystem.Decrease();
    }
}
