using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[System.Serializable]
public class PetGlobal {

    readonly float MIN_HUNGER = 0, MAX_HUNGER = 100;
    readonly float MIN_HAPPINESS = 0, MAX_HAPPINESS = 100;
    readonly float MIN_HEALTH = 0, MAX_HEALTH = 100;

    public float hunger;
    public float happiness;
    public float health;

    long saveTimeStamp;

    public PetGlobal()
    {
        
    }

    public PetGlobal(float hunger, float happiness, float health)
    {
        this.hunger = hunger;
        this.happiness = happiness;
        this.health = health;
    }

    public PetGlobal(float hunger, float happiness, float health, long saveTimeStamp)
    {
        this.hunger = hunger;
        this.happiness = happiness;
        this.health = health;

        this.saveTimeStamp = saveTimeStamp;
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

        Save();
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
            happiness += 0.005f;
        }
        if (health > 75)
        {
            happiness += 0.005f;
        }
        happiness -= 0.01f;
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
            happiness += (0.005f * 360);
        }
        if (health > 75)
        {
            happiness += (0.005f * 360);
        }
        happiness -= (0.01f * 360);
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
            happiness += (0.005f * 360 * 24);
        }
        if (health > 75)
        {
            happiness += (0.005f * 360 * 24);
        }
        happiness -= (0.01f * 360 * 24);
    }

    public void Save()
    {
        saveTimeStamp = System.DateTime.Now.Ticks;

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/AugotchiSave.gd");
        bf.Serialize(file, new PetGlobal(hunger, happiness, health, saveTimeStamp));
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

            saveTimeStamp = pg.saveTimeStamp;

            Debug.LogWarning("LastSaveTimeStamp: " + saveTimeStamp );

            file.Close();

            degenerateTick();
        }
        else
        {
            hunger = 75;
            happiness = 50;
            health = 50;

            Save();
        }
    }
}
