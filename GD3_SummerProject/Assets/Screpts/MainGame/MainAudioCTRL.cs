using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MainAudioCTRL : MonoBehaviour
{
    [Header("�N���b�v")]
    [SerializeField]public AudioClip[] clips_BPM;
    [SerializeField]public AudioClip[] clips_Progress;
    [SerializeField]public AudioClip[] clips_Player_Gun;
    [SerializeField]public AudioClip[] clips_Player_Weapon;
    [SerializeField]public AudioClip[] clips_Damage;
    [Header("����")]
    [Range(0,1)] public float nowVolume = 0.5f;
    [Header("�A�b�v�f�[�g��")]
    [SerializeField] GC_BpmCTRL _GC_BpmCTRL;

    void Awake()
    {
        // �I�v�V�������特�ʐݒ�������Ă���
        var location = Application.streamingAssetsPath + "/jsons/OptionSave.json";
        string inputJson = File.ReadAllText(location).ToString();
        var optionData = JsonUtility.FromJson<OptionData>(inputJson);

        nowVolume = optionData.SEvolume;
    }

    public void VolumeSet(float volume)
    {
        nowVolume = volume;
        _GC_BpmCTRL.VolumeUpdate(nowVolume);
    }
}
