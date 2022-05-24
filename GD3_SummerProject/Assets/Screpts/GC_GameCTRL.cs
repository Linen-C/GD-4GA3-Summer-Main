using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GC_GameCTRL : MonoBehaviour
{
    [Header("BPM�R���g���[��")]
    [SerializeField] GC_BpmCTRL bpmCtrl;
    [Header("�v���C���[���")]
    [SerializeField] PlayerCTRL playerCtrl;
    [Header("�G���A���")]
    [SerializeField] GameObject areas;
    [SerializeField] public AreaCTRL areaCtrl;
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


    void Start()
    {
        areaCtrl = areas.GetComponentInChildren<AreaCTRL>();

        pauseCanvas.enabled = false;

        S_Ready();
        Debug.Log("Ready");
    }

    void Update()
    {
        switch (state)
        {
            case State.Ready:
                S_Ready_CountDown();
                break;

            case State.Play:
                if (Input.GetKeyDown(KeyCode.T)) { S_Play_OnPause(); }
                if (playerCtrl.IfIsDead()) { S_GameOver(); }
                break ;

            case State.GameOver:
                if (Input.GetKeyDown(KeyCode.R)) { ReLoad(); }
                break ;

            case State.GameClear:
                if (Input.GetKeyDown(KeyCode.R)) { ReturnTitle(); }
                break;

            case State.Pause:
                if (Input.GetKeyDown(KeyCode.T)) { S_Pause_End(); }
                break ;
        }
    }

    // ���������� ���������� ���������� ���������� ���������� //
    // �X�e�[�g�p
    // ���������� ���������� ���������� ���������� ���������� //

    // ���f�B
    void S_Ready()
    {
        state = State.Ready;

        playerCtrl.enabled = false;
        DoEnableFalse();
    }
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
        state = State.Play;

        playerCtrl.enabled = true;
        DoEnableTrue();
    }
    void S_Play_OnPause()
    {
        playerCtrl.StopUpdate();
        pauseCanvas.enabled = true;

        S_Pause();
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

        playerCtrl.StopUpdate();
        playerCtrl.enabled = false;
        DoEnableFalse();
    }

    // �|�[�Y
    void S_Pause()
    {
        state = State.Pause;

        playerCtrl.enabled = false;
        DoEnableFalse();
    }
    void S_Pause_End()
    {
        pauseCanvas.enabled = false;
        S_Play();
    }

    // ���������� ���������� ���������� ���������� ���������� //

    // ���������� ���������� ���������� ���������� ���������� //
    // ���̑����� //
    // ���������� ���������� ���������� ���������� ���������� //
    void ReLoad()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }

    void ReturnTitle()
    {
        SceneManager.LoadScene("TitleScene");
    }

    void DoEnableTrue()
    {
        bpmCtrl.enabled = true;
        areaCtrl.enabled = true;
    }
    void DoEnableFalse()
    {
        bpmCtrl.enabled = false;
        areaCtrl.enabled = false;
    }
    // ���������� ���������� ���������� ���������� ���������� //

    // ���������� ���������� ���������� ���������� ���������� //
    // �|�[�YUI�p
    // ���������� ���������� ���������� ���������� ���������� //
    public void Pause_Close()
    {
        S_Pause_End();
    }

    public void Pause_RestartGame()
    {
        ReLoad();

    }
    public void Pause_ReturnToTile()
    {
        ReturnTitle();
    }
    // ���������� ���������� ���������� ���������� ���������� //
}
