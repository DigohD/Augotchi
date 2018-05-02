using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildBaseButton : MonoBehaviour {
	void Update () {
		if(PetKeeper.pet.buildingMaterials > 100)
        {
            GetComponent<Button>().interactable = true;
            foreach (Image img in GetComponentsInChildren<Image>())
            {
                img.color = new Color(1, 1, 1, 1f);
            }
        }
        else
        {
            foreach (Image img in GetComponentsInChildren<Image>())
            {
                img.color = new Color(1, 1, 1, 0.5f);
            }
            GetComponent<Button>().interactable = false;
        }
	}
}
