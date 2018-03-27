using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerPoop : Marker {
    protected override void executeEffect()
    {
        int amount = Random.Range(1, 5) * 10;
        PetKeeper.pet.giveCurrency(amount);
        GameControl gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameControl>();
        gc.queueRewardText("Coins: +" + amount, new Color(1, 0.85f, 0.2f));
        PetKeeper.pet.grantXP(50);
    }
}
