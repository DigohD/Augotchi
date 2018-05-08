using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerPond : Marker {

    protected override void executeEffect()
    {
        Inventory.SeedType seedType = LootTable.GenerateRandomSeedType();

        string name = System.Enum.GetName(typeof(Inventory.SeedType), seedType);

        PetKeeper.pet.addSeed(seedType, 1);
        gc.queueRewardText(name.Replace("_", " ") + " +1", Inventory.getRarityColor(Inventory.getSeedTypeInfo(seedType).rarity));

        InventoryUI.reRender = true;

        PetKeeper.pet.grantXP(25);
    }
}
