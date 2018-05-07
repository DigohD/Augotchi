using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerCrate : Marker {

    protected override void executeEffect()
    {
        PetKeeper.pet.markersCrate++;

        PetKeeper.pet.addRandomLootItem();
        PetKeeper.pet.grantXP(100);
    }
}
