using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomePetUI : MonoBehaviour {

    public GameObject foodButtons;

	void Update () {
        if (PetKeeper.pet.isDead)
        {
            foodButtons.SetActive(false);
        }
        else
        {
            foodButtons.SetActive(true);
        }
	}
}
