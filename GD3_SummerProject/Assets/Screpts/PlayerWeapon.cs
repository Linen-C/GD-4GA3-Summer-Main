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
    [SerializeField] float defChargeCool;

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

    // �R���|�[�l���g
    BoxCollider2D coll;


    void Start()
    {
        coll = GetComponent<BoxCollider2D>();
        coll.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        // ���������� ���������� ���������� ���������� //
        // ���蔭��
        // ���������� ���������� ���������� ���������� //
        
        if (attakingTime >= 0){ attakingTime -= Time.deltaTime; }
        else{ coll.enabled = false; }

        if (spriteAlpha >= 0.0f)
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

    public int SwapWeapon(WeponList wepon,int no)
    {
        // ���������� ���������� ���������� ���������� //
        // ����؂�ւ�
        // ���������� ���������� ���������� ���������� //
        //Debug.Log("����ύX");

        // �o�O���Ă����p�̏���
        if (no + 1 > wepon.weponList.Length)
        {
            // ���W�Z�b�g
            transform.localPosition = new Vector3(
                0.0f, wepon.weponList[0].offset, 0.0f);

            // �X�P�[���Z�b�g
            transform.localScale = new Vector3(
                wepon.weponList[0].wideth, wepon.weponList[0].height, 1.0f);

            // �X�v���C�g�؂�ւ��̂��߃p�X
            Sprite defImage = Resources.Load<Sprite>(wepon.weponList[0].image.ToString());
            spriteChanger.ChangeSprite(defImage, wepon.weponList[0].offset);

            // �e�L�X�g�ύX
            weponNameText.text = wepon.weponList[0].name;

            // �v���C���[�ɕK�v�N�[���_�E����n���ă��^�[��
            return wepon.weponList[0].cool;
        }


        // ���W�Z�b�g
        transform.localPosition = new Vector3(
            0.0f, wepon.weponList[no].offset, 0.0f);

        // �X�P�[���Z�b�g
        transform.localScale = new Vector3(
            wepon.weponList[no].wideth, wepon.weponList[no].height, 1.0f);

        // �X�v���C�g�؂�ւ��̂��߃p�X
        Sprite inImage = Resources.Load<Sprite>(wepon.weponList[no].image.ToString());
        spriteChanger.ChangeSprite(inImage, wepon.weponList[no].offset);

        // �e�L�X�g�ύX
        weponNameText.text = wepon.weponList[no].name;

        // �v���C���[�ɕK�v�N�[���_�E����n���ă��^�[��
        return wepon.weponList[no].cool;

        // ���������� ���������� ���������� ���������� //
    }

    public void Attacking()
    {
        coll.enabled = true;
        attakingTime = defTime;

        spriteAlpha = 1.0f;

        //spriteChanger.ChangeTransparency(1.0f);
        //Debug.Log("�v���C���[�F���蔭��");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy" && chargeCool <= 0)
        {
            playerctrl.GetCharge();
        }
        //Debug.Log("�v���C���[�F����");
    }
}
