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

    public string imagePath;

    public int target;
    public int progress;

    public QuestRewardType rewardType;
    public string rewardImagePath;
    public int rewardAmount;

    public bool complete = false;


    /*
     * Find my missing X
     */
    public enum QuestType {
        GATHERING_COINS = 0,
        GATHERING_BUILDING_MATERIALS = 1,
        FIND_SEEDS = 2,
        GAIN_EXPERIENCE = 3,
        FEED_FOOD = 4,
        WALK = 5,
        HARVEST = 6,
        MARKERS = 7
    }

    public enum QuestRewardType
    {
        COINS = 0,
        BUILDING_MATERIALS = 1,
        EXPERIENCE = 2,
        GARDEN_DECOR = 3
    }

    public QuestType questType;

    public Quest(QuestType questType, int target, QuestRewardType rewardType, int reward, string rewardImagePath)
    {
        this.questType = questType;
        this.target = target;
        this.rewardAmount = reward;
        this.rewardType = rewardType;
        this.rewardImagePath = rewardImagePath;

        switch (questType)
        {
            case QuestType.GATHERING_BUILDING_MATERIALS:
                this.title = "Piling up!";
                this.description = "Gather building materials";
                break;
            case QuestType.GATHERING_COINS:
                this.title = "Coinspiration theory!";
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
            case QuestType.HARVEST:
                this.title = "Farm plot hot shot!";
                this.description = "Harvest produce from your farm plots a number of times.";
                break;
            case QuestType.MARKERS:
                this.title = "Mark my world!";
                this.description = "Find and activate a number of markers in the world.";
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
            case QuestType.HARVEST:
                PetKeeper.pet.OnFarmHarvest += this.OnQuestProgress;
                break;
            case QuestType.MARKERS:
                PetKeeper.pet.OnMarkerPicked += this.OnQuestProgress;
                break;
        }
    }

    private void OnQuestProgress(System.Object sender, EventArgs e)
    {
        progress += ((QuestEventArgs)e).progress;
        if(!complete && progress >= target)
        {
            progress = target;
            complete = true;

            GameControl.playPostMortemAudioClip((AudioClip) Resources.Load("Augotchi/Audio/Fanfare", typeof(AudioClip)));
        }

        QuestUI.reRender = true;
    }

    public static Quest generateQuest()
    {
        QuestRewardType rewardType = (QuestRewardType) UnityEngine.Random.Range(0, Enum.GetNames(typeof(QuestRewardType)).Length);

        QuestType questType = 0;
        bool questAlreadyInLog = false;
        do
        {
            questAlreadyInLog = false;

            questType = (QuestType)UnityEngine.Random.Range(0, Enum.GetNames(typeof(QuestType)).Length);

            foreach (Quest q in PetKeeper.pet.questLog)
            {
                if (q.questType == questType)
                    questAlreadyInLog = true;
            }
        } while (questAlreadyInLog);

        Quest toReturn = null;
        switch (questType)
        {
            case QuestType.FEED_FOOD:
                int rnd = UnityEngine.Random.Range(1, 4);
                toReturn = new Quest(
                    QuestType.FEED_FOOD,
                    rnd * 5,
                    rewardType,
                    rewardType == QuestRewardType.GARDEN_DECOR ? 1 : rnd * 20 * getRewardTypeConversionRate(rewardType),
                    getRewardTypeImagePath(rewardType)
                );
                break;

            case QuestType.FIND_SEEDS:
                rnd = UnityEngine.Random.Range(3, 7);
                toReturn = new Quest(
                    QuestType.FIND_SEEDS,
                    rnd * 4,
                    rewardType,
                    rewardType == QuestRewardType.GARDEN_DECOR ? 1 : rnd * 10 * getRewardTypeConversionRate(rewardType),
                    getRewardTypeImagePath(rewardType)
                );
                break;

            case QuestType.GAIN_EXPERIENCE:
                rnd = UnityEngine.Random.Range(2, 11);
                toReturn = new Quest(
                    QuestType.GAIN_EXPERIENCE,
                    rnd * 500,
                    rewardType,
                    rewardType == QuestRewardType.GARDEN_DECOR ? 1 : rnd * 10 * getRewardTypeConversionRate(rewardType),
                    getRewardTypeImagePath(rewardType)
                );
                break;

            case QuestType.GATHERING_BUILDING_MATERIALS:
                rnd = UnityEngine.Random.Range(1, 5);
                toReturn = new Quest(
                    QuestType.GATHERING_BUILDING_MATERIALS,
                    rnd * 25,
                    rewardType,
                    rewardType == QuestRewardType.GARDEN_DECOR ? 1 : rnd * 10 * getRewardTypeConversionRate(rewardType),
                    getRewardTypeImagePath(rewardType)
                );
                break;

            case QuestType.GATHERING_COINS:
                rnd = UnityEngine.Random.Range(1, 11);
                toReturn = new Quest(
                    QuestType.GATHERING_COINS,
                    rnd * 50,
                    rewardType,
                    rewardType == QuestRewardType.GARDEN_DECOR ? 1 : rnd * 10 * getRewardTypeConversionRate(rewardType),
                    getRewardTypeImagePath(rewardType)
                );
                break;

            case QuestType.WALK:
                rnd = UnityEngine.Random.Range(1, 7);
                toReturn = new Quest(
                    QuestType.WALK,
                    rnd * 500,
                    rewardType,
                    rewardType == QuestRewardType.GARDEN_DECOR ? 1 : rnd * 10 * getRewardTypeConversionRate(rewardType),
                    getRewardTypeImagePath(rewardType)
                );
                break;

            case QuestType.HARVEST:
                rnd = UnityEngine.Random.Range(3, 10);
                toReturn = new Quest(
                    QuestType.HARVEST,
                    rnd,
                    rewardType,
                    rewardType == QuestRewardType.GARDEN_DECOR ? 1 : rnd * 25 * getRewardTypeConversionRate(rewardType),
                    getRewardTypeImagePath(rewardType)
                );
                break;

            case QuestType.MARKERS:
                rnd = UnityEngine.Random.Range(3, 7);
                toReturn = new Quest(
                    QuestType.MARKERS,
                    rnd * 5,
                    rewardType,
                    rewardType == QuestRewardType.GARDEN_DECOR ? 1 : rnd * 5 * getRewardTypeConversionRate(rewardType),
                    getRewardTypeImagePath(rewardType)
                );
                break;
        }

        return toReturn;
    }

    public static int getRewardTypeConversionRate(QuestRewardType rewardType)
    {
        switch (rewardType)
        {
            case QuestRewardType.BUILDING_MATERIALS:
                return 1;
            case QuestRewardType.COINS:
                return 2;
            case QuestRewardType.EXPERIENCE:
                return 5;
        }

        return 1;
    }

    public static string getRewardTypeImagePath(QuestRewardType rewardType)
    {
        switch (rewardType)
        {
            case QuestRewardType.BUILDING_MATERIALS:
                return "Augotchi/Image/Reward/Icon_BM";
            case QuestRewardType.COINS:
                return "Augotchi/Image/Reward/Icon_Coin";
            case QuestRewardType.EXPERIENCE:
                return "Augotchi/Image/Reward/Icon_XP";
            case QuestRewardType.GARDEN_DECOR:
                return "Augotchi/Image/Reward/Icon_Decor";
        }

        return "Augotchi/Image/Whiskers";
    }




}
