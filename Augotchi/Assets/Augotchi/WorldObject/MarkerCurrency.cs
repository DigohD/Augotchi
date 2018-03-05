using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerCurrency : Marker {
    protected override void executeEffect()
    {
        int amount = Random.Range(1, 5) * 10;
        PetKeeper.pet.giveCurrency(amount);
        gc.spawnRewardText("Coins: +" + amount);
    }
}
