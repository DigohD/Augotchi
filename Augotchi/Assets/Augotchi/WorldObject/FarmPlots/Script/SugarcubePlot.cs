using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SugarcubePlot : BushPlot {

    override
    public void onPress()
    {
        int amount = Random.Range(2, 5);
        PetKeeper.pet.addFarmProduce(Inventory.ProduceType.SUGARCUBE, amount);

        gc.queueRewardText("Sugar cubes +" + amount, Inventory.getHarvestColor());

        base.onPress();
    }
}
