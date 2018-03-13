using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothingBTN : MonoBehaviour {

    public GameObject clothing;
    public GameObject customization;

    public GameObject otherButton;

	public void onClothingClick()
    {
        clothing.SetActive(true);
        customization.SetActive(false);
        gameObject.SetActive(false);

        otherButton.SetActive(true);
    }

    public void onCustomizationClick()
    {
        clothing.SetActive(false);
        customization.SetActive(true);
        gameObject.SetActive(false);

        otherButton.SetActive(true);
    }
}
