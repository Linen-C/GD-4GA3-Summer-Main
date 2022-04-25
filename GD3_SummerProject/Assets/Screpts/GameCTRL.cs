using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameCTRL : MonoBehaviour
{
    // Ï
    public float bpm;   // e|

    // LoX
    public Text bpmText;    // BPM\L
    public Text timingText; // ĵ^C~O\L
    public Text metronomeText;  // gm[VOi\Ĥ

    // è
    private float timing = 0.0f;    // gm[p
    private bool metronome = false; // gm[VOi
    private bool doSignal = false;  // VOiMp

    // R|[lg

    void Start()
    {
        BpmReset();
    }

    
    void Update()
    {
        //     //
        // JE^[
        //     //
        if (timing <= 0.2f)
        {
            doSignal = true;
            timingText.text = "true";
        }
        
        if (timing <= 0.0f)
        {
            metronome = true;
            metronomeText.text = "PULSE";
        }
        else
        {
            metronome = false;
            metronomeText.text = "";
        }

        if (timing <= -0.2f)
        {
            doSignal = false;
            BpmReset();
            timingText.text = "false";
        }

        timing -= Time.deltaTime;
        //     //

    }

    float BpmReset()
    {
        bpmText.text = "BPM:" + bpm;
        return timing = 60 / bpm;
    }


    // VOiMÖ

    public bool Metronome()
    {
        return metronome;
    }

    public bool SendSignal()
    {
        return doSignal;
    }

}
