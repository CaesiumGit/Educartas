﻿using UnityEngine;
using System.Collections;

public class CreditsControl : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Application.LoadLevel("MainMenu");
        }
    }
}
