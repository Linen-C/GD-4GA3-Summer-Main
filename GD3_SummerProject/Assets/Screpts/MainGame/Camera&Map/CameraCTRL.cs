using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCTRL : MonoBehaviour
{
    // �Q�[���I�u�W�F�N�g
    [Header("�v���C���[�g�����X�t�H�[��")]
    [SerializeField] Transform playerTr;
    [Header("�J���������ʒu")]
    [SerializeField] Vector3 cameraDefPos;         // �J���������ʒu
    [Header("�J�������E�ʒu�F�E��")]
    [SerializeField] public Vector2 cameraMaxPos;  // �J�����E����E�ʒu
    [Header("�J�������E�ʒu�F����")]
    [SerializeField] public Vector2 cameraMinPos;  // �J�����������E�ʒu
    [Header("�J�����J�ڗp�̒��S�ʒu")]
    [SerializeField] public Vector3 cameraCenter;
    [Header("�g�����W�V����")]
    [SerializeField] public Vector2 cameraNextMaxPos;
    [SerializeField] public Vector2 cameraNextMinPos;

    public enum State
    {
        Nomal,
        Trans
    }
    State _state;

    private void Start()
    {
        _state = State.Nomal;
    }

    void LateUpdate()
    {
        if (_state == State.Nomal)
        {
            Camera_Normal();
        }

        if (_state == State.Trans)
        {
            Camera_Trans();
        }

    }


    void Camera_Normal()
    {
        // ���炩�Ƀv���C���[�֒Ǐ]
        Vector3 camPos = Vector3.Lerp(
            transform.position,
            playerTr.position + cameraDefPos,
            3.0f * Time.deltaTime);

        // �J�����ʒu����
        camPos.x = Mathf.Clamp(camPos.x, cameraMinPos.x, cameraMaxPos.x);
        camPos.y = Mathf.Clamp(camPos.y, cameraMinPos.y, cameraMaxPos.y);
        camPos.z = cameraDefPos.z;

        // �ʒu���
        transform.position = camPos;
    }

    void Camera_Trans()
    {
        Vector3 camPos = Vector3.Lerp(
            transform.position,
            cameraCenter,
            5.0f * Time.deltaTime);

        transform.position = camPos;

        if (Mathf.Abs(cameraCenter.y - transform.position.y) < 0.1f)
        {
            cameraMaxPos = cameraNextMaxPos;
            cameraMinPos = cameraNextMinPos;

            cameraNextMaxPos = new Vector2(0, 0);
            cameraNextMinPos = new Vector2(0, 0);

            _state = State.Nomal;
        }
    }

    public void SetNewCamPoint(Vector2 maxPin,Vector2 minPin,bool downToUp)
    {
        if (cameraMaxPos == maxPin && cameraMinPos == minPin) { return; }

        _state = State.Trans;

        cameraNextMaxPos = maxPin;
        cameraNextMinPos = minPin;

        //Debug.Log("Max" + cameraNextMaxPos);
        //Debug.Log("Min" + cameraNextMinPos);

        if (downToUp)
        {
            cameraCenter = new Vector3(0.0f, cameraNextMinPos.y, -10.0f);
        }
        else
        {
            cameraCenter = new Vector3(0.0f, cameraNextMaxPos.y, -10.0f);
        }
    }
}
