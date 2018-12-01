using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour {

    private GameObject healthBar;
    private Transform bar;
   
    private Player player;

    private const float healthBarXPosition = 0.55f;
    private const float healthBarYPosition = 0.54f;

    // Use this for initialization
    void Start () {
        bar = transform.Find("Bar");
        player = FindObjectOfType<Player>();
        healthBar = GameObject.Find("HealthBar");
        HealthBarPosition();


    }

    private void HealthBarPosition()
    {
        RectTransform healthBarRect = healthBar.transform as RectTransform;
        healthBarRect.anchorMax = new Vector2(healthBarXPosition, healthBarYPosition);
    }

    // Update is called once per frame
    void Update () {
        ChangeHealt();
    }

    public void ChangeHealt()
    {
        float normilizedHealth = player.Health / player.MaxHealth;

        normilizedHealth = Mathf.Clamp(normilizedHealth, 0, 1);
        bar.localScale = new Vector2(normilizedHealth, 1f);
    }
}
