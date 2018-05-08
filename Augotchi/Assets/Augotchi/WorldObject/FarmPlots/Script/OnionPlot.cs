using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnionPlot : GroundPlanesPlot {

    override
    public void onPress()
    {
        int amount = Random.Range(2, 5);
        PetKeeper.pet.addFarmProduce(Inventory.ProduceType.ONION, amount);

        gc.queueRewardText("Onions +" + amount, Inventory.getHarvestColor());

        base.onPress();
    }
}
