using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPet : MonoBehaviour {

	public void onClick()
    {
        PetKeeper.pet.ResetPet();
    }
}
