using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCTRL : MonoBehaviour
{
    // �ϐ�
    public float moveSpeed;     // �ړ����x
    public GameCTRL gameCTRL;   // �Q�[���R���g���[���[
    public int defWeponCooldown;   // �N�[���_�E����

    // �L�����p�X
    public Text cooldownText;   // �N�[���_�E���\���p

    // �萔
    private float cashTime = 0; // ��s���͗p�̃L���b�V���^�C��
    private int weponCooldown = 0;  // �N�[���_�E����

    // �R���|�[�l���g
    Rigidbody2D body;


    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        weponCooldown = defWeponCooldown;
    }


    void Update()
    {
        // �ړ�
        body.velocity = new Vector2(
            Input.GetAxis("Horizontal") * moveSpeed, Input.GetAxis("Vertical") * moveSpeed);


        if (gameCTRL.Metronome() && weponCooldown > 0)
        {
            weponCooldown--;
        }
        cooldownText.text = "COOL:" + weponCooldown;


        if (gameCTRL.SendSignal())
        {
            if (Input.GetMouseButtonDown(0))
            {
                cashTime = 0.2f;
            }
        }

        if (weponCooldown == 0 && gameCTRL.Metronome())
        {
            if (cashTime >= 0.0f)
            {
                Debug.Log("ATTACK");
                weponCooldown = defWeponCooldown;
            }
        }

        if (cashTime >= 0.0f) { cashTime -= Time.deltaTime; }
    }
}
