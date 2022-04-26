using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameCTRL : MonoBehaviour
{
    // �ϐ�
    public float bpm;   // �e���|

    // �L�����o�X
    public Text bpmText;    // BPM�\�L
    public Text timingText; // ���^�C�~���O�\�L
    public Text metronomeText;  // ���g���m�[���V�O�i���\��

    // �萔
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
            timingText.text = "true";
        }

        if (timing <= 0.0f && metronomeFlap == false)
        {
            metronome = true;
            metronomeText.text = "PULSE";
            metronomeFlap = true;
        }
        else
        {
            metronome = false;
            if (metronomeFlap == false)
            {
                metronomeText.text = "";
            }
        }

        if (timing <= -0.2f)
        {
            doSignal = false;
            metronomeFlap = false;
            BpmReset();
            timingText.text = "false";
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
