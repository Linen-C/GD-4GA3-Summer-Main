using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    // �p�u���b�N�ϐ�
    public float defTime;   // �U������̔�������

    // �v���C�x�[�g�ϐ�
    float time = 0.0f;

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

    public int SwapoWeapon()
    {
        // ���������� ���������� ���������� ���������� //
        // ����؂�ւ�
        // ���������� ���������� ���������� ���������� //

        /*
        if (Input.GetMouseButtonDown(1))
        {
            transform.localPosition = new Vector3(0.0f, 3.0f, 0.0f);
            transform.localScale = new Vector3(1.0f, 3.0f, 1.0f);
        }
        */

        transform.localPosition = new Vector3(0.0f, 3.0f, 0.0f);
        transform.localScale = new Vector3(1.0f, 3.0f, 1.0f);

        return 2;

        // ���������� ���������� ���������� ���������� //
    }

    public void Attacking()
    {
        coll.enabled = true;
        time = defTime;
        Debug.Log("���蔭��");
    }
}
