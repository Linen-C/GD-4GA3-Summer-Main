using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCTRL : MonoBehaviour
{
    // �p�u���b�N�ϐ�
    public float moveSpeed;        // �ړ����x
    public GameCTRL gameCTRL;      // �Q�[���R���g���[���[
    public int needWeponCharge;   // �N�[���_�E����
    public GameObject Cursor;   // �J�[�\���擾(�������ꂪ��ԑ���)

    // �L�����p�X
    public Text cooldownText;   // �N�[���_�E���\���p

    // �v���C�x�[�g�ϐ�
    private int weponCharge = 1;      // �N�[���_�E����
    private bool coolDownReset = false; // �N�[���_�E���̃��Z�b�g�t���O

    private float dogeTimer = 0;

    // �R���|�[�l���g
    Rigidbody2D body;


    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        CursorRot();
        Attack();
        Move();
    }


    void Move()
    {
        // ���������� ���������� ���������� ���������� //
        // �ړ�
        // ���������� ���������� ���������� ���������� //
        body.velocity = new Vector2(
            Input.GetAxis("Horizontal") * moveSpeed, Input.GetAxis("Vertical") * moveSpeed);




        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Debug.Log("doge");
            dogeTimer = 0.2f;
        }

        if (dogeTimer > 0.0f)
        {
            body.AddForce(new Vector2(30.0f, 0f), ForceMode2D.Impulse);
            dogeTimer -= Time.deltaTime;
        }

        // ���������� ���������� ���������� ���������� //
    }


    void CursorRot()
    {
        // ���������� ���������� ���������� ���������� //
        // �J�[�\����]
        // ���������� ���������� ���������� ���������� //

        // �����̈ʒu
        Vector2 transPos = transform.position;
        //Debug.Log("tX" + transPos.x + "_" + "tY" + transPos.y);

        // �X�N���[�����W�n�̃}�E�X���W�����[���h���W�n�ɏC��
        Vector2 mouseRawPos = Input.mousePosition;
        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseRawPos);
        //Debug.Log("mX" + mouseWorldPos.x + "_"+ "mY" + mouseWorldPos.y);

        // �x�N�g�����v�Z
        Vector2 diff = (mouseWorldPos - transPos).normalized;

        // ��]�ɑ��
        var curRot = Quaternion.FromToRotation(Vector3.up, diff);

        // �J�[�\������Ƀp�X
        Cursor.GetComponent<Transform>().rotation = curRot;
        // ���������� ���������� ���������� ���������� //
    }


    void Attack()
    {
        // ���������� ���������� ���������� ���������� //
        // �U���E�N�[���_�E��
        // ���������� ���������� ���������� ���������� //
        if (Input.GetMouseButtonDown(0) && (weponCharge == needWeponCharge) && gameCTRL.SendSignal())
        {
            Debug.Log("ATTACK");
            UseWepon();
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
        cooldownText.text = "COOL:" + weponCharge;
        // ���������� ���������� ���������� ���������� //
    }

    void UseWepon()
    {



        // ���������� ���������� ���������� ���������� //
        // �U������
        // ���������� ���������� ���������� ���������� //

        /*
         *�P�j�֐��Ăяo��
         *�Q�j��������󂯎��
         *�R�j����������ɍU���𔭐�������
         */

        // ���������� ���������� ���������� ���������� //
    }
}
