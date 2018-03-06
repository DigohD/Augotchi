﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerFood : Marker {

    private void Start()
    {
        
    }

    protected override void executeEffect()
    {
        switch (Random.Range(0, 3))
        {
            case 0:
                PetKeeper.pet.candy += 1;
                gc.spawnRewardText("Candy +1");
                break;
            case 1:
                PetKeeper.pet.food += 1;
                gc.spawnRewardText("Canned Food +1");
                break;
            case 2:
                PetKeeper.pet.vegetables += 1;
                gc.spawnRewardText("Vegetable +1");
                break;
        }
    }
}