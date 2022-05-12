using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCTRL : MonoBehaviour
{
    // �p�u���b�N�ϐ�
    public float moveSpeed;        // �ړ����x
    public int needWeponCharge;   // �K�v�N�[���_�E��
    public float knockBackPower;    // ������m�b�N�o�b�N�̋���

    // �X�N���v�g
    public GameCTRL gameCTRL;

    public GameObject Cursor;   // �J�[�\���擾(�������ꂪ��ԑ���)
    public GameObject Player;   // �v���C���[

    // �v���C�x�[�g�ϐ�
    public int weponCharge = 1;      // ���݃N�[���_�E��
    private bool coolDownReset = false; // �N�[���_�E���̃��Z�b�g�t���O
    private Vector2 diff;   // �v���C���[�̕���
    private float knockBack = 0;    // �m�b�N�o�b�N���ԃJ�E���^�[

    // �R���|�[�l���g
    Rigidbody2D body;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        Attack();
        TracePlayer();
        CursorRot();
        Move();
    }


    void Attack()
    {
        // ���������� ���������� ���������� ���������� //
        // �U���E�N�[���_�E��
        // ���������� ���������� ���������� ���������� //

        if ((weponCharge == needWeponCharge) && gameCTRL.SendSignal() && coolDownReset == false)
        {
            Debug.Log("ENEMY_ATTACK");

            coolDownReset = true;
        }

        if (gameCTRL.Metronome())
        {
            if (coolDownReset == true)
            {
                weponCharge = 1;
                coolDownReset = false;
            }
            else if (weponCharge < needWeponCharge)
            {
                weponCharge++;
            }
        }

        // ���������� ���������� ���������� ���������� //
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

        if(knockBack > 0.0f)
        {
            body.AddForce(new Vector2(
                -diff.x * knockBackPower,
                -diff.y * knockBackPower),
                ForceMode2D.Impulse);

            knockBack -= Time.deltaTime;
        }
        else
        {
            body.velocity = new Vector2(diff.x * moveSpeed, diff.y * moveSpeed);
        }

        // ���������� ���������� ���������� ���������� //
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        knockBack = 0.1f;
        //Debug.Log("�u���Ă��I�v");
    }

}
