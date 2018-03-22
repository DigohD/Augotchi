using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldButton : MonoBehaviour {

    public PetCreationUI customizationUI;

    public bool isCreation;

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
        if (customizationUI)
        {
            if (isCreation)
                GameControl.firstStartup = true;
            PetKeeper.pet.SaveVisuals(customizationUI.pvd);
        }

        GameControl.markerPicked = true;
        SceneManager.LoadScene("World");
    }

}
