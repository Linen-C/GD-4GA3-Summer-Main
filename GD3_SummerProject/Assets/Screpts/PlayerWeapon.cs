using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerWeapon : MonoBehaviour
{
    // �p�u���b�N�ϐ�
    public float defTime;   // �U������̔�������

    // �L�����p�X
    public Text weponNameText;  // ���햼�\���p

    // �v���C�x�[�g�ϐ�
    float time = 0.0f;

    public PlayerCTRL playerctrl;

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
        
        if (time > 0) { time -= Time.deltaTime; }
        else { coll.enabled = false; }

        // ���������� ���������� ���������� ���������� //
    }

    public int SwapoWeapon(WeponList wepon,int no)
    {
        // ���������� ���������� ���������� ���������� //
        // ����؂�ւ�
        // ���������� ���������� ���������� ���������� //
        if (no + 1 > wepon.weponList.Length)
        {
            transform.localPosition = new Vector3(
                0.0f, wepon.weponList[0].offset, 0.0f);

            transform.localScale = new Vector3(
                wepon.weponList[0].wideth, wepon.weponList[0].height, 1.0f);

            weponNameText.text = wepon.weponList[0].name;

            return wepon.weponList[0].cool;
        }


        transform.localPosition = new Vector3(
            0.0f, wepon.weponList[no].offset, 0.0f);

        transform.localScale = new Vector3(
            wepon.weponList[no].wideth, wepon.weponList[no].height, 1.0f);

        weponNameText.text = wepon.weponList[no].name;

        return wepon.weponList[no].cool;

        // ���������� ���������� ���������� ���������� //
    }

    public void Attacking()
    {
        coll.enabled = true;
        time = defTime;
        Debug.Log("���蔭��");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerctrl.GetCharge();
        Debug.Log("����");
    }
}
