using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReviveButton : MonoBehaviour {

    private void Awake()
    {
        if(PetKeeper.pet.inventory.uniqueCounts[(int) Inventory.UniqueType.WRONGWORLD_ROOTS] > 0)
        {
            GetComponent<Button>().interactable = true;
            foreach (Image img in GetComponentsInChildren<Image>())
            {
                img.color = new Color(1, 1, 1, 1f);
            }
            GetComponentInChildren<Text>().text = "Revive!";
        }
        else
        {
            GetComponent<Button>().interactable = false;
            foreach (Image img in GetComponentsInChildren<Image>())
            {
                img.color = new Color(1, 1, 1, 0.3f);
            }
            GetComponentInChildren<Text>().text = "Find Roots!";
        }
    }

    public void onClick () {

        if (PetKeeper.pet.inventory.uniqueCounts[(int)Inventory.UniqueType.WRONGWORLD_ROOTS] > 0)
        {
            PetKeeper.pet.inventory.uniqueCounts[(int)Inventory.UniqueType.WRONGWORLD_ROOTS] -= 1;
            PetKeeper.pet.revive();
        }
	}

}
