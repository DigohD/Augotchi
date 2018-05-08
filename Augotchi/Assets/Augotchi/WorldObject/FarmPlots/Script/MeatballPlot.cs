using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeatballPlot : BushPlot {

    override
    public void onPress()
    {
        int amount = Random.Range(2, 5);
        PetKeeper.pet.addFarmProduce(Inventory.ProduceType.MEATBALL, amount);

        gc.queueRewardText("Meatballs +" + amount, Inventory.getHarvestColor());

        base.onPress();
    }
}
