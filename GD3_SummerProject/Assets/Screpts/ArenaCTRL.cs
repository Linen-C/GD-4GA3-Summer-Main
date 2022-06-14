using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaCTRL : MonoBehaviour
{

    [Header("�ő�E���݃E�F�[�u��")]
    [SerializeField] int max_Wave;
    [SerializeField] public int now_Wave;
    [Header("BPM�R���g���[��(�����擾)")]
    [SerializeField] GC_BpmCTRL bpmCTRL;   // ���g���m�[���󂯎��p
    [Header("�G�Ǘ�")]
    [SerializeField] ArenaEnemyCTRL enemyCtrl;


    void Start()
    {
        var gameCtrlObj = GameObject.FindGameObjectWithTag("GameController");
        bpmCTRL = gameCtrlObj.GetComponent<GC_BpmCTRL>();

        enabled = false;
    }

    void Update()
    {
        if (enemyCtrl.DoEnemyAllDestroy())
        {
            Debug.Log("�E�F�[�u�i�s");
            WaveProgress();
        }
    }


    public void Entry()
    {
        enabled = true;
    }

    void Interval()
    {

    }

    void WaveProgress()
    {
        now_Wave++;

        if (max_Wave >= now_Wave){ enemyCtrl.WavaStart(); }
        else { ArenaClear(); }
    }

    void ArenaClear()
    {
        Debug.Log("�r��");
        enabled = false;
    }
}
