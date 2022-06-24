using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayerCTRL : MonoBehaviour
{
    // �ϐ�
    [Header("�X�e�[�^�X")]
    [SerializeField] int maxHelthPoint;      // �ő�̗�
    [SerializeField] int nowHelthPoint = 0;  // ���ݑ̗�

    [Header("���G����")]
    [SerializeField] public float defNonDamageTime;  // �f�t�H���g���G����

    // �Q�[���I�u�W�F�N�g
    [Header("�Q�[���I�u�W�F�N�g(�}�j���A��)")]
    [SerializeField] GameObject flashObj;   // �t���b�V���p

    // �X�N���v�g
    [Header("�R���|�[�l���g(�}�j���A��)")]
    [SerializeField] GC_GameCTRL gamectrl;  // ���낢�����Ă���p
    

    [Header("�R���|�[�l���g(�I�[�g)")]
    [SerializeField] GC_BpmCTRL _bpmCTRL;        // BPM�R���g���[���[
    [SerializeField] PlayerWeapon playerWeapon;  // �U���p
    [SerializeField] EquipLoad equipLoad;       // ��������擾

    [Header("�R���|�[�l���g(�}�j���A��)")]
    [SerializeField] PlayerMove _playerMove;
    [SerializeField] PlayerRotation _playerRotation;
    [SerializeField] PlayerAttack _playerAttack;

    // �G���W���ˑ��R���|�[�l���g
    [Header("�R���|�[�l���g(�I�[�g)")]
    [SerializeField] SpriteRenderer _sprite;
    [SerializeField] Rigidbody2D _body;
    [SerializeField] public Animator _anim;
    [SerializeField] public Animator _flashAnim;
    [SerializeField] PlayerControls _playerControls;

    public JsonData equipList; // �����擾

    // �L�����p�X
    [Header("�L�����o�XUI(�}�j���A��)")]
    //[SerializeField] Text hpText;         // �̗͕\���p
    [SerializeField] Text text_Wepon;   // �N�[���_�E���\���p
    [SerializeField] Slider slider_Wepon;
    [SerializeField] Text text_Gun;     // �ˌ��`���[�W
    [SerializeField] Slider slider_Gun;

    // �̗͕\��
    [Header("�̗͕\��(�}�j���A��)")]
    [SerializeField] Slider hpSlider;

    // �v���C�x�[�g�ϐ�
    [Header("�v���C�x�[�g�ϐ�����������")]
    public int maxWeponCharge = 0;      // �K�v�N�[���_�E��
    public int nowWeponCharge = 1;      // ���݃N�[���_�E��
    public int equipNo = 0;            // �������Ă��镐��ԍ�(0�`2)
    public bool coolDownReset = false;  // �N�[���_�E���̃��Z�b�g�t���O

    public int comboTimeLeft = 0;     // �R���{�p���J�E���^�[
    public bool doComboMode = false;  // �R���{���[�h
    //private int comboCount = 0;        // �R���{�񐔃J�E���^�[

    public float knockBackCounter = 0;  // �m�b�N�o�b�N���ԃJ�E���^�[
    private float NonDamageTime = 0;     // ���G����
    private Vector2 _moveDir;             // �ړ��p�x�N�g��

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
        playerWeapon = GetComponentInChildren<PlayerWeapon>();
        _bpmCTRL = gamectrl.transform.GetComponent<GC_BpmCTRL>();
        equipLoad = gamectrl.transform.GetComponent<EquipLoad>();

        _playerMove = GetComponent<PlayerMove>();

        TryGetComponent(out _body);
        TryGetComponent(out _sprite);
        TryGetComponent(out _anim);
        _flashAnim = flashObj.GetComponent<Animator>();
        _playerControls = new PlayerControls();
        
    }

    void Start()
    {
        // �̗�(�ƕ\��)������
        nowHelthPoint = maxHelthPoint;
        hpSlider.value = 1;

        // ���평����
        equipList = equipLoad.GetList();
        maxWeponCharge = playerWeapon.SwapWeapon(equipList.weaponList, 0);
        _playerAttack.nowGunCharge = 0;

        // UI�n������
        UIUpdate();

        // �X�e�[�g������
        state = State.Stop;
        _anim.SetBool("Alive", true);

        // �C���v�b�g�V�X�e���L����
        _playerControls.Enable();
    }

    void Update()
    {
        // �ړ����͂̎擾
        _moveDir = _playerControls.Player.Move.ReadValue<Vector2>();


        // UI�X�V
        UIUpdate();

        SetHP();

        // ���S����
        IsDead();

        // �X�e�[�g����
        if (state != State.Alive)
        {
            _anim.SetBool("Moving", false);
            _body.velocity = new Vector2(0,0);
            return;
        }
        else { _anim.SetBool("Moving", true); }

        // ���G����
        if (NonDamageTime > 0) { NonDamageTime -= Time.deltaTime; }

        // ����
        _playerRotation.Rotation(_playerControls, _sprite);             // ����n
        _playerAttack.Attack(_playerControls, _bpmCTRL, playerWeapon);  // �U��
        _playerAttack.SwapWepon(_playerControls, playerWeapon);         // �������
        _playerAttack.Shooting(_playerControls, _bpmCTRL);              // �������U��
        _playerMove.Dash(_playerControls, _bpmCTRL);                    // �_�b�V������

    }

    void FixedUpdate()
    {
        // �X�e�[�g����
        if (state != State.Alive) { return; }

        _playerMove.Move(_moveDir, _body);
        //Move();
    }



    // UI�X�V
    // ���������� ���������� ���������� ���������� ���������� //
    void UIUpdate()
    {
        //hpText.text = "HP�F" + nowHelthPoint.ToString();
        text_Wepon.text = nowWeponCharge + " / " + maxWeponCharge;
        slider_Wepon.value = (float)nowWeponCharge / (float)maxWeponCharge;
        text_Gun.text = _playerAttack.nowGunCharge + " / " + _playerAttack.needGunCharge;
        slider_Gun.value = (float)_playerAttack.nowGunCharge / (float)_playerAttack.needGunCharge;
    }



    // �̗͕\��
    // ���������� ���������� ���������� ���������� ���������� //
    void SetHP()
    {
        hpSlider.value = (float)nowHelthPoint / (float)maxHelthPoint;
    }



    // �������U���`���[�W
    // ���������� ���������� ���������� ���������� ���������� //
    public void GetCharge()
    {
        if (_playerAttack.nowGunCharge < _playerAttack.needGunCharge)
        {
            _playerAttack.nowGunCharge += 1;
        }
    }



    // ���S����
    // ���������� ���������� ���������� ���������� ���������� //
    void IsDead()
    {
        if (nowHelthPoint <= 0)
        {
            //hpText.text = "HP�F" + nowHelthPoint.ToString();
            state = State.Dead;
        }
    }



    // ���̑�
    // ���������� ���������� ���������� ���������� ���������� //

    // ��_���[�W����
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EnemyAttack" && (NonDamageTime <= 0.0f))
        {
            NonDamageTime = defNonDamageTime;
            knockBackCounter = 0.2f;
            nowHelthPoint -= 1;
            _anim.SetTrigger("Damage");
        }
    }

}
