using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioCTRL : MonoBehaviour
{
    [Header("クリップ")]
    [SerializeField]public AudioClip[] clips;
    [Header("音量")]
    [Range(0,1)] public float defVolume = 0.5f;

    AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        audioSource.volume = defVolume;
    }
}
