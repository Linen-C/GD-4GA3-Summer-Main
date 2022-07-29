using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MenuCTRL : MonoBehaviour
{
    [Header("�e���j���[���")]
    [SerializeField] GameObject _Main; 
    [SerializeField] GameObject _Customize;
    [SerializeField] Menu_Customize _menu_Customize;
    [SerializeField] GameObject _Select; 
    [SerializeField] GameObject _StandbyMaingame;
    [SerializeField] Menu_StundbyMaingame _stMG;
    [SerializeField] GameObject _Option;

    [Header("�A�j���[�^�[")]
    [SerializeField] Animator _animator;

    [Header("�{�^��")]
    [SerializeField] Menu_Button _menu_Button;

    [Header("�I�[�f�B�I")]
    [SerializeField] MenuAudioCTRL _menu_AudioCTRL;  // �I�[�f�B�I�R���g���[��
    [SerializeField] AudioSource audioSource;        // �I�[�f�B�I�\�[�X
    [SerializeField] AudioClip[] audioClip;          // �N���b�v


    void Start()
    {
        // �I�[�f�B�I������
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = _menu_AudioCTRL.nowVolume;

        // ����ȋ�ł�������
        //audioSource.PlayOneShot(_menu_AudioCTRL._clips[0]);
    }

        void Awake()
    {
        _menu_Button.B_MainMenu();

        _animator.SetBool("Main_Bool", true);
    }

    // Any���J�X�^�}�C�Y
    // ���������� ���������� ���������� ���������� ���������� //
    public void Any_to_Customize()
    {
        _menu_Customize.EnableMenu();
        _animator.SetBool("Custom_Bool", true);

        _menu_Button.B_Custom();
    }

    // ���C�����Z���N�g
    // ���������� ���������� ���������� ���������� ���������� //
    public void Main_to_Select()
    {
        _menu_Button.B_select();
        _animator.SetBool("Main_Bool", false);
        _animator.SetBool("Select_Bool", true);
    }


    // �Z���N�g�����C��
    // ���������� ���������� ���������� ���������� ���������� //
    public void Select_to_Main()
    {
        _animator.SetBool("Select_Bool", false);
        _animator.SetBool("Main_Bool", true);
        _menu_Button.B_MainMenu();
    }

    // �Z���N�g�����C���Q�[��
    // ���������� ���������� ���������� ���������� ���������� //
    public void Select_to_StMG()
    {
        _stMG.Select_StMG();

        _animator.SetBool("Select_Bool", false);
        _animator.SetBool("StMG_Bool", true);

        _menu_Button.B_StundbyMG();
    }


    // ���C���Q�[�����Z���N�g
    // ���������� ���������� ���������� ���������� ���������� //
    public void StMG_to_Select()
    {
        _animator.SetBool("StMG_Bool", false);
        _animator.SetBool("Select_Bool", true);

        _menu_Button.B_select();
    }


    // �^�C�g����ʂ�
    // ���������� ���������� ���������� ���������� ���������� //
    public void Main_to_Title()
    {
        SceneManager.LoadScene("TitleScene");
    }


    // �I�v�V�������j���[�N��
    // ���������� ���������� ���������� ���������� ���������� //
    public void OptionEnable()
    {
        EventSystem.current.SetSelectedGameObject(null);
        Instantiate(_Option);
    }
}
