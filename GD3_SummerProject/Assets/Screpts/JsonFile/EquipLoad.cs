using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class EquipLoad : MonoBehaviour
{
    [SerializeField] bool tutorial = false;

    JsonData defData;
    JsonData inputData;

    string _equipSavePath;
    string _weaponListPath;

    void Awake()
    {
        _equipSavePath = Application.streamingAssetsPath + "/jsons/EquipSave.json";
        _weaponListPath = "jsons/WeaponList";

        // ファイルが無い場合は初期化
        if (!File.Exists(_equipSavePath))
        {
            DefInput();

            string datas = JsonUtility.ToJson(inputData, true);
            File.WriteAllText(_equipSavePath, datas);

            Debug.Log("File_Generate：" + _equipSavePath);
        }
    }

    public JsonData GetList()
    {
        if (tutorial)
        {
            DefInput();
            return inputData;
        }

        string inputJson = File.ReadAllText(_equipSavePath).ToString();
        inputData = JsonUtility.FromJson<JsonData>(inputJson);

        //Debug.Log("InputString" + inputJson);

        return inputData;
    }


    void DefInput()
    {
        defData = new JsonData();

        inputData = new JsonData();
        inputData.weaponList = new WeaponList[2];

        string inputJson = Resources.Load<TextAsset>(_weaponListPath).ToString();
        defData = JsonUtility.FromJson<JsonData>(inputJson);

        for (int i = 0; i < inputData.weaponList.Length; i++)
        {
            inputData.weaponList[i] = defData.weaponList[i];
            //Debug.Log(inputData.weaponList[i].name);
        }
    }
}
