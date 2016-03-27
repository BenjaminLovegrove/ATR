﻿using UnityEngine;
using System.Collections;

public class PauseBGM : MonoBehaviour
{
    public AudioSource audio;

    void Start()
    {
        Cursor.visible = false;

        if (EventManager.inst.currentLevel == "City Outskirts")
        {
            audio.Pause();
        }
        else print("No level set for current level");        
    }
}
