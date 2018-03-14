﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[System.Serializable]
public class PetGlobal {

    public PetVisualData petVisualData;
    public PetUnlocksData petUnlocksData;

    readonly float MIN_HUNGER = 0, MAX_HUNGER = 100;
    readonly float MIN_HAPPINESS = 0, MAX_HAPPINESS = 100;
    readonly float MIN_HEALTH = 0, MAX_HEALTH = 100;

    public float hunger;
    public float happiness;
    public float health;

    public int candy;
    public int food;
    public int vegetables;

    public int currency;

    long saveTimeStamp;

    long lastPettingTimeStamp;

    public event EventHandler OnPetting;
    public event EventHandler OnFeedCannedFood;
    public event EventHandler OnFeedVegetables;
    public event EventHandler OnFeedCandy;

    public Marker.MarkerType[] markerSet;

    public bool isDead;
    public int reviveProgress;

    public int xp;
    public int level = 1;



    public PetGlobal()
    {
        
    }

    public PetGlobal(float hunger, float happiness, float health, int candy, int food, int vegetables)
    {
        this.hunger = hunger;
        this.happiness = happiness;
        this.health = health;

        this.candy = candy;
        this.food = food;
        this.vegetables = vegetables;
    }

    public PetGlobal(
        float hunger, 
        float happiness, 
        float health, 
        int candy, 
        int food, 
        int vegetables, 
        long saveTimeStamp, 
        long lastPettingTimeStamp, 
        int currency, 
        Marker.MarkerType[] markerSet, 
        PetVisualData petVisualData, 
        PetUnlocksData petUnlocksData,
        bool isDead, 
        int reviveProgress,
        int xp,
        int level
        )
    {
        this.hunger = hunger;
        this.happiness = happiness;
        this.health = health;

        this.candy = candy;
        this.food = food;
        this.vegetables = vegetables;

        this.currency = currency;

        this.lastPettingTimeStamp = lastPettingTimeStamp;

        this.saveTimeStamp = saveTimeStamp;

        this.markerSet = markerSet;

        this.petVisualData = petVisualData;
        this.petUnlocksData = petUnlocksData;

        this.isDead = isDead;
        this.reviveProgress = reviveProgress;

        this.xp = xp;
        this.level = level;
    }

    public void degenerateTick()
    {
        if (isDead)
            return;

        long nowDT = System.DateTime.Now.Ticks;
        long diff = nowDT - saveTimeStamp;

        System.TimeSpan ts = new System.TimeSpan(diff);

        int xpToReceive = 0;

        for (int i = 0; i < ts.Days; i++)
        {
            degenerateDaily();

            if(happiness > 75)
            {
                xpToReceive += 1 * 6 * 60 * 24;
            }
        }

        for (int i = 0; i < ts.Hours; i++)
        {
            degenerateHourly();

            if (happiness > 75)
            {
                xpToReceive += 1 * 6 * 60;
            }
        }

        for (int i = 0; i < ts.Minutes; i++)
        {
            for (int j = 0; j < 6; j++)
                degenerate();

            if (happiness > 75)
            {
                xpToReceive += 1 * 6;
            }
        }

        for(int i = 0; i < (ts.Seconds / 10) - 1; i++)
        {
            degenerate();

            if (happiness > 75)
            {
                xpToReceive += 1;
            }
        }

        degenerate();

        if (happiness > 75)
        {
            xpToReceive += 1;
        }

        if(xpToReceive > 0)
            grantXP(xpToReceive);

        if (health <= 0 || hunger <= 0 || happiness <= 0)
        {
            die();
        }

        Save(false);
    }

    public void die()
    {
        isDead = true;
        reviveProgress = 0;

        hunger = 0;
        happiness = 0;
        health = 0;

        Save(false);
    }

    public void addReviveProgress()
    {
        reviveProgress++;
        if (reviveProgress >= 10)
        {
            revive();
        }
    }

    private void revive()
    {
        isDead = false;
        reviveProgress = 0;

        hunger = 25;
        happiness = 25;
        health = 25;
    }

