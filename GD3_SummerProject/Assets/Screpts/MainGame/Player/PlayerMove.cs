using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("PlayerCTRL")]
    [SerializeField] PlayerCTRL _plCTRL;

    [Header("�ړ�")]
    [SerializeField] float moveSpeed;  // �ړ����x
    [SerializeField] float dashPower;  // �_�b�V���p���[

    [Header("�m�b�N�o�b�N����")]
    [SerializeField] float knockBackPower;    // ������m�b�N�o�b�N�̋���

    private float dogeTimer = 0;         // ���p�̃^�C�}�[


    public void Move(Vector2 moveDir, Rigidbody2D body)
    {
        // �ړ�
        if (_plCTRL.knockBackCounter > 0.0f)
        {
            KnockBack(body);

            _plCTRL.knockBackCounter -= Time.deltaTime;
        }
        else
        {
            body.velocity = new Vector2(
                moveDir.x * moveSpeed,
                moveDir.y * moveSpeed);
        }

        // ���
        if (dogeTimer > 0.0f)
        {
            body.AddForce(new Vector2(
                moveDir.x * dashPower,
                moveDir.y * dashPower),
                ForceMode2D.Impulse);

            dogeTimer -= Time.deltaTime;
        }
    }

    void KnockBack(Rigidbody2D body)
    {
        var diff = FetchNearObjectWithTag("Enemy");

        body.AddForce(new Vector2(
                -diff.x * knockBackPower,
                -diff.y * knockBackPower),
                ForceMode2D.Impulse);
    }

    Vector2 FetchNearObjectWithTag(string tagName)
    {
        GameObject nearEnemy = null;

        var targets = GameObject.FindGameObjectsWithTag(tagName);
        var minTargetDist = float.MaxValue;

        if (targets == null) { return new Vector2(0, 0); }

        foreach (var target in targets)
        {
            var targetDist = Vector2.Distance(
                transform.position,
                target.transform.position);

            if (!(targetDist < minTargetDist)) { continue; }

            minTargetDist = targetDist;
            nearEnemy = target.transform.gameObject;
        }


        // �����̈ʒu
        Vector2 transPos = transform.position;

        // �ł��߂����W
        Vector2 enemyPos = nearEnemy.transform.position;

        // �x�N�g�����v�Z
        Vector2 diff = (enemyPos - transPos).normalized;

        return diff;
    }


    public void Dash(PlayerControls playerControls, GC_BpmCTRL bpmCTRL)
    {
        if (playerControls.Player.Dash.triggered &&
            bpmCTRL.Signal())
        {
            dogeTimer = 0.1f;
        }
    }
}