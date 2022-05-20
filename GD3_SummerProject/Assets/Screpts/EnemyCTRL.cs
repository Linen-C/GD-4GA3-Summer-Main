using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCTRL : MonoBehaviour
{
    // �p�u���b�N�ϐ�
    [Header("�X�e�[�^�X")]
    public int helthPoint;  // �̗�
    [Header("�ړ�")]
    public float moveSpeed;  // �ړ����x
    [Header("����")]
    public int needWeponCharge;  // �K�v�N�[���_�E��
    [Header("�m�b�N�o�b�N�Ɩ��G����")]
    public float knockBackPower;    // ������m�b�N�o�b�N�̋���
    public float defNonDamageTime;  // �f�t�H���g���G����
    
    //[Header("�X�|�[���ʒu")]
    //[SerializeField] public Vector2 spawnPoint;  // �X�|�[���ʒu

    // �X�N���v�g
    [Header("�X�N���v�g")]
    public GameCTRL gameCTRL;    // ���g���m�[���󂯎��p
    public EnemyWepon ownWepon;  // ��������

    // �Q�[���I�u�W�F�N�g
    [Header("�Q�[���I�u�W�F�N�g")]
    public GameObject Cursor;    // �J�[�\���擾(�������ꂪ��ԑ���)
    public GameObject Player;    // �v���C���[

    // �v���C�x�[�g�ϐ�
    private int weponCharge = 1;         // ���݃N�[���_�E��
    private bool coolDownReset = false;  // �N�[���_�E���̃��Z�b�g�t���O
    private Vector2 diff;                // �v���C���[�̕���
    private float knockBackCounter = 0;  // �m�b�N�o�b�N���ԃJ�E���^�[
    private float NonDamageTime = 0;     // ���G����

    // �R���|�[�l���g
    Rigidbody2D body;

    void Start()
    {
        // �L������
        var gcCtrn = GameObject.FindGameObjectWithTag("GameController");
        gameCTRL = gcCtrn.GetComponent<GameCTRL>();

        // �Q����g���Ƃ��Ȃ������킢�c
        Player = GameObject.FindGameObjectWithTag("Player");

        body = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        Move();

        if (!IfIsAlive()) { return; }

        Attack();
        TracePlayer();
        CursorRot();

        if (NonDamageTime > 0) { NonDamageTime -= Time.deltaTime; }
    }


    void Attack()
    {
        // ���������� ���������� ���������� ���������� //
        // �U���E�N�[���_�E��
        // ���������� ���������� ���������� ���������� //

        if ((weponCharge == needWeponCharge) && gameCTRL.SendSignal() && coolDownReset == false)
        {
            // Debug.Log("ENEMY_ATTACK");
            ownWepon.Attacking();

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

    bool IfIsAlive()
    {
        if(helthPoint > 0) { return true; }
        else
        {
            Destroy(gameObject, defNonDamageTime + 0.1f);
            return false;
        }
    }

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
