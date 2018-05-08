using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CabbagePlot : GroundPlanesPlot {

    override
    public void onPress()
    {
        int amount = Random.Range(2, 5);
        PetKeeper.pet.addFarmProduce(Inventory.ProduceType.CABBAGE, amount);

        gc.queueRewardText("Cabbage +" + amount, Inventory.getHarvestColor());

        base.onPress();
    }
}
