using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerRoot : Marker {

    protected override void executeEffect()
    {
        int random = Random.Range(0, 1000);

        PetKeeper.pet.inventory.uniqueCounts[(int) Inventory.UniqueType.WRONGWORLD_ROOTS] += 1;
        gc.queueRewardText("Wrongworld\nRoots +1", Inventory.getHarvestColor());

        InventoryUI.reRender = true;

        PetKeeper.pet.grantXP(50);
    }
}
