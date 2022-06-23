using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("PlayerCTRL")]
    [SerializeField] PlayerCTRL _plCTRL;

    [Header("ゲームオブジェクト")]
    [SerializeField] GameObject cursor;
    [SerializeField] GameObject cursorImage;
    [SerializeField] GameObject bullet;

    [Header("遠距離攻撃")]
    [SerializeField] public int needCharge;  // 遠距離攻撃に必要なチャージ
    [SerializeField] public int nowCharge;   // 現在のチャージ

    public void Attack(PlayerControls playerControls, GC_BpmCTRL bpmCTRL, PlayerWeapon playerWeapon)
    {
        if (playerControls.Player.Attack.triggered
            && bpmCTRL.Signal()
            && _plCTRL.coolDownReset == false)
        {
            _plCTRL._anim.SetTrigger("Attack");
            playerWeapon.Attacking(_plCTRL.nowWeponCharge);
            _plCTRL.coolDownReset = true;
        }

        if (playerWeapon.Combo())
        {
            _plCTRL.nowWeponCharge = _plCTRL.maxWeponCharge;
            _plCTRL.doComboMode = true;
            _plCTRL.coolDownReset = false;
            _plCTRL.comboTimeLeft = 2;
        }


        if (bpmCTRL.Metronome())
        {
            if (_plCTRL.coolDownReset == true && _plCTRL.doComboMode == false)
            {
                _plCTRL.nowWeponCharge = 1;
                _plCTRL.coolDownReset = false;
            }
            else if (_plCTRL.nowWeponCharge < _plCTRL.maxWeponCharge) { _plCTRL.nowWeponCharge++; }

            if (_plCTRL.comboTimeLeft > 0)
            {
                _plCTRL.comboTimeLeft--;
            }
            if (_plCTRL.comboTimeLeft == 0 && _plCTRL.doComboMode == true)
            {
                _plCTRL.doComboMode = false;
                _plCTRL.coolDownReset = true;
            }


            if (_plCTRL.nowWeponCharge == (_plCTRL.maxWeponCharge - 1))
            {
                //anim.SetTrigger("Charge");
                _plCTRL._flashAnim.SetTrigger("FlashTrigger");
            }
        }

    }

    public void SwapWepon(PlayerControls playerControls, PlayerWeapon playerWeapon)
    {
        var valueW = playerControls.Player.WeponSwapWhile.ReadValue<float>();
        var valueUp = playerControls.Player.WeponSwapButtonUp.triggered;
        var valueDwon = playerControls.Player.WeponSwapButtonDown.triggered;

        if (valueW != 0 || (valueUp || valueDwon))
        {
            if (valueW > 0 || valueUp)
            {
                _plCTRL.equipNo++;

                if (_plCTRL.equipNo >= _plCTRL.equipList.weaponList.Length) { _plCTRL.equipNo = 0; }
            }

            if (valueW < 0 || valueDwon)
            {
                _plCTRL.equipNo--;
                if (_plCTRL.equipNo <= _plCTRL.equipList.weaponList.Length) { _plCTRL.equipNo = 2; }
            }

            // 必要クールダウン上書き
            _plCTRL.maxWeponCharge =
                playerWeapon.SwapWeapon(_plCTRL.equipList.weaponList, _plCTRL.equipNo);
            // 現クールダウンを上書き
            _plCTRL.nowWeponCharge = 0;
        }
    }

    public void Shooting(PlayerControls playerControls, GC_BpmCTRL bpmCTRL)
    {
        if (bpmCTRL.Signal())
        {
            if (nowCharge == needCharge && playerControls.Player.Shot.triggered)
            {
                Instantiate(
                    bullet,
                    new Vector3
                    (cursorImage.transform.position.x,
                    cursorImage.transform.position.y,
                    cursorImage.transform.position.z),
                    cursor.transform.rotation);

                nowCharge = 0;
            }
        }
    }
}
