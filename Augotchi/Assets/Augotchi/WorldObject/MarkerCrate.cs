using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerCrate : Marker {

    protected override void executeEffect()
    {
        switch (Random.Range(0, 2))
        {
            case 0:
                int hatIndex = Random.Range(1, PetKeeper.pet.petUnlocksData.unlockedHats.Length);
                int hatsVariationIndex = Random.Range(0, PetUnlocksData.hatCounts[hatIndex]);
                bool isNewHat = PetKeeper.pet.unlockHat(hatIndex, hatsVariationIndex);
                if (isNewHat)
                {
                    gc.queueRewardText("New Hat!", new Color(0, 0.8f, 0.7f));
                    PetKeeper.pet.grantXP(500);
                }
                else
                {
                    gc.queueRewardText("Duplicate Hat...", new Color(0, 0.8f, 0.7f));
                    int money = (Random.Range(5, 10) * 10);
                    PetKeeper.pet.giveCurrency(money);
                    gc.queueRewardText("Coins: +" + money, new Color(1, 0.85f, 0.2f));
                    PetKeeper.pet.grantXP(200);
                }
                break;
            case 1:
                int faceIndex = Random.Range(1, PetKeeper.pet.petUnlocksData.unlockedFaces.Length);
                int faceVariationIndex = Random.Range(0, PetUnlocksData.faceCounts[faceIndex]);
                bool isNewFace = PetKeeper.pet.unlockFace(faceIndex, faceVariationIndex);
                if (isNewFace)
                {
                    gc.queueRewardText("New Facial\nFeature!", new Color(0, 0.8f, 0.7f));
                    PetKeeper.pet.grantXP(500);
                }
                else
                {
                    gc.queueRewardText("Duplicate Facial\nfeature...", new Color(0, 0.8f, 0.7f));
                    int money = (Random.Range(5, 10) * 10);
                    PetKeeper.pet.giveCurrency(money);
                    gc.queueRewardText("Coins: +" + money, new Color(1, 0.85f, 0.2f));
                    PetKeeper.pet.grantXP(200);
                }
                break;
        }
    }
}
