using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GC_BpmCTRL : MonoBehaviour
{
    // �p�u���b�N�ϐ�
    [Header("BPM")]
    public float bpm;   // �e���|

    // �L�����o�X
    [Header("�L�����o�X")]
    //public Text bpmText;    // BPM�\�L
    public Image beatImage;
    [SerializeField] Slider _beatSlider;

    // �I�[�f�B�I�֌W
    [Header("�I�[�f�B�I")]
    [SerializeField] AudioCTRL _audioCTRL;
    [SerializeField] AudioSource audioSource;   // �I�[�f�B�I�\�[�X
    [SerializeField] AudioClip[] audioClip;     // �N���b�v

    // �v���C�x�[�g�ϐ�
    private float timing = 0.0f;    // ���g���m�[���p
    private bool metronome = false; // ���g���m�[���V�O�i��
    private bool metronomeFlap = false;
    private bool doSignal = false;  // �V�O�i�����M�p
    private float nowImageSize = 0.6f;
    private float minImageSize = 0.6f;
    private float maxImageSize = 1.2f;
    private bool pause = false;



    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioClip = new AudioClip[_audioCTRL.clips.Length];
        audioClip = _audioCTRL.clips;
        beatImage.color = new Color(1.0f, 0.0f, 0.0f, 0.5f);
        BpmReset();
    }
    
    void Update()
    {
        if (pause)
        {
            BpmReset();
            return;
        }
        Counter();
    }


    void Counter()
    {
        // ���������� ���������� ���������� ���������� //
        // �J�E���^�[
        // ���������� ���������� ���������� ���������� //
        if (timing <= 0.05f)
        {
            doSignal = true;
        }

        if (timing <= 0.0f && metronomeFlap == false)
        {
            audioSource.PlayOneShot(audioClip[0]);
            nowImageSize = maxImageSize;
            metronome = true;
            metronomeFlap = true;
        }
        else
        {
            metronome = false;
        }

        if (timing <= -0.2f)
        {
            doSignal = false;
            metronomeFlap = false;
            BpmReset();
        }

        ImageShrinking();
        timing -= Time.deltaTime;
        // ���������� ���������� ���������� ���������� //
    }

    // BPM�X�V�p
    float BpmReset()
    {
        _beatSlider.maxValue = 60 / bpm;
        _beatSlider.minValue = -0.2f;

        return timing = 60 / bpm;
    }

    // �V�O�i�����M�֐�
    public bool Metronome()
    {
        return metronome;
    }
    public bool Signal()
    {
        return doSignal;
    }

    public void ChangePause(bool flag)
    {
        timing = 0;
        pause = flag;
    }

    void ImageShrinking()
    {
        if (nowImageSize > minImageSize){ nowImageSize -= Time.deltaTime * 4.0f; }
        else { nowImageSize = minImageSize; }

        _beatSlider.value = timing;

        beatImage.transform.localScale = new Vector3(nowImageSize, nowImageSize, 1.0f);
    }
}
