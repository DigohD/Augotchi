using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotatoPlot : GroundPlanesPlot {

    override
    public void onPress()
    {
        int amount = Random.Range(2, 5);
        PetKeeper.pet.addFarmProduce(Inventory.ProduceType.POTATO, amount);

        gc.queueRewardText("Potatoes +" + amount, Inventory.getHarvestColor());

        base.onPress();
    }
}
