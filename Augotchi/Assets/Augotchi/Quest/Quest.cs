using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest {

    public class QuestEventArgs : EventArgs
    {
        public int progress;

        public QuestEventArgs(int progress)
        {
            this.progress = progress;
        }
    }

    public string title;
    public string description;

    public int target;
    public int progress;

    public int reward;

    public bool complete = false;

    public enum QuestType { GATHERING_COINS, GATHERING_BUILDING_MATERIALS, FIND_SEEDS, GAIN_EXPERIENCE, FEED_FOOD, WALK }
    public QuestType questType;

    public Quest(QuestType questType, int target, int progress, int reward)
    {
        this.questType = questType;
        this.target = target;
        this.progress = progress;
        this.reward = reward;

        switch (questType)
        {
            case QuestType.GATHERING_BUILDING_MATERIALS:
                this.title = "Piling up!";
                this.description = "Gather building materials";
                break;
            case QuestType.GATHERING_COINS:
                this.title = "Treasure hunt!";
                this.description = "Gather a number of coins.";
                break;
            case QuestType.FEED_FOOD:
                this.title = "Gluttony!";
                this.description = "Feed your pet a number of food items.";
                break;
            case QuestType.FIND_SEEDS:
                this.title = "Need for seed!";
                this.description = "Gather seeds of any type.";
                break;
            case QuestType.GAIN_EXPERIENCE:
                this.title = "Leveling up!";
                this.description = "Gain a number of experience points.";
                break;
            case QuestType.WALK:
                this.title = "Walk in the park!";
                this.description = "Take a number of steps.";
                break;
        }

        initQuestListener();
    }

    public void initQuestListener()
    {
        switch (questType)
        {
            case QuestType.GATHERING_BUILDING_MATERIALS:
                PetKeeper.pet.OnBuildingMaterialsGained += this.OnQuestProgress;
                break;
            case QuestType.GATHERING_COINS:
                PetKeeper.pet.OnCoinsGained += this.OnQuestProgress;
                break;
            case QuestType.FEED_FOOD:
                PetKeeper.pet.OnFeeding += this.OnQuestProgress;
                break;
            case QuestType.FIND_SEEDS:
                PetKeeper.pet.OnSeedGained += this.OnQuestProgress;
                break;
            case QuestType.GAIN_EXPERIENCE:
                PetKeeper.pet.OnExperienceGained += this.OnQuestProgress;
                break;
            case QuestType.WALK:
                PetKeeper.pet.OnStepTaken += this.OnQuestProgress;
                break;
        }
    }

    private void OnQuestProgress(System.Object sender, EventArgs e)
    {
        progress += ((QuestEventArgs)e).progress;
        if(progress >= target)
        {
            progress = target;
            complete = true;
        }

        QuestUI.reRender = true;
    }
}
