using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCURSOR : MonoBehaviour
{


    void Start()
    {
        
    }


    void Update()
    {
        // ���������� ���������� ���������� ���������� //
        // �J�[�\����]
        // ���������� ���������� ���������� ���������� //

        // �����̈ʒu
        Vector2 transPos = transform.position;
        //Debug.Log("tX" + transPos.x + "_" + "tY" + transPos.y);

        // �X�N���[�����W�n�̃}�E�X���W�����[���h���W�n�ɏC��
        Vector2 mouseRawPos = Input.mousePosition;
        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseRawPos);
        //Debug.Log("mX" + mouseWorldPos.x + "_"+ "mY" + mouseWorldPos.y);

        // �x�N�g�����v�Z
        Vector2 diff = (mouseWorldPos - transPos).normalized;

        // ��]�ɑ��
        transform.rotation = Quaternion.FromToRotation(Vector3.up, diff);
        // ���������� ���������� ���������� ���������� //

        // ���������� ���������� ���������� ���������� //
        // �U������
        // ���������� ���������� ���������� ���������� //

        /*
         *�P�j�U���V�O�i�����󂯎��
         *�Q�j��������󂯎��
         *�R�j�U���֐����Ăяo��
         *�S�j����������ɍU���𔭐�������
         */

        // ���������� ���������� ���������� ���������� //
    }
}
