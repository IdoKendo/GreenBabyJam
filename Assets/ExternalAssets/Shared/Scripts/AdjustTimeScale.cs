﻿using Shared.Enums;
using TMPro;
using UnityEngine;

public class AdjustTimeScale : MonoBehaviour
{
    TextMeshProUGUI textMesh;

    private void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if (Input.GetAxis(AxisActionType.MOUSE_SCROLL_WHEEL) > 0f)
        {
            if (Time.timeScale < 1.0F)
            {
                Time.timeScale += 0.1f;
            }
               
            Time.fixedDeltaTime = 0.02F * Time.timeScale;

            if (textMesh != null)
            {
                textMesh.text = "Time Scale : " + System.Math.Round(Time.timeScale, 2);
            }
          
        }
        else if (Input.GetAxis(AxisActionType.MOUSE_SCROLL_WHEEL) < 0f)
        {
            if (Time.timeScale >= 0.2F)
            {
                Time.timeScale -= 0.1f;
            }
                
            Time.fixedDeltaTime = 0.02F * Time.timeScale;

            if (textMesh != null)
            {
                textMesh.text = "Time Scale : " + System.Math.Round(Time.timeScale, 2);
            }
        }
    }

    void OnApplicationQuit()
    {
        Time.timeScale = 1.0F;
        Time.fixedDeltaTime = 0.02F;
    }
}