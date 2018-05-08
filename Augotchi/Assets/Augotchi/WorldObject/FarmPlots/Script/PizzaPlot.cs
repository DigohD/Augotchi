using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaPlot : BushPlot {

    override
    public void onPress()
    {
        int amount = Random.Range(2, 5);
        PetKeeper.pet.addFarmProduce(Inventory.ProduceType.PIZZA, amount);

        gc.queueRewardText("Pizza +" + amount, Inventory.getHarvestColor());

        base.onPress();
    }
}
