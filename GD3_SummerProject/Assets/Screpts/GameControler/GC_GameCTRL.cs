using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class GC_GameCTRL : MonoBehaviour
{
    [Header("BPM�R���g���[��")]
    [SerializeField] GC_BpmCTRL bpmCtrl;
    [Header("�v���C���[���")]
    [SerializeField] PlayerCTRL playerCtrl;
    [Header("�G���A���")]
    [SerializeField] GameObject areas;
    //[Header("�G���A���(�����擾)")]
    //[SerializeField] public AreaCTRL areaCtrl;
    [Header("UI")]
    [SerializeField] GameObject uiPanel;
    [SerializeField] Text centerText;
    [SerializeField] Text underText;
    [Header("�|�[�Y���j���[")]
    [SerializeField] Canvas pauseCanvas;
    [Header("�X�^�[�g���p�^�C�}�[")]
    [SerializeField] float countDown;

    enum State
    {
        Ready,
        Play,
        GameOver,
        GameClear,
        Pause
    }
    State state;

    UIControls uiCtrl;

    private void Awake()
    {
        uiCtrl = new UIControls();
    }

    void Start()
    {
        // �G���A�R���g���[���擾
        //areaCtrl = areas.GetComponentInChildren<AreaCTRL>();

        // �|�[�YUI���B��
        pauseCanvas.enabled = false;

        // �X�e�[�g������
        S_Ready();
        // �̂��ɂ������邩�A�e�L�����̃X�e�[�g��M��悤�ɂ��邩�B
        // state = State.Ready;

        uiCtrl.Enable();
    }

    void Update()
    {
        switch (state)
        {
            case State.Ready:
                S_Ready_CountDown();
                break;

            case State.Play:
                if (uiCtrl.InGameUI.Pause.triggered) { S_Pause(); }
                if (playerCtrl.state == PlayerCTRL.State.Dead) { S_GameOver(); }
                break ;

            case State.GameOver:
                if (uiCtrl.InGameUI.Retry.triggered) { ReLoad(); }
                break ;

            case State.GameClear:
                if (uiCtrl.InGameUI.Retry.triggered) { ReturnTitle(); }
                break;

            case State.Pause:
                if (uiCtrl.InGameUI.Pause.triggered) { S_Pause_End(); }
                break ;
        }
    }



    // �X�e�[�g�p
    // ���������� ���������� ���������� ���������� ���������� //

    // ���f�B
    void S_Ready()
    {
        state = State.Ready;
        DoEnableFalse();
    }

    // �J�E���g�_�E��
    void S_Ready_CountDown()
    {
        if (countDown >= 0)
        {
            uiPanel.SetActive(true);
            centerText.text = "Ready...";
            underText.text = "";

            countDown -= Time.deltaTime;
        }
        else
        {
            uiPanel.SetActive(false);
            centerText.text = "";

            countDown = 0;

            S_Play();
        }
    }

    // �v���C
    void S_Play()
    {
        playerCtrl.state = PlayerCTRL.State.Alive;

        state = State.Play;

        DoEnableTrue();
    }

    // �Q�[���I�[�o�[
    void S_GameOver()
    {
        uiPanel.SetActive(true);
        centerText.text = "GameOver";
        underText.text = "[R] �L�[�Ń��X�^�[�g";

        state = State.GameOver;

        DoEnableFalse();
    }

    // �Q�[���N���A
    public void S_GameClear()
    {
        uiPanel.SetActive(true);
        centerText.text = "GameClear";
        underText.text = "[R] �L�[�Ń^�C�g����";

        state = State.GameClear;

        playerCtrl.state = PlayerCTRL.State.Stop;
        DoEnableFalse();
    }

    // �|�[�Y
    void S_Pause()
    {
        pauseCanvas.enabled = true;

        state = State.Pause;
        playerCtrl.state = PlayerCTRL.State.Stop;

        DoEnableFalse();
    }

    // �|�[�Y����
    void S_Pause_End()
    {
        pauseCanvas.enabled = false;

        playerCtrl.state = PlayerCTRL.State.Alive;

        S_Play();
    }

    // ���������� ���������� ���������� ���������� ���������� //



    // ���̑����� //
    // ���������� ���������� ���������� ���������� ���������� //

    // �����[�h
    void ReLoad()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }

    // �^�C�g���֖߂�
    void ReturnTitle()
    {
        SceneManager.LoadScene("TitleScene");
    }

    // �̂��ɏ���
    void DoEnableTrue()
    {
        bpmCtrl.enabled = true;
        //areaCtrl.enabled = true;
    }
    void DoEnableFalse()
    {
        bpmCtrl.enabled = false;
        //areaCtrl.enabled = false;
    }

    // ���������� ���������� ���������� ���������� ���������� //



    // �|�[�YUI�p
    // ���������� ���������� ���������� ���������� ���������� //

    // UI�F�Q�[���ɖ߂�
    public void Pause_Close()
    {
        S_Pause_End();
    }

    // UI�F���X�^�[�g
    public void Pause_RestartGame()
    {
        ReLoad();

    }

    // UI�F�^�C�g���֖߂�
    public void Pause_ReturnToTile()
    {
        ReturnTitle();
    }
    // ���������� ���������� ���������� ���������� ���������� //
}
