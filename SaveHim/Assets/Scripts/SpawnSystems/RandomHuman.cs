using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomHuman : MonoBehaviour
{
    public GameObject[] Prefabs;
    
    Collider m_Collider;
    [HideInInspector]public Vector3 m_Min, m_Max;

    [Range(1,5)]public int womanCount,manCount;
    [Range(0,10)][SerializeField]int walkable;

    void Start()
    {
        m_Collider = GameObject.FindWithTag("SpawnArea").GetComponent<Collider>();
        m_Min = m_Collider.bounds.min;
        m_Max = m_Collider.bounds.max;

        if(walkable > womanCount + manCount)
        {
            walkable = womanCount + manCount;
        }
    }

    public void Spawn()
    {
        List<GameObject> humanClonesList = new List<GameObject>();
        for(int i = 0; i < womanCount; i++)
        {
            bool validPosition = true;

            float randomX = Random.Range(m_Min.x + 1f , m_Max.x - 1f);
            float randomZ = Random.Range(m_Min.z + 1f , m_Max.z - 1f);

            Vector3 spawnPoint = new Vector3(randomX , m_Max.y , randomZ);
            Collider[] colliders = Physics.OverlapSphere(spawnPoint, 1f);

            foreach(Collider col in colliders)
            {
                if(col.tag == "Woman" || col.tag == "Man" || col.tag == "movableWall")
                {
                    i--;
                    validPosition = false;
                }
            }

            if(validPosition)
            {
                GameObject Clone = Instantiate(Prefabs[0],spawnPoint,new Quaternion(0,180,0,0));
                humanClonesList.Add(Clone);
            }
        }

        for(int i = 0; i < manCount; i++)
        {
            bool validPosition = true;

            float randomX = Random.Range(m_Min.x + 1f , m_Max.x - 1f);
            float randomZ = Random.Range(m_Min.z + 1f , m_Max.z - 1f);

            Vector3 spawnPoint = new Vector3(randomX , m_Max.y , randomZ);
            Collider[] colliders = Physics.OverlapSphere(spawnPoint, 1f);

            foreach(Collider col in colliders)
            {
                if(col.tag == "Woman" || col.tag == "Man" || col.tag == "movableWall")
                {
                    i--;
                    validPosition = false;
                }
            }

            if(validPosition)
            {
                GameObject Clone = Instantiate(Prefabs[1],spawnPoint,new Quaternion(0,180,0,0));
                humanClonesList.Add(Clone);
            }
        }

        for(int i = 0; i < walkable; i++)
        {
            int randomNumber = Random.Range(0,humanClonesList.Count - 1);
            humanClonesList[randomNumber].GetComponent<Walk>().walk = true;
            humanClonesList.RemoveAt(randomNumber);
        }
    }
}
