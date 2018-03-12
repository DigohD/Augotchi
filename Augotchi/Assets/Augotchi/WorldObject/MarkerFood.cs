using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerFood : Marker {

    private void Start()
    {
        
    }

    protected override void executeEffect()
    {
        int amount = Random.Range(3, 6);
        for(int i = 0; i < amount; i++)
        {
            switch (Random.Range(0, 3))
            {
                case 0:
                    PetKeeper.pet.candy += 1;
                    gc.queueRewardText("Candy +1", new Color(0.95f, 0.3f, 1f));
                    break;
                case 1:
                    PetKeeper.pet.food += 1;
                    gc.queueRewardText("Canned Food +1", new Color(0.61f, 0.41f, 0.25f));
                    break;
                case 2:
                    PetKeeper.pet.vegetables += 1;
                    gc.queueRewardText("Vegetable +1", new Color(0.25f, 0.65f, 0.25f));
                    break;
            }
        }
        PetKeeper.pet.grantXP(100);
    }
}
