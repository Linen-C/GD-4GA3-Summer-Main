using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.IO;

public class Rebinding : MonoBehaviour
{
    [SerializeField] Text _Text;
    [SerializeField] Text _ListText;
    [SerializeField] PlayerInput _input;
    [SerializeField] InputActionReference _action;
    [SerializeField] InputActionAsset _actionAsset;

    private InputActionRebindingExtensions.RebindingOperation _rebindingOperation;

    string bindTarget;

    private void Start()
    {
        // �^�X�N�F�o�C���f�B���O���e��json�Ƃ���resource�t�H���_�ɐ����A�ǂݍ��݂���
        // �L�[�{�[�h�ł��������R���g���[���[�ł����Ȃ���

        //string tes = _actionAsset.FindAction(_action.action.name).ToString();
        //Debug.Log(tes);

        foreach (var actions in _actionAsset.actionMaps[0])
        {
            _ListText.text += actions.name + "�F";
            _ListText.text += actions.GetBindingDisplayString() + "\n";
            //Debug.Log(actions.name);
        }

        _Text.text = _action.action.name + "�F" + _action.action.GetBindingDisplayString();
    }

    public void Clicked()
    {
        var target = _action.action;

        _input.SwitchCurrentActionMap("Select");
        _Text.text = "�o�C���f�B���O��";

        _rebindingOperation = _action.action.PerformInteractiveRebinding()
            .OnComplete(opth => Key_RebindingComplete())
            .Start();
    }

    void Key_RebindingMode()
    {
        // �^�X�N�F�u_action�v��Clicked�̃g�R�Ŏw��ł���Ύ��R�ɂł�����

        _rebindingOperation = _action.action.PerformInteractiveRebinding()
            .OnComplete(opth => Key_RebindingComplete())
            .Start();
    }

    void Key_RebindingComplete()
    {
        bindTarget = InputControlPath.ToHumanReadableString(
            _action.action.bindings[0].effectivePath,
            InputControlPath.HumanReadableStringOptions.OmitDevice);

        Debug.Log(bindTarget);

        // �^�X�N�F����path���Ƃ������肢���Ƃ��΂����邩���H

        _action.action.ApplyBindingOverride(new InputBinding { path = "<Keyboard>/g", overridePath = "<Keyboard>/" + bindTarget });
        _input.SwitchCurrentActionMap("Player");

        _rebindingOperation.Dispose();


        _ListText.text = "";

        foreach (var actions in _actionAsset.actionMaps[0])
        {
            _ListText.text += actions.name + "�F";
            _ListText.text += actions.GetBindingDisplayString() + "\n";
            Debug.Log(actions.name + "�F" + actions.GetBindingDisplayString());
        }

        _Text.text = _Text.text = _action.action.name + "�F" + _action.action.GetBindingDisplayString();
    }

}