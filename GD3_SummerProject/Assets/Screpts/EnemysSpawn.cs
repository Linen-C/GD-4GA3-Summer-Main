using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemysSpawn : MonoBehaviour
{

    [Header("��������G�̃��X�g")]
    [SerializeField] GameObject[] spawnEnemyList;

    [Header("��������G�̈ʒu")]
    [SerializeField] Vector2[] spwanEnemyPos;

    
    void Start()
    {

    }

    
    void Update()
    {
        
    }

    public void DoAwake()
    {
        if (spawnEnemyList.Length < 0) { return; }

        // ���W���@�e�̈ʒu+ ���Ă���΂�����

        for (int i = 0; spawnEnemyList.Length > i; i++)
        {
            float pointx = spwanEnemyPos[i].x;
            float pointy = spwanEnemyPos[i].y;

            Instantiate(
                spawnEnemyList[i],
                new Vector3(pointx, pointy, 0.0f),
                Quaternion.identity,
                transform);

            Debug.Log("����");
        }

    }
}
