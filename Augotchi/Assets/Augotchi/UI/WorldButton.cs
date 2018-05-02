using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WorldButton : MonoBehaviour {

    public PetCreationUI customizationUI;

    public bool isCreation;

    public GameObject DoneButton;

    private string currentName;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            onClick();
        }
    }

    public void onClick()
    {
        if (customizationUI)
        {
            if (isCreation)
            {
                GameControl.firstStartup = true;
                PetKeeper.pet.name = currentName;
                PetKeeper.pet.Save(false);
            }
            PetKeeper.pet.SaveVisuals(customizationUI.pvd);
        }

        GameControl.markerPicked = true;
        SceneManager.LoadScene("World");
    }

    public void onTextChange(string input)
    {
        if(input.Length > 0)
        {
            currentName = input;
            DoneButton.GetComponent<Button>().interactable = true;
        }
        else
        {
            currentName = "";
            DoneButton.GetComponent<Button>().interactable = false;
        }
    }

}
