using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponUI : MonoBehaviour {

    private GameObject noermalWeapon;
    private GameObject fireWeapon;
    private GameObject wheelWeapon;
    

    private Player player;

    // Use this for initialization
    void Start () {
        noermalWeapon = GameObject.Find("NoermalWeapon");
        fireWeapon = GameObject.Find("FireWeapon");
        wheelWeapon = GameObject.Find("WheelWeapon");
        player = FindObjectOfType<Player>();
        setUI();
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
