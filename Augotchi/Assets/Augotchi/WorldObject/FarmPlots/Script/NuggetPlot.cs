using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NuggetPlot : BushPlot {

    override
    public void onPress()
    {
        int amount = Random.Range(2, 5);
        PetKeeper.pet.addFarmProduce(Inventory.ProduceType.NUGGETS, amount);

        gc.queueRewardText("Nuggets +" + amount, Inventory.getHarvestColor());

        base.onPress();
    }
}
