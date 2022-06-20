using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu_Customize : MonoBehaviour
{
    [Header("���탊�X�g�Ƒ�������̃��[�h�E�Z�[�u")]
    [SerializeField] SaveManager saveManager;
    [SerializeField] WeaponListLoad weaponListLoad;
    JsonData jsonData;

    [Header("�����Ǘ�")]
    [SerializeField] public WeaponList[] equipList;

    [Header("�������̕���")]
    [SerializeField] int weponNomberA;
    [SerializeField] int weponNomberB;
    [SerializeField] int weponNomberC;
    [SerializeField] Text text_weponA;
    [SerializeField] Text text_weponB;
    [SerializeField] Text text_weponC;

    [Header("��ʑJ�ڗp")]
    [SerializeField] Canvas _Main;
    [SerializeField] Canvas _Customize;

    void Start()
    {
        weaponListLoad = saveManager.transform.GetComponent<WeaponListLoad>();
        jsonData = weaponListLoad.GetList();

        equipList = new WeaponList[3];
        equipList = saveManager.EquipLoad();

        weponNomberA = equipList[0].number;
        weponNomberB = equipList[1].number;
        weponNomberC = equipList[2].number;

        text_weponA.text = equipList[0].name;
        text_weponB.text = equipList[1].name;
        text_weponC.text = equipList[2].name;
    }


    // ��������ύX
    // ���������� ���������� ���������� ���������� ���������� //
    public void SetWeaponChangeA()
    {
        weponNomberA += 1;
        if (jsonData.weaponList.Length <= weponNomberA)
        {
            weponNomberA = 0;
        }
        equipList[0] = jsonData.weaponList[weponNomberA];
        text_weponA.text = equipList[0].name;
    }

    public void SetWeaponChangeB()
    {
        weponNomberB += 1;
        if (jsonData.weaponList.Length <= weponNomberB)
        {
            weponNomberB = 0;
        }
        equipList[1] = jsonData.weaponList[weponNomberB];
        text_weponA.text = equipList[1].name;
    }

    public void SetWeaponChangeC()
    {
        weponNomberC += 1;
        if (jsonData.weaponList.Length <= weponNomberC)
        {
            weponNomberC = 0;
        }
        equipList[2] = jsonData.weaponList[weponNomberC];
        text_weponA.text = equipList[2].name;
    }



    // ���C�����j���[�֖߂�
    // ���������� ���������� ���������� ���������� ���������� //
    public void Customize_to_Main()
    {
        saveManager.EquipSave();

        _Customize.enabled = false;
        _Main.enabled = true;
    }

}
