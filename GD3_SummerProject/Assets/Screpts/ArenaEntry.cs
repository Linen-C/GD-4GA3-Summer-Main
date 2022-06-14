using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaEntry : MonoBehaviour
{
    [Header("�A���[�i(�����擾)")]
    [SerializeField] ArenaCTRL arenaCtrl;
    [Header("�Q�[���R���g���[��(�����擾)")]
    [SerializeField] GC_GameCTRL gameCtrl;

    void Start()
    {
        arenaCtrl = transform.parent.GetComponent<ArenaCTRL>();

        var gameCtrlObj = GameObject.FindGameObjectWithTag("GameController");
        gameCtrl = gameCtrlObj.GetComponent<GC_GameCTRL>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player") { return; }

        // �N��
        arenaCtrl.Entry();

        //gameCtrl.areaCtrl = arenaCtrl;

        // �p�ς݂Ȃ̂ŏ���
        Destroy(gameObject);
    }

}
