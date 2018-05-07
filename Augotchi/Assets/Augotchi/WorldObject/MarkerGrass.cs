using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerGrass : Marker {

    protected override void executeEffect()
    {
        int random = Random.Range(0, 1000);

        if(random < 425)
        {
            PetKeeper.pet.addSeed(Inventory.SeedType.CARROT_SEED, 1);
            gc.queueRewardText("Carrot Seed +1", Inventory.getHarvestColor());
        }
        else if (random < 850)
        {
            PetKeeper.pet.addSeed(Inventory.SeedType.MEATBALL_SEED, 1);
            gc.queueRewardText("Meatball Seed +1", Inventory.getHarvestColor());
        }
        else
        {
            PetKeeper.pet.addSeed(Inventory.SeedType.GOOSEBERRY_SEED, 1);
            gc.queueRewardText("Gooseberry Seed +1", Inventory.getHarvestColor());
        }

        InventoryUI.reRender = true;

        PetKeeper.pet.grantXP(25);
    }
}
