using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerCurrency : Marker {
    protected override void executeEffect()
    {
        PetKeeper.pet.markersCurrency++;
        int amount = Random.Range(1, 5) * 10;
        PetKeeper.pet.giveCurrency(amount);
        gc.queueRewardText("Coins: +" + amount, new Color(1, 0.85f, 0.2f));
        PetKeeper.pet.grantXP(100);
    }
}
