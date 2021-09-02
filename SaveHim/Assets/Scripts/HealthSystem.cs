using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    GameManager gameManager;
    [Range(1,5)] public int Health;
    public GameObject heartPrefab;
    public GameObject gameoverPrefab;
    Transform spawnLoc;

    List<GameObject> heartsList = new List<GameObject>();

    private void Start() {
        gameManager = FindObjectOfType<GameManager>();

        spawnLoc = GameObject.FindWithTag("healthSpawnLoc").transform;
        for(int i = 0; i < Health; i++)
        {
            GameObject g = Instantiate(heartPrefab,spawnLoc);
            heartsList.Add(g.transform.GetChild(0).gameObject);
        }
    }

    public void Decrease()
    {
        Health--;

        GameObject g = heartsList[heartsList.Count - 1];
        Destroy(g);

        heartsList.RemoveAt(heartsList.Count - 1);

        if(Health <= 0)
        {
            StartCoroutine(Gameover());
        }
    }

    IEnumerator Gameover()
    {
        yield return new WaitForSeconds(0.25f);
        Instantiate(gameoverPrefab);
        gameManager.gameover = true;
        yield return new WaitForSeconds(2f);
        LoadingSystem loadingSystem = FindObjectOfType<LoadingSystem>();
        loadingSystem.LoadLevel("Restart");
    }

}
