using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrotPlot : GroundPlanesPlot {

    override
    public void onPress()
    {
        base.onPress();

        int amount = Random.Range(3, 7);
        PetKeeper.pet.inventory.produceCounts[(int) representedCrop.seedType] += amount;

        gc.queueRewardText("Carrots +" + amount, Inventory.getHarvestColor());
    }
}
