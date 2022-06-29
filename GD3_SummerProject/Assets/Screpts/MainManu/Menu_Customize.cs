using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Menu_Customize : MonoBehaviour
{
    [Header("���탊�X�g�Ƒ�������̃��[�h�E�Z�[�u")]
    [SerializeField] SaveManager saveManager;
    [SerializeField] WeaponListLoad weaponListLoad;
    JsonData jsonData;

    [Header("�ꗗ")]
    [SerializeField] public WeaponList[] weaponList;

    [Header("�����Ǘ�")]
    [SerializeField] public WeaponList[] equipList;

    [Header("�������̕���")]
    [SerializeField] int weaponNomberA;
    [SerializeField] int weaponNomberB;
    [SerializeField] int weaponNomberC;
    [SerializeField] Image image_weaponA;
    [SerializeField] Image image_weaponB;
    [SerializeField] Image image_weaponC;

    [Header("�E���")]
    [SerializeField] TextMeshProUGUI text_weaponA_Name;
    [SerializeField] TextMeshProUGUI text_weaponA_Spec;
    //[SerializeField] Text text_weaponB;
    //[SerializeField] Text text_weaponC;

    [Header("��ʑJ�ڗp")]
    [SerializeField] Canvas _Main;
    [SerializeField] Canvas _Customize;

    [Header("�ύX�p")]
    [SerializeField] int _target_Num;

    void Start()
    {
        weaponListLoad = saveManager.transform.GetComponent<WeaponListLoad>();
        jsonData = weaponListLoad.GetList();

        //weaponList = jsonData.weaponList;

        equipList = new WeaponList[3];
        equipList = saveManager.EquipLoad();

        weaponNomberA = equipList[0].number;
        weaponNomberB = equipList[1].number;
        weaponNomberC = equipList[2].number;

        image_weaponA.sprite = Resources.Load<Sprite>(equipList[0].icon);
        image_weaponB.sprite = Resources.Load<Sprite>(equipList[1].icon);
        image_weaponC.sprite = Resources.Load<Sprite>(equipList[2].icon);
    }



    // ��������ύX
    // ���������� ���������� ���������� ���������� ���������� //
    public void SetWeaponChangeA()
    {
        /*
        weaponNomberA += 1;
        if (jsonData.weaponList.Length <= weaponNomberA)
        {
            weaponNomberA = 0;
        }
        equipList[0] = jsonData.weaponList[weaponNomberA];
        image_weaponA.sprite = Resources.Load<Sprite>(equipList[0].icon);
        */

        _target_Num = 0;

        ShowWeaponData();
    }

    public void SetWeaponChangeB()
    {
        /*
        weaponNomberB += 1;
        if (jsonData.weaponList.Length <= weaponNomberB)
        {
            weaponNomberB = 0;
        }
        equipList[1] = jsonData.weaponList[weaponNomberB];
        image_weaponB.sprite = Resources.Load<Sprite>(equipList[1].icon);
        */

        _target_Num = 1;

        ShowWeaponData();
    }

    public void SetWeaponChangeC()
    {
        /*
        weaponNomberC += 1;
        if (jsonData.weaponList.Length <= weaponNomberC)
        {
            weaponNomberC = 0;
        }
        equipList[2] = jsonData.weaponList[weaponNomberC];
        image_weaponC.sprite = Resources.Load<Sprite>(equipList[2].icon);
        */

        _target_Num = 2;

        ShowWeaponData();
    }

    public void SetGunChange()
    {

    }


    void SetWeapon()
    {
        //equipList[_target_Num] = ;
    }


    // �\��
    // ���������� ���������� ���������� ���������� ���������� //
    void ShowWeaponData()
    {
        text_weaponA_Name.text = equipList[_target_Num].name;

        string damage = equipList[_target_Num].damage.ToString();
        string knockBack = equipList[_target_Num].maxknockback.ToString();
        string maxCharge = equipList[_target_Num].maxcharge.ToString();
        string stanPower = equipList[_target_Num].stanpower.ToString();
        string width = equipList[_target_Num].wideth.ToString();
        string height = equipList[_target_Num].height.ToString();

        text_weaponA_Spec.text =
            damage + "\n" +
            knockBack + "\n" +
            maxCharge + "\n" +
            stanPower + "\n" +
            width + "x" + height;
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
