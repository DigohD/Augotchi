using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrotPlot : GroundPlanesPlot {

    override
    public void onPress()
    {
        int amount = Random.Range(3, 7);
        PetKeeper.pet.addFarmProduce(Inventory.ProduceType.CARROT, amount);

        gc.queueRewardText("Carrots +" + amount, Inventory.getHarvestColor());

        base.onPress();
    }
}
