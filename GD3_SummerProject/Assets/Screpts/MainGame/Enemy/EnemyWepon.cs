using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWepon : MonoBehaviour
{
    // �p�u���b�N�ϐ�
    [Header("�p�u���b�N�ϐ�")]
    [SerializeField] float defTime;   // �U������̔�������
    [SerializeField] public float attakingTime = 0.0f;  // ����̔�������

    // �X�N���v�g
    [Header("�X�N���v�g")]
    [SerializeField] EnemyCTRL enemyCTRL;
    [SerializeField] SpriteChanger spriteChanger;

    // �v���C�x�[�g�ϐ�
    float spriteAlpha = 0.0f;

    // �R���|�[�l���g
    BoxCollider2D coll;


    void Start()
    {
        coll = GetComponent<BoxCollider2D>();
        coll.enabled = false;
    }


    void Update()
    {
        // ���������� ���������� ���������� ���������� //
        // ���蔭��
        // ���������� ���������� ���������� ���������� //

        if (attakingTime > 0) { attakingTime -= Time.deltaTime; }
        else { coll.enabled = false; }

        if (spriteAlpha >= 0.0f)
        {
            spriteChanger.ChangeTransparency(spriteAlpha);
            spriteAlpha -= Time.deltaTime * 3.0f;
        }

        // ���������� ���������� ���������� ���������� //
    }

    public void Attacking()
    {
        coll.enabled = true;
        attakingTime = defTime;
        spriteAlpha = 1.0f;
        //spriteChanger.ChangeTransparency(1.0f);
        // Debug.Log("�G�l�~�[�F���蔭��");
    }
}
