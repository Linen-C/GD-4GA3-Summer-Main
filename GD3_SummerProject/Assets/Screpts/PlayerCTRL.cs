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
    public float dashPower; // �_�b�V���p���[
    public PlayerWeapon weapon;   // �U���̃e�X�g�p

    // �L�����p�X
    public Text cooldownText;   // �N�[���_�E���\���p

    // �v���C�x�[�g�ϐ�
    private int weponCharge = 1;      // �N�[���_�E����
    private bool coolDownReset = false; // �N�[���_�E���̃��Z�b�g�t���O
    private float dogeTimer = 0;    // ���p�̃^�C�}�[

    // �R���|�[�l���g
    Rigidbody2D body;


    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }


    void Update()
    {

        var padName = Input.GetJoystickNames();
        if (padName.Length > 0) { CursorRotStick(); }
        else { CursorRotMouse(); }

        Attack();
        Move();

        if (Input.GetMouseButtonDown(1))
        {
            needWeponCharge = weapon.SwapoWeapon(); // �K�v�N�[���_�E���㏑��   
            weponCharge = 0;    // ���N�[���_�E�����㏑��
        }
    }

    void Move()
    {
        // ���������� ���������� ���������� ���������� //
        // �ړ�
        // ���������� ���������� ���������� ���������� //

        body.velocity = new Vector2(
            Input.GetAxis("Horizontal") * moveSpeed, Input.GetAxis("Vertical") * moveSpeed);

        // ���������� ���������� ���������� ���������� //
        // ���
        // ���������� ���������� ���������� ���������� //

        if ((Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.JoystickButton4)) && gameCTRL.SendSignal())
        {
            Debug.Log("doge");
            dogeTimer = 0.1f;
        }

        if (dogeTimer > 0.0f)
        {
            body.AddForce(new Vector2(Input.GetAxis("Horizontal") * dashPower, Input.GetAxis("Vertical") * dashPower), ForceMode2D.Impulse);
            dogeTimer -= Time.deltaTime;
        }

        // ���������� ���������� ���������� ���������� //
    }


    void CursorRotMouse()
    {
        // ���������� ���������� ���������� ���������� //
        // �J�[�\����]�i�}�E�X�j
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

    void CursorRotStick()
    {
        // ���������� ���������� ���������� ���������� //
        // �J�[�\����]�i�X�e�B�b�N�j
        // ���������� ���������� ���������� ���������� //

        var h = Input.GetAxisRaw("Horizontal2");
        var v = Input.GetAxisRaw("Vertical2");

        if (h == 0 && v == 0)
            return;

        float radian = Mathf.Atan2(h, v) * Mathf.Rad2Deg;

        if (radian < 0){ radian += 360; }

        Cursor.GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, radian);

        // ���������� ���������� ���������� ���������� //
    }


    void Attack()
    {
        // ���������� ���������� ���������� ���������� //
        // �U���E�N�[���_�E��
        // ���������� ���������� ���������� ���������� //

        if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.JoystickButton5))
            && (weponCharge == needWeponCharge) && gameCTRL.SendSignal())
        {
            Debug.Log("ATTACK");

            // ���ӁF���X�N���v�g�I
            // ���������� ���������� //
            weapon.Attacking();
            // ���������� ���������� //

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

    void GetWepon()
    {

        // ���������� ���������� ���������� ���������� //
        // ����擾
        // ���������� ���������� ���������� ���������� //

        needWeponCharge = 2;    // �K�v�N�[���_�E���㏑��
        weponCharge = 1;    // ���N�[���_�E�����㏑��

        // ���������� ���������� ���������� ���������� //
    }
}
