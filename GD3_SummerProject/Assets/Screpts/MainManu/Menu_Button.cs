using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Menu_Button : MonoBehaviour
{
    [Header("���C�����j���[")]
    [SerializeField] Button _Button_mainmenu;
    [Header("�J�X�^�}�C�Y")]
    [SerializeField] Button _Button_custom;
    [Header("�X�e�[�W�Z���N�g")]
    [SerializeField] Button _Button_select;
    [Header("�X�^���o�C")]
    [SerializeField] Button _Button_standbyMG;
    [Header("�I�v�V����")]
    [SerializeField] Button _Button_option;


    void Awake()
    {
        var padName = Gamepad.current;
        if (padName == null)  { return; }

        B_MainMenu();
    }

    public void B_MainMenu()
    {
        var padName = Gamepad.current;
        if (padName == null) { return; }

        _Button_mainmenu.Select();
    }

    public void B_Custom()
    {
        var padName = Gamepad.current;
        if (padName == null) { return; }

        _Button_custom.Select();
    }

    public void B_select()
    {
        var padName = Gamepad.current;
        if (padName == null) { return; }

        _Button_select.Select();
    }

    public void B_StundbyMG()
    {
        var padName = Gamepad.current;
        if (padName == null) { return; }

        _Button_standbyMG.Select();
    }
    
    public void B_Option()
    {
        var padName = Gamepad.current;
        if (padName == null) { return; }

        _Button_option.Select();
    }

}
