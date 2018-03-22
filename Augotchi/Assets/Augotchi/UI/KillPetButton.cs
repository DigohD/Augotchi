using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPetButton : MonoBehaviour {

    public void onClick()
    {
        PetKeeper.pet.addReviveProgress();
        
    }
}
