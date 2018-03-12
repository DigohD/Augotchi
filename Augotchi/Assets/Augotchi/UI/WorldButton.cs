using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldButton : MonoBehaviour {

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            onClick();
        }
    }

    public void onClick()
    {
        PetKeeper.pet.clearFeedingListeners();

        GameControl.markerPicked = true;
        SceneManager.LoadScene("World");
    }

}
