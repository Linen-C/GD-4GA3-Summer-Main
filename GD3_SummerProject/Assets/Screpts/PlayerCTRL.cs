using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCTRL : MonoBehaviour
{
    // �ϐ�
    public float moveSpeed;        // �ړ����x
    public GameCTRL gameCTRL;      // �Q�[���R���g���[���[
    public int needWeponCharge;   // �N�[���_�E����

    // �L�����p�X
    public Text cooldownText;   // �N�[���_�E���\���p

    // �萔
    private int weponCharge = 1;      // �N�[���_�E����
    private bool coolDownReset = false; // �N�[���_�E���̃��Z�b�g�t���O

    // �R���|�[�l���g
    Rigidbody2D body;


    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        // ���������� ���������� ���������� ���������� //
        // �ړ�
        // ���������� ���������� ���������� ���������� //
        body.velocity = new Vector2(
            Input.GetAxis("Horizontal") * moveSpeed, Input.GetAxis("Vertical") * moveSpeed);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("doge");
        }

        // ���������� ���������� ���������� ���������� //

        // ���������� ���������� ���������� ���������� //
        // �U���E�N�[���_�E��
        // ���������� ���������� ���������� ���������� //
        if (Input.GetMouseButtonDown(0) && weponCharge == needWeponCharge && gameCTRL.SendSignal())
        {
            Debug.Log("ATTACK");
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
        cooldownText.text = "COOL:" + weponCharge;
        // ���������� ���������� ���������� ���������� //

    }

}
