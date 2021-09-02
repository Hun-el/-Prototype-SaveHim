using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSpawn : MonoBehaviour
{
    public bool active;
    public GameObject wallPrefab;
    Collider m_Collider;
    [HideInInspector]public Vector3 m_Min, m_Max;

    void Start()
    {
        m_Collider = GameObject.FindWithTag("Base").GetComponent<Collider>();
        m_Min = m_Collider.bounds.min;
        m_Max = m_Collider.bounds.max;

        if(active)
        {
            Spawn();
        }
    }

    void Spawn()
    {
        Vector3 spawnPoint = new Vector3(m_Max.x - 1f , m_Max.y + 1.5f , m_Max.z);

        GameObject Clone = Instantiate(wallPrefab,spawnPoint,new Quaternion(0,180,0,0));
        Clone.GetComponent<movableWall>().SetTargets(new Vector3(m_Max.x - 1f,m_Max.y + 1.5f,Clone.transform.position.z),new Vector3(m_Min.x + 1f,m_Max.y + 1.5f,Clone.transform.position.z));
    }
}
