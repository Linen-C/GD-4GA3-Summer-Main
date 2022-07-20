using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_Select : MonoBehaviour
{
    [SerializeField] Canvas _Main;
    [SerializeField] Canvas _Select;
    [SerializeField] Canvas _StundbyMaingame;

    // �e�X�e�[�W��
    // ���������� ���������� ���������� ���������� ���������� //
    public void Select_MainGame()
    {
        //SceneManager.LoadScene("MainGame");

        _Select.enabled = false;
        _StundbyMaingame.enabled = true;
    }

    /*
    public void Select_Tutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }
    */

    // ���C�����j���[�֖߂�
    // ���������� ���������� ���������� ���������� ���������� //
    public void Select_to_Main()
    {
        _Select.enabled = false;
        _Main.enabled = true;
    }

}
