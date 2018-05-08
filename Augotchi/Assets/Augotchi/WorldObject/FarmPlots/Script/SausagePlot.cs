using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SausagePlot : GroundPlanesPlot {

    override
    public void onPress()
    {
        int amount = Random.Range(2, 5);
        PetKeeper.pet.addFarmProduce(Inventory.ProduceType.SAUSAGE, amount);

        gc.queueRewardText("Sausages +" + amount, Inventory.getHarvestColor());

        base.onPress();
    }
}