    public void degenerate()
    {
        if(hunger > 75)
        {
            hunger -= 0.005f;
        }
        if(hunger < 25)
        {
            hunger += 0.005f;
        }
        hunger -= 0.01f;

        if (hunger > 75)
        {
            health -= 0.01f;
        }
        if (hunger < 25)
        {
            health -= 0.01f;
        }
        health -= 0.003f;

        if (hunger < 25)
        {
            happiness -= 0.02f;
        }
        if (health < 25)
        {
            happiness -= 0.02f;
        }

        if (hunger > 75)
        {
            happiness += 0.01f;
        }
        if (health > 75)
        {
            happiness += 0.01f;
        }
        happiness -= 0.005f;

        if (health < 0)
            health = 0;
        if (health > 100)
            health = 100;

        if (hunger < 0)
            hunger = 0;
        if (hunger > 100)
            hunger = 100;

        if (happiness < 0)
            happiness = 0;
        if (happiness > 100)
            happiness = 100;

        if(happiness >= 75)
        {
            
        }
    }

    private void degenerateHourly()
    {
        if (hunger > 75)
        {
            hunger -= (0.005f * 360);
        }
        if (hunger < 25)
        {
            hunger += (0.005f * 360);
        }
        hunger -= (0.01f * 360);

        if (hunger > 75)
        {
            health -= (0.01f * 360);
        }
        if (hunger < 25)
        {
            health -= (0.01f * 360);
        }
        health -= (0.003f * 360);

        if (hunger < 25)
        {
            happiness -= (0.02f * 360);
        }
        if (health < 25)
        {
            happiness -= (0.02f * 360);
        }

        if (hunger > 75)
        {
            happiness += (0.01f * 360);
        }
        if (health > 75)
        {
            happiness += (0.01f * 360);
        }
        happiness -= (0.005f * 360);
    }

    private void degenerateDaily()
    {
        if (hunger > 75)
        {
            hunger -= (0.005f * 360 * 24);
        }
        if (hunger < 25)
        {
            hunger += (0.005f * 360 * 24);
        }
        hunger -= (0.01f * 360 * 24);

        if (hunger > 75)
        {
            health -= (0.01f * 360 * 24);
        }
        if (hunger < 25)
        {
            health -= (0.01f * 360 * 24);
        }
        health -= (0.003f * 360 * 24);

        if (hunger < 25)
        {
            happiness -= (0.02f * 360 * 24);
        }
        if (health < 25)
        {
            happiness -= (0.02f * 360 * 24);
        }

        if (hunger > 75)
        {
            happiness += (0.01f * 360 * 24);
        }
        if (health > 75)
        {
            happiness += (0.01f * 360 * 24);
        }
        happiness -= (0.005f * 360 * 24);
    }

    public void feedCandy()
    {
        if (candy <= 0)
            return;

        if (OnFeedCandy != null)
            OnFeedCandy(this, new EventArgs());

        happiness += 15f;
        health -= 7.5f;
        hunger += 5f;

        candy--;

        Save(false);
    }

    public void feedFood()
    {
        if (food <= 0)
            return;

        if (OnFeedCannedFood != null)
            OnFeedCannedFood(this, new EventArgs());

        hunger += 25f;

        food--;

        Save(false);
    }

    public void feedVegetables()
    {
        if (vegetables <= 0)
            return;

        if (OnFeedVegetables != null)
            OnFeedVegetables(this, new EventArgs());

        hunger += 10f;
        health += 10f;
        happiness -= 2.5f;

        vegetables--;

        Save(false);
    }

    public void clearFeedingListeners()
    {
        OnFeedCandy = null;
        OnFeedVegetables = null;
        OnFeedCannedFood = null;
        OnPetting = null;
    }

    public void hundredSteps()
    {
        health += 2f;

        Save(false);
    }

    public void giveCurrency(int amount)
    {
        currency += amount;

        Save(false);
    }

    public void takeCurrency(int amount)
    {
        currency -= amount;

        Save(false);
    }

    public void petPet()
    {
        long nowDT = System.DateTime.Now.Ticks;
        long diff = nowDT - lastPettingTimeStamp;

        System.TimeSpan ts = new System.TimeSpan(diff);

        if(ts.Hours > 0 || ts.Days > 0)
        {
            if (OnPetting != null)
                OnPetting(this, new EventArgs());

            lastPettingTimeStamp = nowDT;

            happiness += 5f;

            Save(false);
        }
    }

