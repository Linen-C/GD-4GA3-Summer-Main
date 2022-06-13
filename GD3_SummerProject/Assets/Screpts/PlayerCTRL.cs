using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayerCTRL : MonoBehaviour
{
    // �ϐ�
    [Header("�X�e�[�^�X")]
    [SerializeField] int helthPoint;  // �̗�
    [Header("�ړ�")]
    [SerializeField] float moveSpeed;  // �ړ����x
    [SerializeField] float dashPower;  // �_�b�V���p���[
    [Header("�������U��")]
    [SerializeField] int needCharge;  // �������U���ɕK�v�ȃ`���[�W
    [SerializeField] int nowCharge;   // ���݂̃`���[�W
    [Header("�m�b�N�o�b�N�Ɩ��G����")]
    [SerializeField] float knockBackPower;    // ������m�b�N�o�b�N�̋���
    [SerializeField] float defNonDamageTime;  // �f�t�H���g���G����

    // �Q�[���I�u�W�F�N�g
    [Header("�Q�[���I�u�W�F�N�g")]
    [SerializeField] GameObject cursor;  // �J�[�\���擾(�������ꂪ��ԑ���)
    [SerializeField] GameObject cursorImage;  // �J�[�\���C���[�W(TKIH)
    [SerializeField] GameObject bullet;  // �������U���p�̒e
    [SerializeField] GameObject flashObj;   // �t���b�V���p

    // �X�N���v�g
    [Header("�X�N���v�g")]
    [SerializeField] GC_BpmCTRL bpmCTRL;      // BPM�R���g���[���[
    [SerializeField] GC_jsonInput inputList;  // json�t�@�C������̎擾
    [SerializeField] PlayerWeapon trail;  // �U���p

    // �L�����p�X
    [Header("�L�����o�XUI")]
    [SerializeField] Text hpText;         // �̗͕\���p
    [SerializeField] Text cooldownText;   // �N�[���_�E���\���p
    [SerializeField] Text bulletText;     // �ˌ��`���[�W

    // �v���C�x�[�g�ϐ�
    private int maxWeponCharge = 0;     // �K�v�N�[���_�E��
    private int nowWeponCharge = 1;         // ���݃N�[���_�E��
    private int weponNo = 0;             // �������Ă��镐��ԍ�(0�`1)
    private bool coolDownReset = false;  // �N�[���_�E���̃��Z�b�g�t���O

    private int comboCountLeft = 0;
    private bool doComboMode = false;

    private float dogeTimer = 0;         // ���p�̃^�C�}�[
    private float knockBackCounter = 0;  // �m�b�N�o�b�N���ԃJ�E���^�[
    private float NonDamageTime = 0;     // ���G����
    private Vector2 moveDir;    // �ړ��p�x�N�g��

    // �R���|�[�l���g
    SpriteRenderer sprite;
    Rigidbody2D body;
    Animator anim;
    Animator flashAnim;
    PlayerControls plCtrls;
    JsonData getList;

    public enum State
    {
        Stop,
        Alive,
        Dead
    }
    public State state;


    void Awake()
    {
        // �R���|�[�l���g�擾
        TryGetComponent(out body);
        TryGetComponent(out sprite);
        TryGetComponent(out anim);
        //TryGetComponent(out flashAnim);

        flashAnim = flashObj.GetComponent<Animator>();
        plCtrls = new PlayerControls();
        
    }

    void Start()
    {
        // ���평����
        getList = inputList.SendList();
        nowCharge = 0;  // ������0�ŏ�����
        maxWeponCharge = trail.SwapWeapon(getList, 0);

        // UI�n������
        UIUpdate();

        // �X�e�[�g������
        state = State.Stop;
        anim.SetBool("Alive", true);

        // �C���v�b�g�V�X�e���L����
        plCtrls.Enable();
    }

    void Update()
    {
        // �ړ����͂̎擾
        moveDir = plCtrls.Player.Move.ReadValue<Vector2>();


        // UI�X�V
        UIUpdate();

        // ���S����
        IsDead();

        // �X�e�[�g����
        if (state != State.Alive)
        {
            anim.SetBool("Moving", false);
            body.velocity = new Vector2(0,0);
            return;
        }
        else { anim.SetBool("Moving", true); }

        // ���G����
        if (NonDamageTime > 0) { NonDamageTime -= Time.deltaTime; }

        // ����
        Rotation();   // ����n
        Attack();     // �U��
        Dash();       // �_�b�V������
        SwapWepon();  // �������
        Shooting();   // �������U��

    }

    void FixedUpdate()
    {
        // �X�e�[�g����
        if (state != State.Alive) { return; }

        Move();
    }




    // UI�X�V
    // ���������� ���������� ���������� ���������� ���������� //
    void UIUpdate()
    {
        hpText.text = "HP�F" + helthPoint.ToString();
        cooldownText.text = "Wepon : " + nowWeponCharge + "/" + maxWeponCharge;
        bulletText.text = "Shot : " + nowCharge + "/" + needCharge;
    }

    // ���������� ���������� ���������� ���������� ���������� //



    // ����
    // ���������� ���������� ���������� ���������� ���������� //

    // ����n
    void Rotation()
    {
        var padName = Gamepad.current;
        if (padName != null) { CursorRotStick(); }
        else { CursorRotMouse(); }

        if (cursor.transform.eulerAngles.z < 180.0f) { sprite.flipX = true; }
        else { sprite.flipX = false; }

    }

    // ����i�L�[�{�[�h�E�}�E�X�j
    void CursorRotMouse()
    {
        // �����̈ʒu
        Vector2 transPos = transform.position;

        // �X�N���[�����W�n�̃}�E�X���W�����[���h���W�n�ɏC��
        var rawDir = plCtrls.Player.MouseDir.ReadValue<Vector2>();
        Vector2 mouseDir = Camera.main.ScreenToWorldPoint(rawDir);

        // �x�N�g�����v�Z
        Vector2 diff = (mouseDir - transPos).normalized;

        // ��]�ɑ��
        var curRot = Quaternion.FromToRotation(Vector3.up, diff);

        // �J�[�\���Ƀp�X
        cursor.GetComponent<Transform>().rotation = curRot;
    }

    // ����i�X�e�B�b�N�j
    void CursorRotStick()
    {
        // �X�e�B�b�N�����擾
        var stickDir = plCtrls.Player.StickDir.ReadValue<Vector2>();

        // ���͂�������΍X�V���Ȃ�
        if (stickDir == new Vector2(0,0)) { return; }

        // �x�N�g�����v�Z
        float radian = Mathf.Atan2(stickDir.x, stickDir.y) * Mathf.Rad2Deg;

        // ��]�ɑ��
        if (radian < 0) { radian += 360; }

        // �J�[�\���Ƀp�X
        cursor.GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, radian);
    }

    // ���������� ���������� ���������� ���������� ���������� //



    // �U��
    // ���������� ���������� ���������� ���������� ���������� //
    void Attack()
    {
        

        if (plCtrls.Player.Attack.triggered
            && bpmCTRL.Signal()
            && coolDownReset == false)
        {
            anim.SetTrigger("Attack");
            trail.Attacking(nowWeponCharge);
            coolDownReset = true;
        }

        if (trail.Combo())
        {
            nowWeponCharge = maxWeponCharge;
            doComboMode = true;
            coolDownReset = false;
            comboCountLeft = 2;
        }

        
        if (bpmCTRL.Metronome())
        {
            if (coolDownReset == true && doComboMode == false)
            {
                nowWeponCharge = 1;
                coolDownReset = false;
            }
            else if (nowWeponCharge < maxWeponCharge) { nowWeponCharge++; }

            if (comboCountLeft > 0)
            {
                comboCountLeft--;
            }
            if (comboCountLeft == 0 && doComboMode == true)
            {
                nowWeponCharge = 1;
                doComboMode = false;
                coolDownReset = true;
            }


            if (nowWeponCharge == (maxWeponCharge - 1))
            {
                anim.SetTrigger("Charge");
                flashAnim.SetTrigger("FlashTrigger");
            }
        }

    }

    // ���������� ���������� ���������� ���������� ���������� //



    // �_�b�V������
    // ���������� ���������� ���������� ���������� ���������� //
    void Dash()
    {
        if (plCtrls.Player.Dash.triggered && bpmCTRL.Signal()) { dogeTimer = 0.1f; }
    }

    // ���������� ���������� ���������� ���������� ���������� //



    // �ړ�
    // ���������� ���������� ���������� ���������� ���������� //

    // �ړ��n
    void Move()
    {
        // �ړ�
        if (knockBackCounter > 0.0f)
        {
            KnockBack();

            knockBackCounter -= Time.deltaTime;
        }
        else
        {
            body.velocity = new Vector2(moveDir.x * moveSpeed, moveDir.y * moveSpeed);
        }

        // ���
        if (dogeTimer > 0.0f)
        {
            body.AddForce(new Vector2(moveDir.x * dashPower, moveDir.y * dashPower), ForceMode2D.Impulse);

            dogeTimer -= Time.deltaTime;
        }
    }



    // ����ύX
    // ���������� ���������� ���������� ���������� ���������� //
    void SwapWepon()
    {
        var valueW = plCtrls.Player.WeponSwapWhile.ReadValue<float>();
        var valueUp = plCtrls.Player.WeponSwapButtonUp.triggered;
        var valueDwon = plCtrls.Player.WeponSwapButtonDown.triggered;

        if (valueW != 0 || (valueUp || valueDwon))
        {
            if (valueW > 0 || valueUp)
            {
                switch (weponNo)
                {
                    case 0:
                        weponNo = 1;
                        break;
                    case 1:
                        weponNo = 2;
                        break;
                    case 2:
                        weponNo = 0;
                        break;
                    default:
                        weponNo = 0;
                        break;
                }
            }

            if (valueW < 0 || valueDwon)
            {
                switch (weponNo)
                {
                    case 0:
                        weponNo = 2;
                        break;
                    case 1:
                        weponNo = 0;
                        break;
                    case 2:
                        weponNo = 1;
                        break;
                    default:
                        weponNo = 0;
                        break;
                }
            }

            maxWeponCharge = trail.SwapWeapon(getList, weponNo); // �K�v�N�[���_�E���㏑��   
            nowWeponCharge = 0;    // ���N�[���_�E�����㏑��
        }
    }



    // �������U��
    // ���������� ���������� ���������� ���������� ���������� //

    // �������U���`���[�W
    public void GetCharge()
    {
        if (nowCharge < needCharge) { nowCharge += 1; }
    }

    // �������U��
    void Shooting()
    {
        if (bpmCTRL.Signal())
        {
            if (nowCharge == needCharge && plCtrls.Player.Shot.triggered)
            {
                // �������U������
                Debug.Log("�������U��");

                Instantiate(
                    bullet,
                    new Vector3
                    (cursorImage.transform.position.x,
                    cursorImage.transform.position.y,
                    cursorImage.transform.position.z),
                    cursor.transform.rotation);

                nowCharge = 0;
            }
        }
    }




    // ���S����
    // ���������� ���������� ���������� ���������� ���������� //
    void IsDead()
    {
        if (helthPoint <= 0)
        {
            hpText.text = "HP�F" + helthPoint.ToString();
            state = State.Dead;
        }
    }




    // ���̑�
    // ���������� ���������� ���������� ���������� ���������� //

    // �m�b�N�o�b�N
    void KnockBack()
    {
        var diff = FetchNearObjectWithTag("Enemy");

        body.AddForce(new Vector2(
                -diff.x * knockBackPower,
                -diff.y * knockBackPower),
                ForceMode2D.Impulse);
    }

    // �ł��߂��G�I�u�W�F�N�g�̎擾
    private Vector2 FetchNearObjectWithTag(string tagName)
    {
        GameObject nearEnemy = null;

        var targets = GameObject.FindGameObjectsWithTag(tagName);
        var minTargetDist = float.MaxValue;

        if (targets != null) { return new Vector2(0, 0); }

        foreach (var target in targets)
        {
            var targetDist = Vector2.Distance(
                transform.position,
                target.transform.position);

            if (!(targetDist < minTargetDist)) { continue; }

            minTargetDist = targetDist;
            nearEnemy = target.transform.gameObject;
        }


        // �����̈ʒu
        Vector2 transPos = transform.position;

        // �ł��߂����W
        Vector2 enemyPos = nearEnemy.transform.position;

        // �x�N�g�����v�Z
        Vector2 diff = (enemyPos - transPos).normalized;

        return diff;
    }

    // ��_���[�W����
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EnemyAttack" && (NonDamageTime <= 0.0f))
        {
            NonDamageTime = defNonDamageTime;
            knockBackCounter = 0.2f;
            helthPoint -= 1;
            anim.SetTrigger("Damage");
        }
    }


}
