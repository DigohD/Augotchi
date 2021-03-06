﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerPond : Marker {

    protected override void executeEffect()
    {
        Inventory.ProduceType produceType = LootTable.GeneratePondLoot();

        string name = System.Enum.GetName(typeof(Inventory.ProduceType), produceType);

        int amount = Random.Range(1, 4);
        PetKeeper.pet.addWildProduce(produceType, amount);
        gc.queueRewardText(name.Replace("_", " ") + " +" + amount, Inventory.getRarityColor(Inventory.getProduceTypeInfo(produceType).rarity));

        InventoryUI.reRender = true;

        PetKeeper.pet.grantXP(50);
    }
}
