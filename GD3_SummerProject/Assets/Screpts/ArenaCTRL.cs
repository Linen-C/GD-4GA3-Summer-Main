using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArenaCTRL : MonoBehaviour
{

    [Header("�ő�E���݃E�F�[�u��")]
    [SerializeField] int max_Wave;
    [SerializeField] public int now_Wave;
    [Header("BPM�R���g���[��(�����擾)")]
    [SerializeField] GC_BpmCTRL bpmCTRL;   // ���g���m�[���󂯎��p
    [Header("�G�Ǘ�")]
    [SerializeField] ArenaEnemyCTRL enemyCtrl;
    [Header("�C���^�[�o���p")]
    [SerializeField] int maxCount;
    [SerializeField] int nowCount;
    [SerializeField] bool interval;
    [SerializeField] Text interval_text;


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
            Interval();
        }
    }


    public void Entry()
    {
        enabled = true;
    }

    void Interval()
    {
        if (nowCount == -1)
        {
            nowCount = 0;
            interval = true;
        }

        Debug.Log("�C���^�[�o��");
        if (bpmCTRL.Metronome())
        {
            nowCount++;
            interval_text.text = nowCount.ToString();
        }

        if (nowCount > maxCount)
        {
            interval_text.text = " ";
            interval = false;
            WaveProgress();
        }
    }

    void WaveProgress()
    {
        now_Wave++;
        nowCount = -1;

        if (max_Wave >= now_Wave){ enemyCtrl.WavaStart(); }
        else { ArenaClear(); }
    }

    void ArenaClear()
    {
        Debug.Log("�r��");
        enabled = false;
    }
}
