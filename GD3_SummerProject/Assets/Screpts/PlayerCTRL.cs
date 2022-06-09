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
    private int needWeponCharge = 0;     // �K�v�N�[���_�E��
    private int weponCharge = 1;         // ���݃N�[���_�E��
    private bool coolDownReset = false;  // �N�[���_�E���̃��Z�b�g�t���O
    private float dogeTimer = 0;         // ���p�̃^�C�}�[
    private float knockBackCounter = 0;  // �m�b�N�o�b�N���ԃJ�E���^�[
    private float NonDamageTime = 0;     // ���G����
    WeponList getList;

    private int weponNo = 0;  // �������Ă��镐��ԍ�(0�`1)
    private Vector2 moveDir;


    // �R���|�[�l���g
    SpriteRenderer sprite;
    Rigidbody2D body;
    Animator anim;
    Animator flashAnim;
    PlayerControls plCtrls;

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
        TryGetComponent(out flashAnim);

        plCtrls = new PlayerControls();
        plCtrls.Enable();
    }

    void Start()
    {
        // ���평����
        getList = inputList.SendList();
        nowCharge = 0;  // 0�ŏ�����
        needWeponCharge = trail.SwapWeapon(getList, 0);

        // UI�n������
        UIUpdate();

        // �X�e�[�g������
        state = State.Stop;
        anim.SetBool("Alive", true);
    }

    void Update()
    {


        // �Ȃ񂾂��Ȃ�
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
        //Rotation();   // ����n
        Attack();     // �U��
        //Move();     // �ړ�
        Dash();       // ������
        //SwapWepon();  // �������
        Shooting();   // �������U��

    }

    void FixedUpdate()
    {
        // �X�e�[�g����
        if (state != State.Alive) { return; }

        Move(); // ��U�����ɂ��Ƃ���
    }


    // ���������� ���������� ���������� ���������� ���������� //
    // UI�X�V
    // ���������� ���������� ���������� ���������� ���������� //
    void UIUpdate()
    {
        hpText.text = "HP�F" + helthPoint.ToString();
        cooldownText.text = "Wepon : " + weponCharge + "/" + needWeponCharge;
        bulletText.text = "Shot : " + nowCharge + "/" + needCharge;
    }
    // ���������� ���������� ���������� ���������� ���������� //


    // ���������� ���������� ���������� ���������� ���������� //
    // ����
    // ���������� ���������� ���������� ���������� ���������� //

    // ����n
    void Rotation()
    {
        /*
        var padName = Input.GetJoystickNames();
        if (trail.attakingTime <= 0.0f)
        {
            if (padName.Length > 0) { CursorRotStick(); }
            else { CursorRotMouse(); }
        }
        */

        CursorRotMouse();
    }

    // ����i�L�[�{�[�h�E�}�E�X�j
    void CursorRotMouse()
    {
        // ���������� ���������� ���������� ���������� //
        // �J�[�\����]�i�}�E�X�j
        // ���������� ���������� ���������� ���������� //

        // �����̈ʒu
        Vector2 transPos = transform.position;

        // �X�N���[�����W�n�̃}�E�X���W�����[���h���W�n�ɏC��
        Vector2 mouseRawPos = new Vector2(0, 0);
        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseRawPos);

        // �x�N�g�����v�Z
        Vector2 diff = (mouseWorldPos - transPos).normalized;

        // ��]�ɑ��
        var curRot = Quaternion.FromToRotation(Vector3.up, diff);

        // �J�[�\������Ƀp�X
        cursor.GetComponent<Transform>().rotation = curRot;

        if (cursor.transform.eulerAngles.z < 180.0f) { sprite.flipX = true; }
        else { sprite.flipX = false; }

        // ���������� ���������� ���������� ���������� //
    }

    // ����i�X�e�B�b�N�j
    void CursorRotStick()
    {
        // ���������� ���������� ���������� ���������� //
        // �J�[�\����]�i�X�e�B�b�N�j
        // ���������� ���������� ���������� ���������� //

        /*

        var h = Input.GetAxisRaw("Horizontal2");
        var v = Input.GetAxisRaw("Vertical2");

        if (h == 0 && v == 0)
            return;

        float radian = Mathf.Atan2(h, v) * Mathf.Rad2Deg;

        if (radian < 0) { radian += 360; }

        cursor.GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, radian);

        if (cursor.transform.eulerAngles.z < 180.0f) { sprite.flipX = true; }
        else { sprite.flipX = false; }

        */

        // ���������� ���������� ���������� ���������� //
    }
    // ���������� ���������� ���������� ���������� ���������� //


    // ���������� ���������� ���������� ���������� ���������� //
    // �U��
    // ���������� ���������� ���������� ���������� ���������� //
    void Attack()
    {
        if ((Mouse.current.leftButton).wasPressedThisFrame
            && (weponCharge == needWeponCharge)
            && bpmCTRL.SendSignal()
            && coolDownReset == false)
        {
            anim.SetTrigger("Attack");
            trail.Attacking();
            coolDownReset = true;
        }

        if (bpmCTRL.Metronome())
        {
            if (coolDownReset == true)
            {
                weponCharge = 1;
                coolDownReset = false;
            }
            else if (weponCharge < needWeponCharge) { weponCharge++; }

            if (weponCharge == (needWeponCharge - 1))
            {
                anim.SetTrigger("Charge");
                flashAnim.SetTrigger("FlashTrigger");
            }
        }

    }
    // ���������� ���������� ���������� ���������� ���������� //


    // ���������� ���������� ���������� ���������� ���������� //
    // �ړ�
    // ���������� ���������� ���������� ���������� ���������� //
    
    // �ړ��n
    void Move()
    {
        // ���������� ���������� ���������� ���������� //
        // �ړ�
        // ���������� ���������� ���������� ���������� //

        if (knockBackCounter > 0.0f)
        {
            KnockBack();

            knockBackCounter -= Time.deltaTime;
        }
        else
        {
            body.velocity = new Vector2(moveDir.x * moveSpeed, moveDir.y * moveSpeed);

            /*
            body.velocity = new Vector2(
            Input.GetAxis("Horizontal") * moveSpeed,
            Input.GetAxis("Vertical") * moveSpeed);
            */
        }

        // ���������� ���������� ���������� ���������� //
        // ���
        // ���������� ���������� ���������� ���������� //

        if (dogeTimer > 0.0f)
        {
            /*
            body.AddForce(new Vector2(
                Input.GetAxis("Horizontal") * dashPower,
                Input.GetAxis("Vertical") * dashPower),
                ForceMode2D.Impulse);
            */
            dogeTimer -= Time.deltaTime;
        }

        // ���������� ���������� ���������� ���������� //
    }

    // �ړ�����(�Ă������_�b�V������)
    void Dash()
    {
        /*
        if ((Input.GetKeyDown(KeyCode.Space) ||
            Input.GetKeyDown(KeyCode.JoystickButton1))
            && bpmCTRL.SendSignal())
        {
            // Debug.Log("doge");
            dogeTimer = 0.1f;
        }
        */
    }
    // ���������� ���������� ���������� ���������� ���������� //


    // ���������� ���������� ���������� ���������� ���������� //
    // ����ύX
    // ���������� ���������� ���������� ���������� ���������� //
    void SwapWepon()
    {
        weponNo = 0;

        /*
        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Q) ||
            Input.GetKeyDown(KeyCode.JoystickButton3) || Input.GetKeyDown(KeyCode.JoystickButton2))
        {
            if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.JoystickButton3))
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
            else
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

            needWeponCharge = trail.SwapWeapon(getList, weponNo); // �K�v�N�[���_�E���㏑��   
            weponCharge = 0;    // ���N�[���_�E�����㏑��
        }
        */
    }
    // ���������� ���������� ���������� ���������� ���������� //


    // ���������� ���������� ���������� ���������� ���������� //
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
        /*
        if (bpmCTRL.SendSignal())
        {
            if (nowCharge == needCharge && (Input.GetMouseButton(1) || Input.GetKeyDown(KeyCode.JoystickButton4)))
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
        */

        //bulletText.text = "S�F" + nowCharge + "/" + needCharge;
    }
    // ���������� ���������� ���������� ���������� ���������� //


    // ���������� ���������� ���������� ���������� ���������� //
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
    // ���������� ���������� ���������� ���������� ���������� //


    // ���������� ���������� ���������� ���������� ���������� //
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

    // �Փ˔���
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
    // ���������� ���������� ���������� ���������� ���������� //

}
