using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveArea : MonoBehaviour
{
    public GameObject gameoverPrefab,confettiPrefab;
    GameManager gameManager;
    RandomHuman randomGenerate;
    Text rescuedText;
    int rescued;

    private void Start() {
        randomGenerate = FindObjectOfType<RandomHuman>();
        gameManager = FindObjectOfType<GameManager>();
        rescuedText = GameObject.FindWithTag("RescuedText").GetComponent<Text>();
    }

    private void Update() {
        rescuedText.text = "Rescued : "+rescued.ToString();
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.layer == LayerMask.NameToLayer("Ball"))
        {
            Destroy(other.transform.root.gameObject);
            rescued++;

            if(randomGenerate.womanCount <= rescued)
            {
                StartCoroutine(gameOver());
            }
            else
            {
                gameManager.SpawnBall();
            }
        }
    }

    IEnumerator gameOver()
    {
        Collider m_Collider = GameObject.FindWithTag("Base").GetComponent<Collider>();
        Vector3 m_Center = m_Collider.bounds.center;
        m_Center = new Vector3(m_Center.x,m_Center.y+0.6f,m_Center.z);
        GameObject g = Instantiate(confettiPrefab,m_Center,Quaternion.identity);
        g.transform.eulerAngles = new Vector3(-90,0,0);

        yield return new WaitForSeconds(0.25f);
        Instantiate(gameoverPrefab);

        gameManager.gameover = true;
    }
}
