﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGame : MonoBehaviour {

	public void Quit()
    {
        print("Quiting game");
        Application.Quit();
    }
}
