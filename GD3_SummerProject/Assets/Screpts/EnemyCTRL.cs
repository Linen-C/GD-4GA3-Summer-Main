using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCTRL : MonoBehaviour
{
    [Header("�������U���p�̉��g��")]
    [SerializeField] bool shootingType = false; // �������U���^�C�v��
    [SerializeField] GameObject bullet; // �e��

    // �ϐ�
    [Header("�X�e�[�^�X")]
    [SerializeField] int helthPoint;  // �̗�
    [Header("�ړ�")]
    [SerializeField] float moveSpeed;  // �ړ����x
    [Header("����")]
    [SerializeField] int needWeponCharge;  // �K�v�N�[���_�E��
    [Header("�m�b�N�o�b�N�Ɩ��G����")]
    [SerializeField] float knockBackPower;    // ������m�b�N�o�b�N�̋���
    [SerializeField] float defNonDamageTime;  // �f�t�H���g���G����

    // �X�N���v�g
    [Header("�X�N���v�g(�}�j���A��)")]
    [SerializeField] EnemyWepon ownWepon;  // ��������
    [Header("�X�N���v�g(�����擾)")]
    [SerializeField] GC_BpmCTRL bpmCTRL;   // ���g���m�[���󂯎��p
    [SerializeField] AreaCTRL areaCTRL;    // �G���A�R���|�[�l���g

    // �Q�[���I�u�W�F�N�g
    [Header("�Q�[���I�u�W�F�N�g(�}�j���A��)")]
    [SerializeField] GameObject cursor;    // �J�[�\���擾(�������ꂪ��ԑ���)
    [SerializeField] GameObject cursorImage;  // �J�[�\���C���[�W(TKIH)
    [SerializeField] GameObject flashObj;   // �t���b�V���p
    [Header("�Q�[���I�u�W�F�N�g(�����擾)")]
    [SerializeField] GameObject player;    // �v���C���[
    [SerializeField] GameObject areaObj;   // �G���A�I�u�W�F�N�g

    // �v���C�x�[�g�ϐ�
    private int weponCharge = 1;         // ���݃N�[���_�E��
    private bool coolDownReset = false;  // �N�[���_�E���̃��Z�b�g�t���O
    private Vector2 diff;                // �v���C���[�̕���
    private float knockBackCounter = 0;  // �m�b�N�o�b�N���ԃJ�E���^�[
    private float NonDamageTime = 0;     // ���G����

    // �R���|�[�l���g
    SpriteRenderer sprite;
    Rigidbody2D body;
    Animator anim;
    Animator flashAnim;
    Transform curTrans;

    enum State
    {
        Stop,
        Alive,
        Dead
    }
    State state;


    void Start()
    {
        if (!shootingType) { bullet = null; }

        // �e�G���A�R���|�[�l���g�̎擾
        areaObj = transform.parent.parent.gameObject;
        areaCTRL = areaObj.GetComponent<AreaCTRL>();

        // �L������
        var bpmCtrl = GameObject.FindGameObjectWithTag("GameController");
        bpmCTRL = bpmCtrl.GetComponent<GC_BpmCTRL>();

        // �Q����g���Ƃ��Ȃ������킢�c
        player = GameObject.FindGameObjectWithTag("Player");

        // �R���|�[�l���g�擾
        sprite = GetComponent<SpriteRenderer>();
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        flashAnim = flashObj.GetComponent<Animator>();
        curTrans = cursor.GetComponent<Transform>();

        // �X�e�[�g������
        state = State.Stop;
    }

    void Update()
    {
        // ��~����
        if (!areaCTRL.enabled)
        {
            state = State.Stop;
            anim.SetBool("Moving", false);
        }
        else
        {
            state = State.Alive;
            anim.SetBool("Moving", true);
        }

        // ���S����
        IfIsAlive();

        // �X�e�[�g����
        if (state != State.Alive)
        {
            body.velocity = new Vector2(0, 0);
            return;
        }
        else { anim.SetBool("Alive", true); }

        // ���G����
        if (NonDamageTime > 0) { NonDamageTime -= Time.deltaTime; }

        // ����
        TracePlayer();  // �v���C���[�⑫�E�ǐ�
        CursorRot();    // ����
        Attack();       // �U��
        //Move();         // �ړ�
    }

    void FixedUpdate()
    {
        // �X�e�[�g����
        if (state != State.Alive)
        {
            body.velocity = new Vector2(0, 0);
            return;
        }

        Move();
    }


    // �v���C���[�⑫�E�ǐ�
    void TracePlayer()
    {
        // ���������� ���������� ���������� ���������� //
        // �v���C���[�����̕⑫
        // ���������� ���������� ���������� ���������� //

        // �����̈ʒu
        Vector2 transPos = transform.position;

        // �v���C���[���W
        Vector2 playerPos = player.transform.position;

        // �x�N�g�����v�Z
        diff = (playerPos - transPos).normalized;
        // ���������� ���������� ���������� ���������� //
    }

    // ����
    void CursorRot()
    {
        // ���������� ���������� ���������� ���������� //
        // �J�[�\����]
        // ���������� ���������� ���������� ���������� //

        if (weponCharge == needWeponCharge)
        {
            return;
        }

        // ��]�ɑ��
        var curRot = Quaternion.FromToRotation(Vector3.up, diff);

        // �J�[�\������Ƀp�X
        curTrans.rotation = curRot;
        // ���������� ���������� ���������� ���������� //

        // �X�v���C�g���]
        if (curTrans.eulerAngles.z < 180.0f) { sprite.flipX = true; }
        else { sprite.flipX = false; }
    }

    // �U��
    void Attack()
    {
        // ���������� ���������� ���������� ���������� //
        // �U���E�N�[���_�E��
        // ���������� ���������� ���������� ���������� //

        if ((weponCharge == needWeponCharge) && bpmCTRL.Signal() && coolDownReset == false)
        {
            // Debug.Log("ENEMY_ATTACK");

            if (shootingType)
            {
                Instantiate(
                    bullet,
                    new Vector3
                    (cursorImage.transform.position.x,
                    cursorImage.transform.position.y,
                    cursorImage.transform.position.z),
                    cursor.transform.rotation);
            }
            else { ownWepon.Attacking(); }

            anim.SetTrigger("Attack");

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

        // ���������� ���������� ���������� ���������� //
    }

    // �ړ�
    void Move()
    {
        // ���������� ���������� ���������� ���������� //
        // �ړ�
        // ���������� ���������� ���������� ���������� //

        if(knockBackCounter > 0.0f)
        {
            body.AddForce(new Vector2(
                -diff.x * knockBackPower,
                -diff.y * knockBackPower),
                ForceMode2D.Impulse);

            knockBackCounter -= Time.deltaTime;
        }
        else
        {
            body.velocity = new Vector2(diff.x * moveSpeed, diff.y * moveSpeed);
        }

        // ���������� ���������� ���������� ���������� //
    }

    // ���S����
    void IfIsAlive()
    {
        if(helthPoint <= 0)
        {
            Destroy(gameObject, defNonDamageTime + 0.1f);
        }
    }



    // �_���[�W���󂯂�
    public void TakeDamage(int damage,int knockback)
    {
        helthPoint -= damage;
        knockBackPower = knockback;

        NonDamageTime = defNonDamageTime;
        knockBackCounter = 0.1f;
        
        anim.SetTrigger("Damage");
    }
}
