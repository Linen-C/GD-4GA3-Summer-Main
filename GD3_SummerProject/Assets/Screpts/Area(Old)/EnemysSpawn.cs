using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemysSpawn : MonoBehaviour
{
    [Header("�G���A�I�u�W�F�N�g��������")]
    [SerializeField] GameObject areaObject;

    [Header("��������G�̃��X�g")]
    [SerializeField] GameObject[] spawnEnemyList;

    [Header("��������G�̈ʒu")]
    [SerializeField] Vector2[] spwanEnemyPos;

    [Header("�G���A�I�u�W�F�N�g�̍��W")]
    [SerializeField] Vector2 ereaPos;

    void Start()
    {
        ereaPos = new Vector2(areaObject.transform.position.x,
            areaObject.transform.position.y);
    }

    public void DoAwake()
    {
        if (spawnEnemyList.Length < 0) { return; }

        // ���W���@�e�̈ʒu+ ���Ă���΂�����

        for (int i = 0; spawnEnemyList.Length > i; i++)
        {
            float pointx = ereaPos.x + spwanEnemyPos[i].x;
            float pointy = ereaPos.y + spwanEnemyPos[i].y;

            Instantiate(
                spawnEnemyList[i],
                new Vector3(pointx, pointy, 0.0f),
                Quaternion.identity,
                transform);

            //Debug.Log("����");
        }

    }
}
