using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class JsonData
{
    public WeponList[] weponList;
}

[System.Serializable]
public class WeponList
{
    //public Tags tags;
    public string name;
    public string image;
    //public Status status;
    public int damage;
    public int defknockback;
    public int maxknockback;
    public int maxcharge;
    //public Sprites sprites;
    public int wideth;
    public int height;
    public float offset;
}


public class GC_jsonInput : MonoBehaviour
{

    JsonData inputData;

    void Awake()
    {
        string inputString = Resources.Load<TextAsset>("jsons/WeponList").ToString();
        inputData = JsonUtility.FromJson<JsonData>(inputString);

        /*
        Debug.Log("json�C���v�b�g�F" + inputData.weponList.Length);
        Debug.Log("Tags");
        Debug.Log("Name�F" + inputData.weponList[0].name);
        Debug.Log("Image�F" + inputData.weponList[0].image);
        Debug.Log("Status");
        Debug.Log("Damage�F" + inputData.weponList[0].damage);
        Debug.Log("Knockback�F" + inputData.weponList[0].defknockback);
        Debug.Log("Knockback�F" + inputData.weponList[0].maxknockback);
        Debug.Log("Maxcharge�F" + inputData.weponList[0].maxcharge);
        Debug.Log("Sprites");
        Debug.Log("Wideth�F" + inputData.weponList[0].wideth);
        Debug.Log("Height�F" + inputData.weponList[0].height);
        Debug.Log("Offset�F" + inputData.weponList[0].offset);
        */
    }

    public JsonData GetList()
    {
        return inputData;
    }
}
