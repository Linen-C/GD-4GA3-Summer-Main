using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerWeapon : MonoBehaviour
{
    /*
    // Ï
    [Header("Ï")]
    [SerializeField] float defAttackingTime = 0.3f;   // U»èÌîb­¶Ô
    [SerializeField] public float nowAttakingTime = 0.0f;  // »èÌ­¶Ô

    // XNvg
    [Header("XNvg")]
    [SerializeField] PlayerCTRL _playerCTRL;
    [SerializeField] SpriteChanger _spriteChanger;

    // LpX
    //[Header("LoX")]
    //[SerializeField] Text weponNameText;  // í¼\¦p
    [Header("UIp(}jA)")]
    [SerializeField] Image image_Wepon;
    //[SerializeField] Image image_Gun;

    // vCx[gÏ
    float spriteAlpha = 0.0f;
    float chargeCool = 0.0f;

    [Header("p[^")]
    [SerializeField] int defDamage = 1;      // Êí_[W
    [SerializeField] int maxDamage = 0;      // Åå_[W
    [SerializeField] int defKnockBack = 0;   // mbNobNp[
    [SerializeField] int maxKnockBack = 0;   // mbNobNp[
    [SerializeField] int maxCharge = 0;      // KvÅå`[W
    [SerializeField] int maxStanPower = 0;      // X^l

    int nowDamage = 0;
    int nowKockBack = 0;
    int nowStanPower = 0;
    bool comboFlag = false;

    // R|[lg
    BoxCollider2D coll;


    void Start()
    {
        coll = GetComponent<BoxCollider2D>();
        coll.enabled = false;

        _spriteChanger.ChangeTransparency(spriteAlpha);
    }
    
    void Update()
    {
        // »è­¶
        //     //
        if (nowAttakingTime >= 0)
        {
            nowAttakingTime -= Time.deltaTime;
        }
        else{ coll.enabled = false; }

        if (spriteAlpha > 0.0f)
        {
            _spriteChanger.ChangeTransparency(spriteAlpha);
            spriteAlpha -= Time.deltaTime * 2.0f;
        }

        if (chargeCool >= 0.0f)
        {
            chargeCool -= Time.deltaTime;
        }
        //     //
    }



    // íØèÖ¦
    //     //
    public int SwapWeapon(WeaponList[] wepon,int no)
    {
        // oOÁÄ½ç­§IÉ0ðËÁÞ
        if (no + 1 > wepon.Length) { no = 0; }


        // Tags
        //     //
        // eLXgÏX
        //weponNameText.text = wepon[no].name;

        // XvCgØèÖ¦Ì½ßpX
        Sprite inImage = Resources.Load<Sprite>(wepon[no].trail);
        _spriteChanger.ChangeSprite(inImage, wepon[no].offset);

        // ±±ÉACRàÇÁ·é©à
        // (Empty)


        // Status
        //     //
        // Åå_[W
        maxDamage = wepon[no].damage;

        // îbmbNobNÊ
        defKnockBack = wepon[no].defknockback;

        // ÅåmbNobNÊ
        maxKnockBack = wepon[no].maxknockback;

        // Åå`[WÊ
        maxCharge = wepon[no].maxcharge;

        // X^l
        maxStanPower = wepon[no].stanpower;


        // Sprites
        //     //
        // XP[
        transform.localScale = new Vector3(
            wepon[no].wideth, wepon[no].height, 1.0f);

        // ÀW
        transform.localPosition = new Vector3(
            0.0f, wepon[no].offset, 0.0f);


        // UI
        image_Wepon.sprite = Resources.Load<Sprite>(wepon[no].icon);


        // vC[ÉKvN[_EðnµÄ^[
        return wepon[no].maxcharge;
    }

    //     //



    // U­¶
    //     //
    public void Attacking(int nowCharge)
    {
        if (maxCharge == nowCharge)
        {
            nowDamage = maxDamage;
            nowKockBack = maxKnockBack;
            nowStanPower = maxStanPower;
        }
        else
        {
            nowDamage = defDamage;
            nowKockBack = defKnockBack;
            nowStanPower = 0;
        }

        coll.enabled = true;
        nowAttakingTime = defAttackingTime;

        spriteAlpha = 1.0f;
    }

    //     //


    public bool Combo()
    {
        if (!comboFlag) { return false; }

        comboFlag = false;
        return true;
    }


    // UÌ½»è
    //     //
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyCTRL>().TakeDamage(nowDamage, nowKockBack, nowStanPower);
            
            if (nowDamage == maxDamage)
            {
                if (chargeCool <= 0)
                {
                    _playerCTRL.GetCharge();
                    chargeCool = defAttackingTime;
                    _playerCTRL.comboCount++;
                }
                comboFlag = true;
            }
        }
    }

    //     //
    */
}
