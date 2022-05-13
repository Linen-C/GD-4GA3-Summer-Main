using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWepon : MonoBehaviour
{
    // �p�u���b�N�ϐ�
    public float defTime;   // �U������̔�������

    // �v���C�x�[�g�ϐ�
    float attakingTime = 0.0f;  // ����̔�������

    // �X�N���v�g
    public EnemyCTRL enemyCTRL;

    // �R���|�[�l���g
    BoxCollider2D coll;


    void Start()
    {
        coll = GetComponent<BoxCollider2D>();
        coll.enabled = false;
    }


    void Update()
    {
        // ���������� ���������� ���������� ���������� //
        // ���蔭��
        // ���������� ���������� ���������� ���������� //

        if (attakingTime > 0) { attakingTime -= Time.deltaTime; }
        else { coll.enabled = false; }

        // ���������� ���������� ���������� ���������� //
    }

    public void Attacking()
    {
        coll.enabled = true;
        attakingTime = defTime;
        Debug.Log("�G�l�~�[�F���蔭��");
    }
}
