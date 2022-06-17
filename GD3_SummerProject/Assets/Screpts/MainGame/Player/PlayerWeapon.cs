using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerWeapon : MonoBehaviour
{
    // �ϐ�
    [Header("�ϐ�")]
    [SerializeField] float defTime;   // �U������̔�������
    [SerializeField] public float attakingTime = 0.0f;  // ����̔�������

    // �X�N���v�g
    [Header("�X�N���v�g")]
    [SerializeField] PlayerCTRL playerctrl;
    [SerializeField] SpriteChanger spriteChanger;

    // �L�����p�X
    [Header("�L�����o�X")]
    [SerializeField] Text weponNameText;  // ���햼�\���p

    // �v���C�x�[�g�ϐ�
    float spriteAlpha = 0.0f;
    float chargeCool = 0.0f;

    [SerializeField] int defDamage = 1;      // �ʏ�_���[�W
    [SerializeField] int maxDamage = 0;      // �ő�_���[�W
    [SerializeField] int defKnockBack = 0;   // �m�b�N�o�b�N�p���[
    [SerializeField] int maxKnockBack = 0;   // �m�b�N�o�b�N�p���[
    [SerializeField] int maxCharge = 0;      // �K�v�ő�`���[�W

    int nowDamage = 0;
    int nowKockBack = 0;
    bool comboFlag = false;

    // �R���|�[�l���g
    BoxCollider2D coll;


    void Start()
    {
        coll = GetComponent<BoxCollider2D>();
        coll.enabled = false;

        spriteChanger.ChangeTransparency(spriteAlpha);
    }

    void Update()
    {
        // ���蔭��
        // ���������� ���������� ���������� ���������� //
        if (attakingTime >= 0)
        {
            attakingTime -= Time.deltaTime;
        }
        else{ coll.enabled = false; }

        if (spriteAlpha > 0.0f)
        {
            spriteChanger.ChangeTransparency(spriteAlpha);
            spriteAlpha -= Time.deltaTime * 2.0f;
        }

        if (chargeCool >= 0.0f)
        {
            chargeCool -= Time.deltaTime;
        }
        // ���������� ���������� ���������� ���������� //
    }



    // ����؂�ւ�
    // ���������� ���������� ���������� ���������� //
    public int SwapWeapon(JsonData wepon,int no)
    {
        // �o�O���Ă��狭���I��0��˂�����
        if (no + 1 > wepon.weaponList.Length) { no = 0; }


        // �ő�_���[�W�Z�b�g
        maxDamage = wepon.weaponList[no].maxcharge;

        // ��b�m�b�N�o�b�N�ʃZ�b�g
        defKnockBack = wepon.weaponList[no].defknockback;

        // �ő�m�b�N�o�b�N�ʃZ�b�g
        maxKnockBack = wepon.weaponList[no].maxknockback;

        // �ő�`���[�W�ʃZ�b�g
        maxCharge = wepon.weaponList[no].maxcharge;


        // ���W�Z�b�g
        transform.localPosition = new Vector3(
            0.0f, wepon.weaponList[no].offset, 0.0f);

        // �X�P�[���Z�b�g
        transform.localScale = new Vector3(
            wepon.weaponList[no].wideth, wepon.weaponList[no].height, 1.0f);

        // �X�v���C�g�؂�ւ��̂��߃p�X
        Sprite inImage = Resources.Load<Sprite>(wepon.weaponList[no].image.ToString());
        spriteChanger.ChangeSprite(inImage, wepon.weaponList[no].offset);


        // �e�L�X�g�ύX
        weponNameText.text = wepon.weaponList[no].name;

        // �v���C���[�ɕK�v�N�[���_�E����n���ă��^�[��
        return wepon.weaponList[no].maxcharge;
    }

    // ���������� ���������� ���������� ���������� //



    // �U������
    // ���������� ���������� ���������� ���������� //
    public void Attacking(int nowCharge)
    {
        if (maxCharge == nowCharge)
        {
            nowDamage = maxDamage;
            nowKockBack = maxKnockBack;
        }
        else
        {
            nowDamage = defDamage;
            nowKockBack = defKnockBack;
        }

        coll.enabled = true;
        attakingTime = defTime;

        spriteAlpha = 1.0f;
    }

    // ���������� ���������� ���������� ���������� //


    public bool Combo()
    {
        if (!comboFlag) { return false; }

        comboFlag = false;
        return true;
    }


    // �U���̖�������
    // ���������� ���������� ���������� ���������� //
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy" && chargeCool <= 0)
        {
            collision.gameObject.GetComponent<EnemyCTRL>().TakeDamage(nowDamage, nowKockBack);
            playerctrl.GetCharge();
            if (nowDamage == maxDamage) { comboFlag = true; }
        }
    }

    // ���������� ���������� ���������� ���������� //
}
