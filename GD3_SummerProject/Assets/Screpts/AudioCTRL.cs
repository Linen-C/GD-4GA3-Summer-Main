using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioCTRL : MonoBehaviour
{
    [Header("クリップ")]
    [SerializeField] AudioClip testClip;
    [SerializeField] AudioClip[] clips;
    [Header("音量")]
    [Range(0,1)] public float defVolume;

    AudioSource audioSource;

    void Start()
    {
        //audioSource = GetComponent<AudioSource>();
        //audioSource.volume = defVolume;
    }

    
    void Update()
    {
        //audioSource.PlayOneShot(testClip);
    }
}
