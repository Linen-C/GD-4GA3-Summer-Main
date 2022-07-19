using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon_B : MonoBehaviour
{
    // �ϐ�
    [Header("�ϐ�")]
    [SerializeField] float defAttackingTime = 0.3f;   // �U������̊�b��������
    [SerializeField] public float nowAttakingTime = 0.0f;  // ����̔�������

    // �X�N���v�g
    [Header("�X�N���v�g")]
    [SerializeField] PlayerCTRL _playerCTRL;
    [SerializeField] PlayerAttack_B _playerAttack;
    [SerializeField] SpriteChanger _spriteChanger;

    [Header("�p�����[�^")]
    [SerializeField] int _damage = 0;
    [SerializeField] int _knockBack = 0;
    [SerializeField] int _stanPower = 0;

    // �v���C�x�[�g�ϐ�
    float _spriteAlpha = 0.0f;
    float _hitCoolDown = 0.0f;
    bool _comboFlag = false;

    // �R���|�[�l���g
    BoxCollider2D coll;


    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<BoxCollider2D>();
        coll.enabled = false;

        _spriteChanger.ChangeTransparency(_spriteAlpha);
    }

    // Update is called once per frame
    void Update()
    {
        // ���蔭��
        // ���������� ���������� ���������� ���������� //
        if (nowAttakingTime >= 0)
        {
            nowAttakingTime -= Time.deltaTime;
        }
        else { coll.enabled = false; }

        if (_spriteAlpha > 0.0f)
        {
            _spriteChanger.ChangeTransparency(_spriteAlpha);
            _spriteAlpha -= Time.deltaTime * 2.0f;
        }

        if (_hitCoolDown > 0.0f)
        {
            _hitCoolDown -= Time.deltaTime;
        }
        // ���������� ���������� ���������� ���������� //
    }

    // ����؂�ւ�
    // ���������� ���������� ���������� ���������� //
    public void SwapWeapon(WeaponList[] wepon, int no)
    {
        // �o�O���Ă��狭���I��0��˂�����
        if (no + 1 > wepon.Length) { no = 0; }


        // Tags
        // ���������� ���������� ���������� ���������� //
        // �e�L�X�g�ύX
        //weponNameText.text = wepon[no].name;

        // �X�v���C�g�؂�ւ��̂��߃p�X
        Sprite inImage = Resources.Load<Sprite>(wepon[no].trail);
        _spriteChanger.ChangeSprite(inImage, wepon[no].offset);

        // �����ɃA�C�R�����ǉ����邩��
        // (Empty)


        // Status
        // ���������� ���������� ���������� ���������� //
        // �ő�_���[�W
        _damage = wepon[no].damage;

        // ��b�m�b�N�o�b�N��
        //defKnockBack = wepon[no].defknockback;

        // �ő�m�b�N�o�b�N��
        _knockBack = wepon[no].maxknockback;

        // �ő�`���[�W��
        //maxCharge = wepon[no].maxcharge;

        // �X�^���l
        _stanPower = wepon[no].stanpower;


        // Sprites
        // ���������� ���������� ���������� ���������� //
        // �X�P�[��
        transform.localScale = new Vector3(
            wepon[no].wideth, wepon[no].height, 1.0f);

        // ���W
        transform.localPosition = new Vector3(
            0.0f, wepon[no].offset, 0.0f);


        // UI
        _playerAttack._image_Wepon.sprite = Resources.Load<Sprite>(wepon[no].icon);


        // �v���C���[�ɕK�v�N�[���_�E����n���ă��^�[��
        //return wepon[no].maxcharge;
    }

    public bool Combo()
    {
        if (!_comboFlag) { return false; }

        _comboFlag = false;
        return true;
    }

    public void Attacking()
    {
        coll.enabled = true;
        nowAttakingTime = defAttackingTime;

        _spriteAlpha = 1.0f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            if (_hitCoolDown <= 0)
            {
                _playerCTRL.comboCount++;
                _comboFlag = true;
                _hitCoolDown = defAttackingTime;
            }

            collision.gameObject.GetComponent<EnemyCTRL>().TakeDamage(
                _damage + _playerCTRL.comboCount,
                _knockBack,
                _stanPower);
        }
    }
}
