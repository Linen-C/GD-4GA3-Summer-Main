using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioCTRL : MonoBehaviour
{
    [Header("�N���b�v")]
    [SerializeField]public AudioClip[] clips_BPM;
    [SerializeField]public AudioClip[] clips_Progress;
    [SerializeField]public AudioClip[] clips_Player_Gun;
    [SerializeField]public AudioClip[] clips_Player_Weapon;
    [SerializeField]public AudioClip[] clips_Damage;
    [Header("����")]
    [Range(0,1)] public float defVolume = 0.5f;

    void Awake()
    {
        // �I�v�V�������特�ʐݒ�������Ă���
    }
}
