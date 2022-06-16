using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaCTRL : MonoBehaviour
{
    [Header("�N���A�t���O")]
    [SerializeField] bool clearFlag = false;
    [Header("�e�Q�[�g�ʒu")]
    [SerializeField] public GateCTRL[] gateCTRLs;
    [Header("�G�l�~�[(�����擾)")]
    [SerializeField] Transform enemys;
    [SerializeField] Transform[] enemyList;
    [Header("�G���h�|�C���g�t���O")]
    [SerializeField] bool endPoint = false;
    [Header("�Q�[���R���g���[��(�����擾)")]
    [SerializeField] GameObject gameCtrl;


    void Start()
    {
        if (endPoint) { gameCtrl = GameObject.FindGameObjectWithTag("GameController"); }
        enabled = false;
    }

    void Update()
    {
        enemyList = GetEnemyList(enemys);

        if (enemyList.Length <= 0)
        {
            if (endPoint)
            {
                SendGameEND();
                return;
            }

            for (int i = 0; i < gateCTRLs.Length; i++)
            {
                gateCTRLs[i].GateOpen();
            }

            Debug.Log("�r��");
            enabled = false;
        }
    }

    private Transform[] GetEnemyList(Transform parent)
    {
        var children = new Transform[parent.childCount];

        for (int i = 0; i < children.Length; i++)
        {
            children[i] = parent.GetChild(i);
        }

        return children;
    }

    public bool DoClearFlag()
    {
        return clearFlag;
    }

    public void DoAwake()
    {
        enabled = true;
    }

    void SendGameEND()
    {
        gameCtrl.SendMessage("S_GameClear");
    }
}
