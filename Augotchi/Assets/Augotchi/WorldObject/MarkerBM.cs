using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerBM : Marker {

    protected override void executeEffect()
    {
        int random = Random.Range(1, 3);
        
        gc.queueRewardText("Building\nMaterials: +" + (random * 5), new Color(0.9f, 0.75f, 0.4f));

        PetKeeper.pet.giveBuildingMaterials(random * 5);
        PetKeeper.pet.grantXP(25);
    }
}
