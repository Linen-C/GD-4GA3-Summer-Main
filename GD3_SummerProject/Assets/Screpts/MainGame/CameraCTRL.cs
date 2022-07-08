using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCTRL : MonoBehaviour
{
    // �Q�[���I�u�W�F�N�g
    [Header("�������W")]
    [SerializeField] Vector3 startPos;
    [SerializeField] Vector2 startMax;
    [SerializeField] Vector2 startMin;
    [Header("�v���C���[�g�����X�t�H�[��")]
    [SerializeField] Transform playerTr;
    [Header("�J���������ʒu")]
    [SerializeField] Vector3 cameraDefPos;         // �J�������S�ʒu
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

    private void Awake()
    {
        transform.position = startPos;
        
        cameraMaxPos = startMax;
        cameraMinPos = startMin;

        cameraNextMaxPos = startMax;
        cameraNextMinPos = startMin;
        
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
            10.0f * Time.deltaTime);

        camPos.x = Mathf.Clamp(camPos.x, cameraNextMinPos.x, cameraNextMaxPos.x);

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
        if (cameraNextMaxPos == maxPin && cameraNextMinPos == minPin) { return; }

        _state = State.Trans;

        cameraNextMaxPos = maxPin;
        cameraNextMinPos = minPin;

        //Debug.Log("Max" + maxPin);
        //Debug.Log("Min" + minPin);

        float nextCenter = cameraNextMaxPos.x - (-cameraNextMinPos.x);
        //Debug.Log(nextCenter);

        if (downToUp)
        {
            cameraCenter = new Vector3(nextCenter, cameraNextMinPos.y, -10.0f);
        }
        else
        {
            cameraCenter = new Vector3(nextCenter, cameraNextMaxPos.y, -10.0f);
        }
    }

    public void SetNewCenter(Vector2 center,Vector2 minPin, Vector2 maxPin)
    {
        if (cameraNextMinPos == minPin) { return; }

        _state = State.Trans;
        cameraNextMinPos = minPin;
        cameraNextMaxPos = maxPin;
        cameraCenter = new Vector3(center.x, center.y, -10);
    }
}
