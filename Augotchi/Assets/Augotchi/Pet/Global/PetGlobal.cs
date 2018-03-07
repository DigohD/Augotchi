using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[System.Serializable]
public class PetGlobal {

    public PetVisualData petVisualData;

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

    public PetGlobal(float hunger, float happiness, float health, int candy, int food, int vegetables, long saveTimeStamp, long lastPettingTimeStamp, int currency, Marker.MarkerType[] markerSet, PetVisualData petVisualData)
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
    }

    public void degenerateTick()
    {
        Debug.Log("Happiness Pre-tick: " + happiness);

        long nowDT = System.DateTime.Now.Ticks;
        long diff = nowDT - saveTimeStamp;

        System.TimeSpan ts = new System.TimeSpan(diff);

        Debug.LogWarning(ts.ToString());

        for (int i = 0; i < ts.Days; i++)
        {
            degenerateDaily();
            Debug.LogWarning("DayTick");
        }

        for (int i = 0; i < ts.Hours; i++)
        {
            degenerateHourly();
            Debug.LogWarning("HourTick");
        }

        for (int i = 0; i < ts.Minutes; i++)
        {
            for (int j = 0; j < 6; j++)
                degenerate();
            Debug.LogWarning("MinuteTick");
        }

        for(int i = 0; i < (ts.Seconds / 10) - 1; i++)
        {
            degenerate();
            Debug.LogWarning("Tick");
        }

        degenerate();

        Debug.Log("Happiness Post-tick: " + happiness);

        Save(false);
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
        health -= 10f;
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

        hunger += 20f;
        health -= 2.5f;

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
        health += 5f;
        happiness -= 5f;

        vegetables--;

        Save(false);
    }

    public void hundredSteps()
    {
        health += 2f;
        hunger -= 0.5f;

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

    public void setMarkers(Marker.MarkerType[] markers)
    {
        this.markerSet = markers;

        Save(false);
    }

    public void Save(bool ignoreTime)
    {
        if(!ignoreTime)
            saveTimeStamp = System.DateTime.Now.Ticks;

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/AugotchiSave.gd");
        bf.Serialize(file, new PetGlobal(hunger, happiness, health, candy, food, vegetables, saveTimeStamp, lastPettingTimeStamp, currency, markerSet, petVisualData));
        file.Close();
    }

    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/AugotchiSave.gd"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/AugotchiSave.gd", FileMode.Open);
            PetGlobal pg = (PetGlobal) bf.Deserialize(file);

            Debug.LogWarning("Happiness on load: " + pg.happiness);

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

            Debug.LogWarning("LastSaveTimeStamp: " + saveTimeStamp );

            file.Close();

            degenerateTick();
        }
        else
        {
            hunger = 75;
            happiness = 50;
            health = 50;

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
}
