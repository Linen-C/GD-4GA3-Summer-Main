using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleCTRL : MonoBehaviour
{
    public void UIM_ClockToGame()
    {
        SceneManager.LoadScene("DebugScene");
    }

    public void UIM_Shutdown()
    {
        Application.Quit();
    }
}
