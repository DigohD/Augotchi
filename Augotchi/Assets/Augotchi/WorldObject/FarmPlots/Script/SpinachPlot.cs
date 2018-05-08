using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinachPlot : GroundPlanesPlot {

    override
    public void onPress()
    {
        int amount = Random.Range(2, 5);
        PetKeeper.pet.addFarmProduce(Inventory.ProduceType.SPINACH, amount);

        gc.queueRewardText("Spinach +" + amount, Inventory.getHarvestColor());

        base.onPress();
    }
}
