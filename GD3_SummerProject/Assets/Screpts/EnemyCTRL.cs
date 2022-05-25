using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCTRL : MonoBehaviour
{
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
    [SerializeField] GameObject Cursor;    // �J�[�\���擾(�������ꂪ��ԑ���)
    [Header("�Q�[���I�u�W�F�N�g(�����擾)")]
    [SerializeField] GameObject Player;    // �v���C���[
    [SerializeField] GameObject areaObj;   // �G���A�I�u�W�F�N�g

    // �v���C�x�[�g�ϐ�
    private int weponCharge = 1;         // ���݃N�[���_�E��
    private bool coolDownReset = false;  // �N�[���_�E���̃��Z�b�g�t���O
    private Vector2 diff;                // �v���C���[�̕���
    private float knockBackCounter = 0;  // �m�b�N�o�b�N���ԃJ�E���^�[
    private float NonDamageTime = 0;     // ���G����

    // �R���|�[�l���g
    Rigidbody2D body;

    public enum State
    {
        Stop,
        Alive,
        Dead
    }
    public State state;


    void Start()
    {
        // �R���|�[�l���g�擾
        body = GetComponent<Rigidbody2D>();

        // �e�G���A�R���|�[�l���g�̎擾
        areaObj = transform.parent.parent.gameObject;
        areaCTRL = areaObj.GetComponent<AreaCTRL>();

        // �L������
        var bpmCtrl = GameObject.FindGameObjectWithTag("GameController");
        bpmCTRL = bpmCtrl.GetComponent<GC_BpmCTRL>();

        // �Q����g���Ƃ��Ȃ������킢�c
        Player = GameObject.FindGameObjectWithTag("Player");

        // �X�e�[�g������
        state = State.Stop;
    }


    void Update()
    {
        // ���씻��
        if(areaCTRL.enabled == true){ state = State.Alive; }
        else { state = State.Stop; }

        // ��~
        if (state == State.Stop)
        {
            body.velocity = new Vector2(0, 0);
            return;
        }

        // ���S����
        if (state == State.Dead) { return; }
        IsDead();

        // ���G����
        if (NonDamageTime > 0) { NonDamageTime -= Time.deltaTime; }

        // ����
        TracePlayer();  // �v���C���[�ǐ�
        CursorRot();    // ����(�J�[�\����])
        Attack();       // �U��
        Move();         // �ړ�

    }


    // �v���C���[�ǐ�
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

    // ����(�J�[�\����])
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
        Cursor.GetComponent<Transform>().rotation = curRot;
        // ���������� ���������� ���������� ���������� //
    }

    // �U��
    void Attack()
    {
        // ���������� ���������� ���������� ���������� //
        // �U���E�N�[���_�E��
        // ���������� ���������� ���������� ���������� //

        if ((weponCharge == needWeponCharge) && bpmCTRL.SendSignal() && coolDownReset == false)
        {
            // Debug.Log("ENEMY_ATTACK");
            ownWepon.Attacking();

            coolDownReset = true;
        }

        if (bpmCTRL.Metronome())
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
    void IsDead()
    {
        if(helthPoint <= 0 && state != State.Dead)
        {
            Destroy(gameObject, defNonDamageTime + 0.1f);
            state = State.Dead;
        }
    }

    // �Փ˔���
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.tag == "PlayerAttack") && (NonDamageTime <= 0.0f))
        {
            helthPoint -= 1;
            NonDamageTime = defNonDamageTime;
            knockBackCounter = 0.1f;
        }
        //Debug.Log("�u���Ă��I�v");
    }

}
