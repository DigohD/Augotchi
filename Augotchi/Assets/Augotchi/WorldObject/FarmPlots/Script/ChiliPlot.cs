using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChiliPlot : BushPlot {

    override
    public void onPress()
    {
        int amount = Random.Range(2, 5);
        PetKeeper.pet.addFarmProduce(Inventory.ProduceType.CHILI, amount);

        gc.queueRewardText("Chili +" + amount, Inventory.getHarvestColor());

        base.onPress();
    }
}
