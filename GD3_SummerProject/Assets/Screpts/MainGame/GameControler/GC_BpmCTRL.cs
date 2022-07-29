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
    [SerializeField] MainAudioCTRL _audioCTRL;
    [SerializeField] AudioSource audioSource;   // �I�[�f�B�I�\�[�X
    [SerializeField] AudioClip[] audioClip;     // �N���b�v

    [Header("�f�o�b�O")]
    [SerializeField] float _maxValue;
    [SerializeField] float _halfValue;
    [SerializeField] float _ping;
    [SerializeField] float _halfPing;

    // �v���C�x�[�g�ϐ�
    [SerializeField] private float _timing = 0.0f;    // ���g���m�[���p
    private bool _metronome = false; // ���g���m�[���V�O�i��
    private bool _metronomeFlap = false;
    private bool _halfMetronome = false;       // �P��
    private bool _stepFlip = false;   // �V�O�i������
    public bool _doSignal = false;   // �V�O�i�����M�p
    public bool _halfSignal = false;
    public bool _perfect = false;    // �p�[�t�F�N�g
    private bool _count = false;      // �J�E���g�p
    private float _nowImageSize = 0.6f;
    private float _minImageSize = 0.6f;
    private float _maxImageSize = 1.2f;
    private bool _pause = false;



    void Start()
    {
        // �I�[�f�B�I������
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = _audioCTRL.nowVolume;
        audioClip = new AudioClip[_audioCTRL.clips_BPM.Length];
        audioClip = _audioCTRL.clips_BPM;

        beatImage.color = new Color(1.0f, 0.0f, 0.0f, 0.5f);
        BpmReset();
    }
    
    void Update()
    {
        if (_pause)
        {
            BpmReset();
            return;
        }
        Counter();
    }

    private void FixedUpdate()
    {
        if (_pause) { return; }
        _timing -= Time.deltaTime;
    }


    void Counter()
    {
        // ���������� ���������� ���������� ���������� //
        // �J�E���^�[
        // ���������� ���������� ���������� ���������� //


        if (_timing <= (_halfValue + _ping) && _halfSignal == false)
        {
            _halfSignal = true;
            //_doSignal = true;
        }

        if (_timing <= _halfValue && _stepFlip == false)
        {
            audioSource.PlayOneShot(audioClip[0]);
            _nowImageSize = _maxImageSize;
            _halfMetronome = true;
            _stepFlip = true;
        }
        else 
        {
            _halfMetronome = false;
        }

        if (_timing <= (_halfValue + _halfPing))
        {
            _perfect = true;
        }

        if ( _timing <= _halfValue && _halfSignal == true)
        {
            //_doSignal = false;
            _halfSignal = false;
            _perfect = false;
        }

        // ���������� ���������� ���������� ���������� //

        if (_timing <= _ping)
        {
            _doSignal = true;
        }

        if (_timing <= _halfPing)
        {
            _perfect = true;
        }

        if (_timing <= 0.0f && _metronomeFlap == false)
        {
            audioSource.PlayOneShot(audioClip[0]);
            _nowImageSize = _maxImageSize;
            _metronome = true;
            _metronomeFlap = true;
        }
        else 
        {
            _metronome = false;
        }
        if (_timing <= 0.0f)
        {
            _doSignal = false;
            _metronomeFlap = false;
            _stepFlip = false;
            _perfect = false;

            _count = true;

            BpmReset();
        }
        else 
        {
            _count = false;
        }

        
        ImageShrinking();

        // ���������� ���������� ���������� ���������� //
    }

    // BPM�X�V�p
    float BpmReset()
    {
        _halfValue = float.Parse((60 / bpm).ToString("N4"));
        _maxValue = float.Parse((_halfValue * 2.0f).ToString("N4"));

        _beatSlider.maxValue = _maxValue;

        _ping = float.Parse((_maxValue * 0.18f).ToString("N4"));

        _halfPing = float.Parse((_ping * 0.4f).ToString("N4"));

        _beatSlider.minValue = 0;

        return _timing = _maxValue;
    }

    // �V�O�i�����M�֐�
    public bool Metronome()
    {
        return _metronome;
    }

    public bool Step()
    {
        return _halfMetronome;
    }

    public bool Signal()
    {
        return _doSignal;
    }

    public bool HalfSignal()
    {
        return _halfSignal;
    }

    public bool Perfect()
    {
        return _perfect;
    }

    public bool Count()
    {
        return _count;
    }

    public void ChangePause(bool flag)
    {
        _timing = 0;
        _pause = flag;
    }

    void ImageShrinking()
    {
        if (_nowImageSize > _minImageSize){ _nowImageSize -= Time.deltaTime * 3.0f; }
        else { _nowImageSize = _minImageSize; }

        beatImage.transform.localScale = new Vector3(_nowImageSize, _nowImageSize, 1.0f);


        _beatSlider.value = _timing;
    }

    public void VolumeUpdate(float volume)
    {
        audioSource.volume = volume;
    }
}
