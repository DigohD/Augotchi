using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[System.Serializable]
public class PetGlobal {

    public PetVisualData petVisualData;
    public PetUnlocksData petUnlocksData;
    public Inventory inventory;
    public Base Base;

    public List<Quest> questLog;

    readonly float MIN_HUNGER = 0, MAX_HUNGER = 100;
    readonly float MIN_HAPPINESS = 0, MAX_HAPPINESS = 100;
    readonly float MIN_HEALTH = 0, MAX_HEALTH = 100;

    readonly float DECAY_HUNGER = 0.004f;
    readonly float DECAY_HAPPINESS = 0.003f;
    readonly float DECAY_HEALTH = 0.003f;

    public float hunger;
    public float happiness;
    public float health;

    public int markersCurrency;
    public int markersFood;
    public int markersCrate;
    public int markersRevive;

    public int petDeathCount;
    public int petRevivalCount;

    public int pettingCount;

    public int candy;
    public int food;
    public int vegetables;

    public int candyFed;
    public int foodFed;
    public int vegetableFed;

    public int currency;
    public int buildingMaterials;
    public int puffles = 100;

    long saveTimeStamp;

    long lastPettingTimeStamp;

    // THESE LISTENERS ARE NOT USED AT THIS POINT! LEAVE FOR SERIALIZING ERROR PREVENTION!

    public event EventHandler OnPetting;
    public event EventHandler OnFeedCannedFood;
    public event EventHandler OnFeedVegetables;
    public event EventHandler OnFeedCandy;

    // END OF GARBAGE LISTENERS!

    public event EventHandler OnBuildingMaterialsGained;
    public event EventHandler OnCoinsGained;
    public event EventHandler OnSeedGained;
    public event EventHandler OnStepTaken;
    public event EventHandler OnFeeding;
    public event EventHandler OnExperienceGained;
    public event EventHandler OnFarmHarvest;
    public event EventHandler OnMarkerPicked;

    public Marker.MarkerType[] markerSet;
    public float[] markerRelativeDistances;

    public bool isDead;
    public int reviveProgress;

    public int xp;
    public int level = 1;

    public int stepCounter;
    public int activeTicks;
    public int inactiveTicks;

    public int startAppCount;

    public int currentAliveTicks;
    public int longestAliveTicks;

