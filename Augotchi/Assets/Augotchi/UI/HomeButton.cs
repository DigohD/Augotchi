﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeButton : MonoBehaviour {

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            onClick();
        }
    }

    public void onClick()
    {
        GameState.mapZoom = GameObject.FindGameObjectWithTag("Map").transform.localScale.x;
        SceneManager.LoadScene("Home");
    }

}
