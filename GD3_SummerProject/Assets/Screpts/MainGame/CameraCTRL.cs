using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCTRL : MonoBehaviour
{
    // �Q�[���I�u�W�F�N�g
    [Header("�������W")]
    [SerializeField] Vector3 startPos;
    [Header("�v���C���[�g�����X�t�H�[��")]
    [SerializeField] Transform playerTr;
    [Header("�J���������ʒu")]
    [SerializeField] Vector3 cameraDefPos;         // �J�������S�ʒu
    [Header("�J�������E�ʒu�F�E��")]
    [SerializeField] public Vector2 cameraMaxPos;  // �J�����E����E�ʒu
    [Header("�J�������E�ʒu�F����")]
    [SerializeField] public Vector2 cameraMinPos;  // �J�����������E�ʒu


    private void Awake()
    {
        transform.position = startPos;
    }

    void LateUpdate()
    {
        Camera_Normal();
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


    public void SetNewCamPoint(Vector2 maxPin, Vector2 minPin/*,bool downToUp*/)
    {
        if (cameraMaxPos == maxPin && cameraMinPos == minPin) { return; }

        //_state = State.Trans;

        cameraMaxPos = maxPin;
        cameraMinPos = minPin;
    }
}
