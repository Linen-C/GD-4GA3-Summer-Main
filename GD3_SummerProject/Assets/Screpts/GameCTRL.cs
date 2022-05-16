using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameCTRL : MonoBehaviour
{
    // �p�u���b�N�ϐ�
    [Header("BPM")]
    public float bpm;   // �e���|

    // �L�����o�X
    [Header("�L�����o�X")]
    public Text bpmText;    // BPM�\�L
    public Image beatImage;

    // �v���C�x�[�g�ϐ�
    private float timing = 0.0f;    // ���g���m�[���p
    private bool metronome = false; // ���g���m�[���V�O�i��
    private bool metronomeFlap = false;
    private bool doSignal = false;  // �V�O�i�����M�p

    // �R���|�[�l���g

    void Start()
    {
        BpmReset();
    }

    
    void Update()
    {
        // ���������� ���������� ���������� ���������� //
        // �J�E���^�[
        // ���������� ���������� ���������� ���������� //
        if (timing <= 0.2f)
        {
            doSignal = true;
            beatImage.color = new Color(1.0f, 0.0f, 0.0f, 0.5f);
        }

        if (timing <= 0.0f && metronomeFlap == false)
        {
            metronome = true;
            metronomeFlap = true;
        }
        else
        {
            metronome = false;
            if (metronomeFlap == false)
            {
            }
        }

        if (timing <= -0.2f)
        {
            doSignal = false;
            metronomeFlap = false;
            BpmReset();
            beatImage.color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
        }

        timing -= Time.deltaTime;
        // ���������� ���������� ���������� ���������� //

    }

    float BpmReset()
    {
        bpmText.text = "BPM:" + bpm;
        return timing = 60 / bpm;
    }


    // �V�O�i�����M�֐�

    public bool Metronome()
    {
        return metronome;
    }

    public bool SendSignal()
    {
        return doSignal;
    }

}