    public string name;

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
        int buildingMaterials,
        Marker.MarkerType[] markerSet, 
        PetVisualData petVisualData, 
        PetUnlocksData petUnlocksData,
        bool isDead, 
        int reviveProgress,
        int xp,
        int level,
        int markersCurrency,
        int markersFood,
        int markersCrate,
        int markersRevive,
        int petDeathCount,
        int pettingCount,
        int candyFed,
        int foodFed,
        int vegetableFed,
        int startAppCount,
        int stepCounter,
        int activeTicks,
        int inactiveTicks,
        int petRevivalCount,
        int currentAliveTicks,
        int longestAliveTicks,
        string name,
        Inventory inventory,
        List<Quest> questLog,
        Base Base
        )
    {
        this.hunger = hunger;
        this.happiness = happiness;
        this.health = health;

        this.candy = candy;
        this.food = food;
        this.vegetables = vegetables;

        this.currency = currency;
        this.buildingMaterials = buildingMaterials;

        this.lastPettingTimeStamp = lastPettingTimeStamp;

        this.saveTimeStamp = saveTimeStamp;

        this.markerSet = markerSet;
        this.markerRelativeDistances = markerRelativeDistances;

        this.petVisualData = petVisualData;
        this.petUnlocksData = petUnlocksData;

        this.isDead = isDead;
        this.reviveProgress = reviveProgress;

        this.xp = xp;
        this.level = level;

        this.markersCurrency = markersCurrency;
        this.markersFood = markersFood;
        this.markersCrate = markersCrate;
        this.markersRevive = markersRevive;

        this.petDeathCount = petDeathCount;

        this.pettingCount = pettingCount;

        this.candyFed = candyFed;
        this.foodFed = foodFed;
        this.vegetableFed = vegetableFed;

        this.startAppCount = startAppCount;

        this.stepCounter = stepCounter;
        this.activeTicks = activeTicks;
        this.inactiveTicks = inactiveTicks;

        this.petRevivalCount = petRevivalCount;

        this.currentAliveTicks = currentAliveTicks;
        this.longestAliveTicks = longestAliveTicks;

        this.name = name;

        this.inventory = inventory;
        this.questLog = questLog;

        this.Base = Base;
    }

    private void initListeners()
    {
        
    }

    public void degenerateTick()
    {
        if (isDead)
        {
            Save(false);
            return;
        }

        long nowDT = System.DateTime.Now.Ticks;
        long diff = nowDT - saveTimeStamp;

        System.TimeSpan ts = new System.TimeSpan(diff);

        int xpToReceive = 0;

        for (int i = 0; i < ts.Days; i++)
        {
            degenerateDaily();
            inactiveTicks += (6*60*24);
            currentAliveTicks += (6 * 60 * 24);

            if (happiness > 75)
            {
                xpToReceive += 1 * 6 * 60 * 24;
            }
        }

        for (int i = 0; i < ts.Hours; i++)
        {
            degenerateHourly();
            inactiveTicks += (6 * 60);
            currentAliveTicks += (6 * 60);

            if (happiness > 75)
            {
                xpToReceive += 1 * 6 * 60;
            }
        }

        for (int i = 0; i < ts.Minutes; i++)
        {
            for (int j = 0; j < 6; j++)
                degenerate();

            inactiveTicks += 6;
            currentAliveTicks += 6;

            if (happiness > 75)
            {
                xpToReceive += 1 * 6;
            }
        }

        for(int i = 0; i < (ts.Seconds / 10) - 1; i++)
        {
            degenerate();

            inactiveTicks++;
            currentAliveTicks++;

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

        currentAliveTicks++;

        if (currentAliveTicks > longestAliveTicks)
        {
            longestAliveTicks = currentAliveTicks;
        }

        if (health <= 0 || hunger <= 0 || happiness <= 0)
        {
            die();
        }

        //die();

        activeTicks++;
        

        Save(false);
    }

    public void die()
    {
        isDead = true;
        reviveProgress = 0;

        petDeathCount++;

        currentAliveTicks = 0;

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

        Save(false);
    }

    public void revive()
    {
        isDead = false;
        reviveProgress = 0;

        petRevivalCount++;

        hunger = 25;
        happiness = 25;
        health = 25;

        Save(false);
    }

    public void degenerate()
    {
        hunger -= DECAY_HUNGER;
        health -= DECAY_HEALTH;

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
        happiness -= DECAY_HAPPINESS;

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
    }

    private void degenerateHourly()
    {
        hunger -= (DECAY_HUNGER * 360);
        health -= (DECAY_HEALTH * 360);

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
        happiness -= (DECAY_HAPPINESS * 360);
    }

    private void degenerateDaily()
    {
        hunger -= (DECAY_HUNGER * 360 * 24);
        health -= (DECAY_HEALTH * 360 * 24);

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
        happiness -= (DECAY_HAPPINESS * 360 * 24);
    }

    public void feed(float hunger, float health, float happiness)
    {
        addHunger(hunger);
        addHealth(health);
        addHappiness(happiness);

        if (this.OnFeeding != null)
            this.OnFeeding(this, new Quest.QuestEventArgs(1));

        Save(false);
    }

    public void onMarkerPicked()
    {
        if (this.OnMarkerPicked != null)
            this.OnMarkerPicked(this, new Quest.QuestEventArgs(1));
    }

    public void clearAllListeners()
    {

    }

    public void addStep()
    {
        PetKeeper.pet.stepCounter++;

        if(this.OnStepTaken != null)
            this.OnStepTaken(this, new Quest.QuestEventArgs(1));
    }

    public void hundredSteps()
    {
        health += 2f;

        Save(false);
    }

    public void addSeed(Inventory.SeedType seedType, int amount)
    {
        inventory.seedCounts[(int) seedType] += amount;

        if(this.OnSeedGained != null)
            this.OnSeedGained(this, new Quest.QuestEventArgs(amount));

        Save(false);
    }

    public void addFarmProduce(Inventory.ProduceType produceType, int amount)
    {
        inventory.produceCounts[(int) produceType] += amount;

        if (this.OnFarmHarvest != null)
            this.OnFarmHarvest(this, new Quest.QuestEventArgs(1));

        Save(false);
    }

    public void giveCurrency(int amount)
    {
        currency += amount;

        if(this.OnCoinsGained != null)
            this.OnCoinsGained(this, new Quest.QuestEventArgs(amount));

        Save(false);
    }

    public void giveBuildingMaterials(int amount)
    {
        buildingMaterials += amount;

        if(this.OnBuildingMaterialsGained != null)
            this.OnBuildingMaterialsGained(this, new Quest.QuestEventArgs(amount));

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
            lastPettingTimeStamp = nowDT;

            happiness += 5f;

            pettingCount++;

            Save(false);
        }
    }

    public void addHunger(float amount)
    {
        hunger += amount;

        Save(false);
    }

    public void addHappiness(float amount)
    {
        happiness += amount;

        Save(false);
    }

    public void addHealth(float amount)
    {
        health += amount;

        Save(false);
    }

    public void grantXP(int amount)
    {
        if(amount > 1)
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

            giveBuildingMaterials(level * 10);
            giveCurrency(level * 10);

            //addRandomLootItem();
        }

        if(this.OnExperienceGained != null)
            this.OnExperienceGained(this, new Quest.QuestEventArgs(amount));

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

        if (Base == null)
            Base = new Base();

        if (inventory == null)
            inventory = new Inventory();

        if(questLog == null)
            questLog = new List<Quest>();

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
                buildingMaterials,
                markerSet,
                petVisualData, 
                petUnlocksData,
                isDead, 
                reviveProgress,
                xp,
                level,
                markersCurrency,
                markersFood,
                markersCrate,
                markersRevive,
                petDeathCount,
                pettingCount,
                candyFed,
                foodFed,
                vegetableFed,
                startAppCount,
                stepCounter,
                activeTicks,
                inactiveTicks,
                petRevivalCount,
                currentAliveTicks,
                longestAliveTicks,
                name,
                inventory,
                questLog,
                Base
            ));
        file.Close();

        TestPetToDatabase.postData = true;

        PetNotificationHandeler.PlanNotification(this);
    }

    public bool Load()
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
            buildingMaterials = pg.buildingMaterials;

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

            markersCurrency = pg.markersCurrency;
            markersFood = pg.markersFood;
            markersCrate = pg.markersCrate;
            markersRevive = pg.markersRevive;

            petDeathCount = pg.petDeathCount;
            petRevivalCount = pg.petRevivalCount;

            pettingCount = pg.pettingCount;
            candyFed = pg.candyFed;
            foodFed = pg.foodFed;
            vegetableFed = pg.vegetableFed;

            startAppCount = pg.startAppCount;

            stepCounter = pg.stepCounter;

            activeTicks = pg.activeTicks;
            inactiveTicks = pg.inactiveTicks;

            currentAliveTicks = pg.currentAliveTicks;
            longestAliveTicks = pg.longestAliveTicks;

            name = pg.name;

            if (pg.inventory != null)
                this.inventory = new Inventory(
                    pg.inventory.seedCounts == null ? new int[0] : pg.inventory.seedCounts,
                    pg.inventory.produceCounts == null ? new int[0] : pg.inventory.produceCounts,
                    pg.inventory.uniqueCounts == null ? new int[0] : pg.inventory.uniqueCounts
                );
            else
                this.inventory = new Inventory();

            this.questLog = pg.questLog == null ? new List<Quest>() : pg.questLog;

            foreach (Quest q in questLog)
                q.initQuestListener();

            this.Base = pg.Base;

            file.Close();

            TestPetToDatabase.postData = true;

            degenerateTick();

            return true;
        }
        else
        {
            hunger = 75;
            health = 75;
            happiness = 75;

            level = 1;

            Save(false);

            return false;
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
        buildingMaterials = 0;

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
            buildingMaterials = pg.buildingMaterials;

            saveTimeStamp = pg.saveTimeStamp;

            lastPettingTimeStamp = pg.lastPettingTimeStamp;

            markerSet = pg.markerSet;

            petUnlocksData = pg.petUnlocksData;

            isDead = pg.isDead;
            reviveProgress = pg.reviveProgress;

            xp = pg.xp;
            level = pg.level;

            markersCurrency = pg.markersCurrency;
            markersFood = pg.markersFood;
            markersCrate = pg.markersCrate;
            markersRevive = pg.markersRevive;

            petDeathCount = pg.petDeathCount;
            petRevivalCount = pg.petRevivalCount;

            pettingCount = pg.pettingCount;
            candyFed = pg.candyFed;
            foodFed = pg.foodFed;
            vegetableFed = pg.vegetableFed;

            startAppCount = pg.startAppCount;

            stepCounter = pg.stepCounter;

            activeTicks = pg.activeTicks;
            inactiveTicks = pg.inactiveTicks;

            currentAliveTicks = pg.currentAliveTicks;
            longestAliveTicks = pg.longestAliveTicks;

            name = pg.name;

            file.Close();

            this.petVisualData = petVisualData;

            Save(true);
        }
        else
        {
            hunger = 75;
            happiness = 50;
            health = 50;

            level = 1;

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

        int xpRequired = (int) (level * level * 20);
        xpRequired = xpRequired + (100 - (xpRequired % 100));
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

    public void unlockUniqueHat(int hatIndex, int hatVariation)
    {
        GameObject gControl = GameObject.FindGameObjectWithTag("GameController");
        GameControl gc = null;
        if (gControl)
        {
            gc = gControl.GetComponent<GameControl>();
        }

        int hatsVariationIndex = UnityEngine.Random.Range(0, PetUnlocksData.hatCounts[hatIndex]);
        bool isNewHat = unlockHat(hatIndex, hatsVariationIndex);
        if (isNewHat)
        {
            if (gc != null)
                gc.queueRewardText("Unique Hat!", new Color(1, 0.3f, 1f));
        }
    }

    public void addRandomLootItem()
    {
        GameObject gControl = GameObject.FindGameObjectWithTag("GameController");
        GameControl gc = null;
        if (gControl)
        {
            gc = gControl.GetComponent<GameControl>();
        }

        switch (UnityEngine.Random.Range(0, 2))
        {
            case 0:
                int hatIndex = 0;

                // Do until it's not poohat
                do
                {
                    hatIndex = UnityEngine.Random.Range(1, petUnlocksData.unlockedHats.Length);
                } while (hatIndex == 14);

                int hatsVariationIndex = UnityEngine.Random.Range(0, PetUnlocksData.hatCounts[hatIndex]);
                bool isNewHat = unlockHat(hatIndex, hatsVariationIndex);
                if (isNewHat)
                {
                    if(gc != null)
                        gc.queueRewardText("New Hat!", new Color(0, 0.8f, 0.7f));
                }
                else
                {
                    int money = (UnityEngine.Random.Range(5, 10) * 10);
                    giveCurrency(money);
                    if (gc != null)
                    {
                        gc.queueRewardText("Duplicate Hat...", new Color(0, 0.8f, 0.7f));
                        gc.queueRewardText("Coins: +" + money, new Color(1, 0.85f, 0.2f));
                    }
                }
                break;
            case 1:
                int faceIndex = UnityEngine.Random.Range(1, petUnlocksData.unlockedFaces.Length);
                int faceVariationIndex = UnityEngine.Random.Range(0, PetUnlocksData.faceCounts[faceIndex]);
                bool isNewFace = unlockFace(faceIndex, faceVariationIndex);
                if (isNewFace)
                {
                    if(gc != null)
                        gc.queueRewardText("New Facial\nFeature!", new Color(0, 0.8f, 0.7f));
                }
                else
                {
                    int money = (UnityEngine.Random.Range(5, 10) * 10);
                    PetKeeper.pet.giveCurrency(money);
                    if (gc != null)
                    {
                        gc.queueRewardText("Duplicate Facial\nfeature...", new Color(0, 0.8f, 0.7f));
                        gc.queueRewardText("Coins: +" + money, new Color(1, 0.85f, 0.2f));
                    }
                }
                break;
        }
    }
}
