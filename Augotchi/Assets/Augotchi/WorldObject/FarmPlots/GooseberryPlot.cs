using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GooseberryPlot : BushPlot {

    override
    public void onPress()
    {
        base.onPress();

        int amount = Random.Range(5, 10);
        PetKeeper.pet.inventory.produceCounts[(int)representedCrop.seedType] += amount;

        gc.queueRewardText("Gooseberries +" + amount, Inventory.getHarvestColor());
    }
}
