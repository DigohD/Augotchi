using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerQuest : Marker {

    protected override void executeEffect()
    {
        gc.queueRewardText("New Quest!", new Color(0.5f, 0.9f, 0.6f));

        PetKeeper.pet.questLog.Add(Quest.generateQuest());

        QuestUI.reRender = true;
        PetKeeper.pet.grantXP(50);
    }
}
