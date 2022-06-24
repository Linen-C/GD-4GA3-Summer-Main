using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GC_BpmCTRL : MonoBehaviour
{
    // pubNÏ
    [Header("BPM")]
    public float bpm;   // e|

    // LoX
    [Header("LoX")]
    //public Text bpmText;    // BPM\L
    public Image beatImage;

    // I[fBIÖW
    [Header("I[fBI")]
    [SerializeField] AudioSource audioSource;   // I[fBI\[X
    [SerializeField] AudioClip[] audioClip;     // Nbv

    // vCx[gÏ
    private float timing = 0.0f;    // gm[p
    private bool metronome = false; // gm[VOi
    private bool metronomeFlap = false;
    private bool doSignal = false;  // VOiMp
    private float nowImageSize = 0.6f;
    private float minImageSize = 0.6f;
    private float maxImageSize = 1.2f;



    void Start()
    {
        beatImage.color = new Color(1.0f, 0.0f, 0.0f, 0.5f);
        BpmReset();
    }
    
    void Update()
    {
        Counter();
    }


    void Counter()
    {
        //     //
        // JE^[
        //     //
        if (timing <= 0.2f)
        {
            doSignal = true;
            nowImageSize = maxImageSize;
            //beatImage.transform.localScale = new Vector3(1.2f, 1.2f, 1.0f);
        }

        if (timing <= 0.0f && metronomeFlap == false)
        {
            //audioSource.PlayOneShot(audioClip[0]);
            metronome = true;
            metronomeFlap = true;
        }
        else
        {
            metronome = false;
            if (metronomeFlap == false)
            {
               //beatImage.transform.localScale = new Vector3(0.8f, 0.8f, 1.0f);
            }
        }

        if (timing <= -0.2f)
        {
            doSignal = false;
            metronomeFlap = false;
            BpmReset();
        }

        ImageShrinking();

        timing -= Time.deltaTime;
        //     //
    }

    // BPMXVp
    float BpmReset()
    {
        //bpmText.text = "BPM:" + bpm;
        return timing = 60 / bpm;
    }

    // VOiMÖ
    public bool Metronome()
    {
        return metronome;
    }
    public bool Signal()
    {
        return doSignal;
    }


    void ImageShrinking()
    {
        if (nowImageSize > minImageSize){ nowImageSize -= Time.deltaTime * 4.0f; }
        else { nowImageSize = minImageSize; }

        beatImage.transform.localScale = new Vector3(nowImageSize, nowImageSize, 1.0f);
    }
}
