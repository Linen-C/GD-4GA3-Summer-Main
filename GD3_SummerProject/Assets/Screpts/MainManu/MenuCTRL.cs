using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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

    void Awake()
    {
        //_Option.SetActive(false);
        _animator.SetBool("Main_Bool", true);
    }

    // Any���J�X�^�}�C�Y
    // ���������� ���������� ���������� ���������� ���������� //
    public void Any_to_Customize()
    {
        _menu_Customize.EnableMenu();
        _animator.SetBool("Custom_Bool", true);
    }

    // ���C�����Z���N�g
    // ���������� ���������� ���������� ���������� ���������� //
    public void Main_to_Select()
    {
        _animator.SetBool("Main_Bool", false);
        _animator.SetBool("Select_Bool", true);
    }


    // �Z���N�g�����C��
    // ���������� ���������� ���������� ���������� ���������� //
    public void Select_to_Main()
    {
        _animator.SetBool("Select_Bool", false);
        _animator.SetBool("Main_Bool", true);
    }

    // �Z���N�g�����C���Q�[��
    // ���������� ���������� ���������� ���������� ���������� //
    public void Select_to_StMG()
    {
        _stMG.SetWeaponImage();

        _animator.SetBool("Select_Bool", false);
        _animator.SetBool("StMG_Bool", true);
    }


    // ���C���Q�[�����Z���N�g
    // ���������� ���������� ���������� ���������� ���������� //
    public void StMG_to_Select()
    {
        _animator.SetBool("StMG_Bool", false);
        _animator.SetBool("Select_Bool", true);
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
