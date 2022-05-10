using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCTRL : MonoBehaviour
{
    // �p�u���b�N�ϐ�
    public float moveSpeed;        // �ړ����x
    //public int needWeponCharge;   // �N�[���_�E��
    public GameObject Cursor;   // �J�[�\���擾(�������ꂪ��ԑ���)
    public GameObject Player;   // �v���C���[

    // �v���C�x�[�g�ϐ�
    //private int weponCharge = 1;      // �N�[���_�E����
    private Vector2 diff;   // �v���C���[�̕���

    // �R���|�[�l���g
    Rigidbody2D body;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        TracePlayer();
        CursorRot();
        Move();
    }


    void TracePlayer()
    {
        // ���������� ���������� ���������� ���������� //
        // �v���C���[�����̕⑫
        // ���������� ���������� ���������� ���������� //

        // �����̈ʒu
        Vector2 transPos = transform.position;

        // �v���C���[���W
        Vector2 playerPos = Player.transform.position;

        // �x�N�g�����v�Z
        diff = (playerPos - transPos).normalized;
        // ���������� ���������� ���������� ���������� //
    }

    void CursorRot()
    {
        // ���������� ���������� ���������� ���������� //
        // �J�[�\����]
        // ���������� ���������� ���������� ���������� //

        // ��]�ɑ��
        var curRot = Quaternion.FromToRotation(Vector3.up, diff);

        // �J�[�\������Ƀp�X
        Cursor.GetComponent<Transform>().rotation = curRot;
        // ���������� ���������� ���������� ���������� //
    }
    void Move()
    {
        // ���������� ���������� ���������� ���������� //
        // �ړ�
        // ���������� ���������� ���������� ���������� //

        // �v���C���[�Ɍ������Ĉړ����邾���̃��c�����ǈꉞ�֐���
        body.velocity = new Vector2(diff.x * moveSpeed, diff.y * moveSpeed);

        // ���������� ���������� ���������� ���������� //
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("�u���Ă��I�v");
    }

}
