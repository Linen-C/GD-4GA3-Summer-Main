using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuCTRL : MonoBehaviour
{
    [Header("�e���j���[���")]
    [SerializeField] GameObject _Main; 
    [SerializeField] GameObject _Customize;
    [SerializeField] Menu_Customize _menu_Customize;
    [SerializeField] GameObject _Select; 
    [SerializeField] GameObject _StandbyMaingame;
    [SerializeField] GameObject _Option;

    [Header("�e�X�g")]
    [SerializeField] Animator _animator;

    [Header("�{�^��")]
    [SerializeField] Menu_Button _menu_Button;

    void Awake()
    {
        //_Customize.SetActive(false);
        //_Select.SetActive(false);
        //_StandbyMaingame.SetActive(false);
        _Option.SetActive(false);
    }

    // �J�X�^�}�C�Y��ʂ�
    // ���������� ���������� ���������� ���������� ���������� //
    public void Main_to_Customize()
    {
        //_Main.SetActive(false);
        //_menu_Customize.EnableMenu(_Main);

        //_menu_Button.B_Custom();
        //Debug.Log("Main��Custom");
    }


    // �Z���N�g��ʂ�
    // ���������� ���������� ���������� ���������� ���������� //
    public void Main_to_Select()
    {
        //_Main.enabled = false;
        //_Select.enabled = true;

        //_menu_Button.B_select();
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
        //_Main.enabled = false;
        //_Option.enabled = true;

        //_menu_Button.B_Option();
    }

}
