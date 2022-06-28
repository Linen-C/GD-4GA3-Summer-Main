using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuCTRL : MonoBehaviour
{
    [SerializeField] Canvas _Main; 
    [SerializeField] Canvas _Customize; 
    [SerializeField] Canvas _Select; 
    [SerializeField] Canvas _Standby;
    [SerializeField] Canvas _Option;

    void Awake()
    {
        _Customize.enabled = false;
        _Select.enabled = false;
        _Standby.enabled = false;
        _Option.enabled = false;
    }


    // �J�X�^�}�C�Y��ʂ�
    // ���������� ���������� ���������� ���������� ���������� //
    public void Main_to_Customize()
    {
        _Main.enabled = false;
        _Customize.enabled = true;
    }


    // �Z���N�g��ʂ�
    // ���������� ���������� ���������� ���������� ���������� //
    public void Main_to_Select()
    {
        _Main.enabled = false;
        _Select.enabled = true;
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
        _Main.enabled = false;
        _Option.enabled = true;
    }

}
