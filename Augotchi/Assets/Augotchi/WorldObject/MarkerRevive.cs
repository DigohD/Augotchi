using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerRevive : Marker {

    protected override void executeEffect()
    {
        PetKeeper.pet.markersRevive++;

        PetKeeper.pet.addReviveProgress();
        gc.queueRewardText("Revive: +1", new Color(1f, 0.4f, 0.4f));
    }
}
