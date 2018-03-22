using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditModeUI : MonoBehaviour {

    public static bool isClothingMode = false;

    public GameObject creationUI;
    public GameObject clothingUI;

    public GameObject randomButton;

    public GameObject creationConfirmButton;
    public GameObject clothingConfirmButton;

	void Start () {
        if (isClothingMode)
        {
            creationUI.SetActive(false);
            clothingUI.SetActive(true);

            clothingConfirmButton.SetActive(true);
            creationConfirmButton.SetActive(false);

            randomButton.SetActive(false);

            creationUI.GetComponent<PetCreationUI>().Start();
        }
        else
        {
            creationUI.SetActive(true);
            clothingUI.SetActive(false);

            clothingConfirmButton.SetActive(false);
            creationConfirmButton.SetActive(true);

            randomButton.SetActive(true);
        }
	}
}