    public void addHunger(int amount)
    {
        hunger += amount;

        Save(false);
    }

    public void addHappiness(int amount)
    {
        happiness += amount;

        Save(false);
    }

    public void addHealth(int amount)
    {
        health += amount;

        Save(false);
    }

    public void grantXP(int amount)
    {
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameControl>().queueRewardText("XP: +" + amount, GameControl.XPColor);
        xp += amount;

        int nextLevel = level + 1;
        int nextLevelXP = xpToNextLevel(level);
        while(xp >= nextLevelXP)
        {
            xp -= nextLevelXP;
            level++;
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameControl>().queueRewardText("Level Up!", GameControl.LevelUpColor);
            nextLevelXP = xpToNextLevel(level);
        }

        Save(false);
    }

    public void setMarkers(Marker.MarkerType[] markers)
    {
        this.markerSet = markers;

        Save(false);
    }

    public void Save(bool ignoreTime)
    {
        if(!ignoreTime)
            saveTimeStamp = System.DateTime.Now.Ticks;

        if (petUnlocksData == null)
            petUnlocksData = new PetUnlocksData();

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/AugotchiSave.gd");
        bf.Serialize(file, 
            new PetGlobal(
                hunger, 
                happiness, 
                health, 
                candy, 
                food, 
                vegetables, 
                saveTimeStamp, 
                lastPettingTimeStamp, 
                currency, 
                markerSet, 
                petVisualData, 
                petUnlocksData,
                isDead, 
                reviveProgress,
                xp,
                level));
        file.Close();
    }

    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/AugotchiSave.gd"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/AugotchiSave.gd", FileMode.Open);
            PetGlobal pg = (PetGlobal) bf.Deserialize(file);

            hunger = pg.hunger;
            happiness = pg.happiness;
            health = pg.health;

            candy = pg.candy;
            food = pg.food;
            vegetables = pg.vegetables;

            currency = pg.currency;

            saveTimeStamp = pg.saveTimeStamp;

            lastPettingTimeStamp = pg.lastPettingTimeStamp;

            markerSet = pg.markerSet;

            petVisualData = pg.petVisualData;

            petUnlocksData = pg.petUnlocksData;
            if(petUnlocksData.unlockedHats.Length < PetUnlocksData.hatCounts.Length)
            {
                int[] expandedArray = new int[PetUnlocksData.hatCounts.Length];
                for(int i = 0; i < petUnlocksData.unlockedHats.Length; i++)
                {
                    expandedArray[i] = petUnlocksData.unlockedHats[i];
                }
                petUnlocksData.unlockedHats = expandedArray;
            }

            if (petUnlocksData.unlockedFaces.Length < PetUnlocksData.faceCounts.Length)
            {
                int[] expandedArray = new int[PetUnlocksData.faceCounts.Length];
                for (int i = 0; i < petUnlocksData.unlockedFaces.Length; i++)
                {
                    expandedArray[i] = petUnlocksData.unlockedFaces[i];
                }
                petUnlocksData.unlockedFaces = expandedArray;
            }

            isDead = pg.isDead;
            reviveProgress = pg.reviveProgress;

            xp = pg.xp;
            level = pg.level;

            if (level == 0)
                level = 1;

            file.Close();

            degenerateTick();
        }
        else
        {
            hunger = 75;
            happiness = 50;
            health = 50;

            level = 1;

            Save(false);
        }
    }

    public void ResetPet()
    {
        hunger = 75;
        happiness = 50;
        health = 50;

        candy = 5;
        food = 5;
        vegetables = 5;

        currency = 0;

        lastPettingTimeStamp = 0;

        isDead = false;
        reviveProgress = 0;

        Save(false);
    }

    public PetVisualData LoadVisuals()
    {
        if (File.Exists(Application.persistentDataPath + "/AugotchiSave.gd"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/AugotchiSave.gd", FileMode.Open);
            PetGlobal pg = (PetGlobal)bf.Deserialize(file);

            file.Close();

            if (pg.petVisualData != null)
                return pg.petVisualData;
            else
                return new PetVisualData();
        }
        else
        {
            return new PetVisualData();
        }
    }

    public PetUnlocksData LoadUnlocks()
    {
        if (File.Exists(Application.persistentDataPath + "/AugotchiSave.gd"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/AugotchiSave.gd", FileMode.Open);
            PetGlobal pg = (PetGlobal)bf.Deserialize(file);

            file.Close();



            if (pg.petUnlocksData != null)
            {
                if (pg.petUnlocksData.unlockedHats.Length < PetUnlocksData.hatCounts.Length)
                {
                    int[] expandedArray = new int[PetUnlocksData.hatCounts.Length];
                    for (int i = 0; i < pg.petUnlocksData.unlockedHats.Length; i++)
                    {
                        expandedArray[i] = pg.petUnlocksData.unlockedHats[i];
                    }
                    pg.petUnlocksData.unlockedHats = expandedArray;
                }

                if (pg.petUnlocksData.unlockedFaces.Length < PetUnlocksData.faceCounts.Length)
                {
                    int[] expandedArray = new int[PetUnlocksData.faceCounts.Length];
                    for (int i = 0; i < pg.petUnlocksData.unlockedFaces.Length; i++)
                    {
                        expandedArray[i] = pg.petUnlocksData.unlockedFaces[i];
                    }
                    pg.petUnlocksData.unlockedFaces = expandedArray;
                }

                return pg.petUnlocksData;
            }
            else
                return new PetUnlocksData();
        }
        else
        {
            return new PetUnlocksData();
        }
    }

    public void SaveVisuals(PetVisualData petVisualData)
    {
        if (File.Exists(Application.persistentDataPath + "/AugotchiSave.gd"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/AugotchiSave.gd", FileMode.Open);
            PetGlobal pg = (PetGlobal)bf.Deserialize(file);

            hunger = pg.hunger;
            happiness = pg.happiness;
            health = pg.health;

            candy = pg.candy;
            food = pg.food;
            vegetables = pg.vegetables;

            currency = pg.currency;

            saveTimeStamp = pg.saveTimeStamp;

            lastPettingTimeStamp = pg.lastPettingTimeStamp;

            markerSet = pg.markerSet;

            petUnlocksData = pg.petUnlocksData;

            isDead = pg.isDead;
            reviveProgress = pg.reviveProgress;

            xp = pg.xp;
            level = pg.level;

            file.Close();

            this.petVisualData = petVisualData;

            Save(true);
        }
        else
        {
            hunger = 75;
            happiness = 50;
            health = 50;

            this.petVisualData = petVisualData;

            Save(false);
        }
    }

    public float getXpRatio()
    {
        float xp = (float) this.xp;
        float xpToNext = xpToNextLevel(level);
        return xp / xpToNext;
    }

    public int xpToNextLevel(int level)
    {
        switch (level)
        {
            case 1:
                return 100;
            case 2:
                return 200;
            case 3:
                return 300;
            case 4:
                return 400;
        }

        int xpRequired = (int) (level * level / 0.05f);
        int xpReuired = xpRequired + (xpRequired % 100);
        return xpRequired;
    }

    public bool unlockHat(int hatIndex, int hatVariation)
    {
        bool categoryHadEntries = petUnlocksData.hatCategoryHasUnlocks(hatIndex);
        int currentEquippedIndex = petUnlocksData.currentEquippedHatAbsoluteIndex(petVisualData.hatIndex);
        bool isNewHat = petUnlocksData.unlockHat(hatIndex, hatVariation);
        if (isNewHat)
        {
            if(hatIndex < currentEquippedIndex && !categoryHadEntries)
            {
                petVisualData.hatIndex++;
            }
        }

        return isNewHat;
    }

    public bool unlockFace(int faceIndex, int faceVariation)
    {
        bool categoryHadEntries = petUnlocksData.faceCategoryHasUnlocks(faceIndex);
        int currentEquippedIndex = petUnlocksData.currentEquippedFaceAbsoluteIndex(petVisualData.faceIndex);
        bool isNewFace = petUnlocksData.unlockFace(faceIndex, faceVariation);
        if (isNewFace)
        {
            if (faceIndex < currentEquippedIndex && !categoryHadEntries)
            {
                petVisualData.faceIndex++;
            }
        }

        return isNewFace;
    }
}
