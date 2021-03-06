using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using TMPro;

public class GC_GameCTRL : MonoBehaviour
{
    [Header("BPMコントロール")]
    [SerializeField] GC_BpmCTRL bpmCtrl;

    [Header("プレイヤー情報")]
    [SerializeField] PlayerCTRL playerCtrl;

    [Header("エリア情報")]
    [SerializeField] StageManager _stage;

    [Header("UI")]
    [SerializeField] GameObject upperPanel;
    [SerializeField] TextMeshProUGUI centerText;
    [SerializeField] TextMeshProUGUI underText;
    [SerializeField] public TextMeshProUGUI prog_text;

    [Header("ポーズメニュー")]
    [SerializeField] Canvas pauseCanvas;

    [Header("オプション")]
    [SerializeField] Option _option;
    [SerializeField] GameObject _firstButton;

    [Header("スタート時用タイマー")]
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
        // ポーズUIを隠す
        pauseCanvas.enabled = false;

        // ステート初期化
        S_Ready();
        // のちにこうするか、各キャラのステートを弄るようにするか。
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
                if (uiCtrl.InGameUI.Retry.triggered) { ReturnMainMenu(); }
                break;

            case State.Pause:
                if (uiCtrl.InGameUI.Pause.triggered) { S_Pause_End(); }
                break ;
        }
    }



    // ステート用
    // ＝＝＝＝＝ ＝＝＝＝＝ ＝＝＝＝＝ ＝＝＝＝＝ ＝＝＝＝＝ //

    // レディ
    void S_Ready()
    {
        state = State.Ready;
        DoEnableFalse();
    }

    // カウントダウン
    void S_Ready_CountDown()
    {
        if (countDown >= 0)
        {
            upperPanel.SetActive(true);
            centerText.text = "Ready...";
            underText.text = "";

            countDown -= Time.deltaTime;
        }
        else
        {
            upperPanel.SetActive(false);
            centerText.text = "";

            countDown = 0;

            S_Play();
        }
    }

    // プレイ
    void S_Play()
    {
        playerCtrl.state = PlayerCTRL.State.Alive;

        state = State.Play;

        DoEnableTrue();
    }

    // ゲームオーバー
    void S_GameOver()
    {
        upperPanel.SetActive(true);
        centerText.text = "GameOver";
        underText.text = "[R] キーでリスタート";

        state = State.GameOver;

        DoEnableFalse();
    }

    // ゲームクリア
    public void S_GameClear()
    {
        upperPanel.SetActive(true);
        centerText.text = "GameClear";
        underText.text = "[R] キーでタイトルへ";

        state = State.GameClear;

        playerCtrl.state = PlayerCTRL.State.Stop;
        DoEnableFalse();
    }

    // ポーズ
    void S_Pause()
    {
        pauseCanvas.enabled = true;

        state = State.Pause;
        playerCtrl.state = PlayerCTRL.State.Stop;

        var padName = Gamepad.current;
        if (padName != null)
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(_firstButton);
        }

        DoEnableFalse();
    }

    // ポーズ解除
    void S_Pause_End()
    {
        pauseCanvas.enabled = false;

        playerCtrl.state = PlayerCTRL.State.Alive;

        S_Play();
    }



    // その他処理 //
    // ＝＝＝＝＝ ＝＝＝＝＝ ＝＝＝＝＝ ＝＝＝＝＝ ＝＝＝＝＝ //

    // リロード
    void ReLoad()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }

    // メインメニューへ戻る
    void ReturnMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    // のちに消す
    void DoEnableTrue()
    {
        bpmCtrl.enabled = true;
        _stage.enabled = true;
    }
    void DoEnableFalse()
    {
        bpmCtrl.enabled = false;
        _stage.enabled = false;
    }



    // ポーズUI用
    // ＝＝＝＝＝ ＝＝＝＝＝ ＝＝＝＝＝ ＝＝＝＝＝ ＝＝＝＝＝ //

    // UI：ゲームに戻る
    public void Pause_Close()
    {
        S_Pause_End();
    }

    // UI：リスタート
    public void Pause_RestartGame()
    {
        ReLoad();
    }

    // UI：タイトルへ戻る
    public void Pause_ReturnToMainMenu()
    {
        ReturnMainMenu();
    }

    public void Pause_Option()
    {
        EventSystem.current.SetSelectedGameObject(null);
        Instantiate(_option);
    }
}
