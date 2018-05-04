using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildBaseButton : MonoBehaviour {

    public bool isMoveNotBuild = false;

	void Update () {
		if(!isMoveNotBuild && PetKeeper.pet.buildingMaterials >= 100)
        {
            GetComponent<Button>().interactable = true;
            foreach (Image img in GetComponentsInChildren<Image>())
            {
                img.color = new Color(img.color.r, img.color.g, img.color.b, 1f);
            }
        }else if (isMoveNotBuild && PetKeeper.pet.buildingMaterials >= 500)
        {
            GetComponent<Button>().interactable = true;
            foreach (Image img in GetComponentsInChildren<Image>())
            {
                img.color = new Color(img.color.r, img.color.g, img.color.b, 1f);
            }
        }
        else
        {
            foreach (Image img in GetComponentsInChildren<Image>())
            {
                img.color = new Color(img.color.r, img.color.g, img.color.b, 0.5f);
            }
            GetComponent<Button>().interactable = false;
        }
	}
}
