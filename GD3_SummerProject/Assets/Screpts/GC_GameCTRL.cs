using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GC_GameCTRL : MonoBehaviour
{
    [Header("BPM�R���g���[��")]
    [SerializeField] GC_BpmCTRL bpmCtrl;
    [Header("�v���C���[���")]
    [SerializeField] PlayerCTRL playerCtrl;
    [Header("�G���A���")]
    [SerializeField] GameObject areas;
    [SerializeField] public AreaCTRL areaCtrl;

    enum State
    {
        Ready,
        Play,
        GameOver,
    }
    State state;

    void Start()
    {
        areaCtrl = areas.GetComponentInChildren<AreaCTRL>();

        S_Ready();
        Debug.Log("Ready");
    }


    void Update()
    {
        switch (state)
        {
            case State.Ready:
                if (Input.GetKeyDown(KeyCode.H)) { S_Play(); Debug.Log("Play"); }
                break;
            case State.Play:
                if (playerCtrl.IfIsDead()) { S_GameOver(); Debug.Log("GameOver"); }
                break ;
            case State.GameOver:
                if (Input.GetKeyDown(KeyCode.H)) { ReLoad(); }
                    break ;
        }
    }

    void S_Ready()
    {
        state = State.Ready;

        playerCtrl.enabled = false;
        DoEnableFalse();
    }

    void S_Play()
    {
        state = State.Play;

        playerCtrl.enabled = true;
        DoEnableTrue();
    }

    void S_GameOver()
    {
        state = State.GameOver;

        DoEnableFalse();
    }

    void ReLoad()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
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

}
