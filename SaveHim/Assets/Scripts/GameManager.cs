using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    GameObject[] LevelTexts;
    public GameObject ballPrefab;
    [Range(1f,2f)][SerializeField] float Ballspeed;

    [HideInInspector] public bool start = false;
    [HideInInspector] public bool gameover = false;
    [HideInInspector] public bool pause = false;

    private void Start() {
        LevelTexts = GameObject.FindGameObjectsWithTag("LevelText");
        for(int i = 0; i < LevelTexts.Length; i++)
        {
            LevelTexts[i].GetComponent<Text>().text = SceneManager.GetActiveScene().name.ToUpper();
        }
    }

    private void Update() 
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * 2f);
    }

    public void SpawnBall()
    {
        GameObject clone = Instantiate(ballPrefab,new Vector3(0f,-4.5f,0f) , Quaternion.identity);
        clone.GetComponent<Ball>().Ballspeed = Ballspeed;
    }

    public void StartLevel()
    {
        start = true;
        Destroy(EventSystem.current.currentSelectedGameObject.transform.root.gameObject);
    }
}
