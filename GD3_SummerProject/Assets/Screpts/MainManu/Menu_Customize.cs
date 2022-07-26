using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Menu_Customize : MonoBehaviour
{
    [Header("�A�j���[�^�[")]
    [SerializeField] Animator _animator;

    [Header("�\�L�X�V�p")]
    [SerializeField] Menu_StundbyMaingame _stMG;

    [Header("���탊�X�g�Ƒ�������̃��[�h�E�Z�[�u")]
    [SerializeField] SaveManager saveManager;
    [SerializeField] WeaponListLoad weaponListLoad;
    JsonData jsonData;

    [Header("�ꗗ")]
    [SerializeField] public WeaponList[] weaponList;

    [Header("�����Ǘ�")]
    [SerializeField] public WeaponList[] equipList;

    [Header("�������̕���")]
    [SerializeField] int[] weaponNumbers;
    [SerializeField] Image image_weaponA;
    [SerializeField] Image image_weaponB;
    [SerializeField] Image[] weaponImages;

    [Header("�{�^�����I����")]
    [SerializeField] GameObject _buttonTemp;
    [SerializeField] GameObject _parent;   // �����e�Ƃ��ă{�^���𐶐�
    [SerializeField] int _defHoriDist;
    [SerializeField] int _horiDist;
    [SerializeField] int _defVertDist;
    [SerializeField] int _vertDist;

    [Header("�E���")]
    [SerializeField] TextMeshProUGUI text_weaponName;
    [SerializeField] TextMeshProUGUI text_weaponSpec;
    [SerializeField] TextMeshProUGUI text_description;

    [Header("�ύX�p")]
    [SerializeField] int _target_Num;

    [Header("�{�^�����X�g")]
    [SerializeField] Menu_Button _menu_Button;


    public void EnableMenu()
    {
        //Debug.Log("Custom_Run");
        weaponListLoad = saveManager.transform.GetComponent<WeaponListLoad>();
        jsonData = weaponListLoad.GetList();

        equipList = new WeaponList[2];
        equipList = saveManager.EquipLoad();

        weaponNumbers = new int[2];
        weaponNumbers[0] = equipList[0].number;
        weaponNumbers[1] = equipList[1].number;

        weaponImages = new Image[2];
        weaponImages[0] = image_weaponA;
        weaponImages[1] = image_weaponB;

        weaponImages[0].sprite = Resources.Load<Sprite>(equipList[0].icon);
        weaponImages[1].sprite = Resources.Load<Sprite>(equipList[1].icon);

        ButtonErase();
    }


    // ��������ύX
    // ���������� ���������� ���������� ���������� ���������� //
    public void SetWeaponChangeA()
    {
        ButtonErase();
        _target_Num = 0;
        ShowWeaponData();
        ButtonGenerate();
    }

    public void SetWeaponChangeB()
    {
        ButtonErase();
        _target_Num = 1;
        ShowWeaponData();
        ButtonGenerate();
    }

    public void SetGunChange()
    {
        ButtonErase();
    }

    void ButtonGenerate()
    {
        _horiDist = -_defHoriDist;
        _vertDist = _defVertDist;

        for (int num = 1; jsonData.weaponList.Length > (num - 1); num++)
        {
            int indexNumber = num - 1;
            var button = Instantiate(_buttonTemp, new Vector2(_horiDist, _vertDist), Quaternion.identity);
            var image = button.transform.GetChild(0).GetComponent<Image>();

            button.transform.SetParent(_parent.transform, false);
            button.name = "weaponButton" + num.ToString();

            image.sprite = Resources.Load<Sprite>(jsonData.weaponList[indexNumber].icon);

            button.GetComponent<Button>().onClick.AddListener(() => SetWepon(button, indexNumber));
            var test = button.GetComponent<Selectable>();
            
            _horiDist += _defHoriDist - 50;
            if (num % 4 == 0 && num != 0)
            {
                _horiDist = -_defHoriDist;
                _vertDist -= _defVertDist;
            }
        }
    }

    void SetWepon(GameObject btn, int number)
    {
        //Debug.Log("�e�X�g�F" + btn.name + "\n�i���o�[�F" + number);
        equipList[_target_Num] = jsonData.weaponList[number];

        weaponNumbers[_target_Num] = equipList[_target_Num].number;
        weaponImages[_target_Num].sprite = Resources.Load<Sprite>(equipList[_target_Num].icon);

        ShowWeaponData();
    }

    void ButtonErase()
    {
        text_weaponName.text = "";
        text_weaponSpec.text = "";
        text_description.text = "";

        foreach (Transform child in _parent.transform)
        {
            Destroy(child.gameObject);
        }
    }

    // �\��
    // ���������� ���������� ���������� ���������� ���������� //
    void ShowWeaponData()
    {
        text_weaponName.text = equipList[_target_Num].name;

        string damage = equipList[_target_Num].damage.ToString();
        string knockBack = equipList[_target_Num].maxknockback.ToString();
        string stanPower = equipList[_target_Num].stanpower.ToString();
        string width = equipList[_target_Num].wideth.ToString();
        string height = equipList[_target_Num].height.ToString();
        string offset = equipList[_target_Num].offset.ToString();

        text_weaponSpec.text =
            damage + "\n" +
            knockBack + "\n" +
            stanPower + "\n" +
            offset + "+" + width + "x" + height;

        string description = equipList[_target_Num].text;
        text_description.text = description;
    }



    //  �O�̃��j���[�֖߂�
    // ���������� ���������� ���������� ���������� ���������� //
    public void Customize_to_Back()
    {
        ButtonErase();
        saveManager.EquipSave();

        _stMG.SetWeaponImage();
        _animator.SetBool("Custom_Bool", false);
    }

}
