using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MenuAudioCTRL : MonoBehaviour
{
    [Header("�N���b�v")]
    [SerializeField] public AudioClip[] _clips;

    [Header("����")]
    [Range(0, 1)] public float nowVolume = 0.5f;

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
    }
}
