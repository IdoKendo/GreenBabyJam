using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponUI : MonoBehaviour {

    
    private GameObject fireWeapon;
    private GameObject wheelWeapon;
    private GameObject weapons;

    private Player player;

    private const float weaponUIXPosition = -0.25f;
    private const float weaponUIYPosition = 0.975f;

    // Use this for initialization
    void Start () {
        
        fireWeapon = GameObject.Find("FireWeapon");
        wheelWeapon = GameObject.Find("WheelWeapon");
        player = FindObjectOfType<Player>();
        setUI();
        weapons = GameObject.Find("WeaponUI");
        WeaponUIPosition();
    }

    private void WeaponUIPosition()
    {
        RectTransform weaponUIRect = weapons.transform as RectTransform;
        weaponUIRect.anchoredPosition = Vector2.zero;
        weaponUIRect.sizeDelta = Vector2.zero;
        weaponUIRect.anchorMax = new Vector2(weaponUIXPosition, weaponUIYPosition);
    }

    // Update is called once per frame
    void Update ()
    {
        setUI();
    }

    private void setUI()
    {
        if (player.Fireballs)
        {
            fireWeapon.SetActive(true);

            if (player.Shield)
            {
                wheelWeapon.SetActive(true);
            }
            else
            {
                wheelWeapon.SetActive(false);
            }
        }
        else
        {
            fireWeapon.SetActive(false);
            wheelWeapon.SetActive(false);
        }
    }

    public void ActivateFireball()
    {
        wheelWeapon.SetActive(true);
    }

    public void ActivateShild()
    {
        wheelWeapon.SetActive(true);
    }
}
