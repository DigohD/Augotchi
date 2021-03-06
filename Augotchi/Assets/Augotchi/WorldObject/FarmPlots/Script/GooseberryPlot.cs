﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GooseberryPlot : BushPlot {

    override
    public void onPress()
    {
        int amount = Random.Range(5, 10);
        PetKeeper.pet.addFarmProduce(Inventory.ProduceType.GOOSEBERRY, amount);

        gc.queueRewardText("Gooseberries +" + amount, Inventory.getHarvestColor());

        base.onPress();
    }
}
