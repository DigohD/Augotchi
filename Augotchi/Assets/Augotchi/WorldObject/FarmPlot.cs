using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FarmPlot : MonoBehaviour {

    TimeSpan timeLeft;
    GameObject G_TimerLabel;

    Transform cameraT;
    public Transform T_TimerLabel;
    public Transform T_FinishedLabel;

    protected GardenCrop representedCrop;
    private int initialGrowthTime;

    protected GameControl gc;

    private float delayedSpawnTime;
    private bool hasSpawned;
    float timer = 0;
    float spawnTimer = 0;

    void Start()
    {
        transform.localScale = Vector3.zero;

        updateVisuals(1 - ((float)timeLeft.TotalSeconds / (float)initialGrowthTime));
    }

    public virtual void init(Seed seedInfo, GardenCrop gardenCrop, GameControl gc, bool onPlant)
    {
        if (onPlant)
            delayedSpawnTime = 0;
        else
            delayedSpawnTime = UnityEngine.Random.Range(0.5f, 1.5f);

        initialGrowthTime = seedInfo.growthTime;

        long growthTimeTicks = TimeSpan.TicksPerSecond * seedInfo.growthTime;
        long finishedTimeStamp = gardenCrop.plantedTimeStamp + growthTimeTicks;

        if (DateTime.Now.Ticks > finishedTimeStamp)
        {
            T_FinishedLabel.gameObject.SetActive(true);
        }
        else
        {
            TimeSpan diff = new TimeSpan(DateTime.Now.Ticks - finishedTimeStamp);
            timeLeft = diff.Duration();

            T_TimerLabel.gameObject.SetActive(true);

            setLabel();
        }

        representedCrop = gardenCrop;

        cameraT = GameObject.FindGameObjectWithTag("MainCamera").transform.parent;

        this.gc = gc;

        timer = 0;
    }

    void FixedUpdate () {
        T_TimerLabel.localRotation = cameraT.rotation;
        T_FinishedLabel.localRotation = cameraT.rotation;

        if (timeLeft.TotalSeconds <= 0)
        {
            T_TimerLabel.gameObject.SetActive(false);
            T_FinishedLabel.gameObject.SetActive(true);
        }

        timer += Time.fixedDeltaTime;
        if(timer >= 1)
        {
            timer -= 1;
            timeLeft = timeLeft.Subtract(new TimeSpan(0, 0, 1));
            setLabel();
            updateVisuals(1 - ((float) timeLeft.TotalSeconds / (float) initialGrowthTime));
        }

        spawnTimer += Time.fixedDeltaTime;
        if (!hasSpawned && spawnTimer > delayedSpawnTime)
        {
            float spawnTimer2 = spawnTimer - delayedSpawnTime;

            float scale = Mathf.Sin(Mathf.PI * spawnTimer2);

            if (spawnTimer2 >= 0.5f)
            {
                scale = 1;
                hasSpawned = true;
            }

            transform.localScale = Vector3.one * scale;
        }
    }

    public virtual void onPress()
    {
        PetKeeper.pet.Base.gardenCrops.Remove(representedCrop);

        Destroy(gameObject);

        InventoryUI.reRender = true;

        PetKeeper.pet.Save(false);
    }

    private void setLabel()
    {
        string timeString = "";

        if (timeLeft.Hours > 0)
        {
            timeString += timeLeft.Hours + ":";
        }

        if (timeLeft.Minutes > 9)
        {
            timeString += timeLeft.Minutes + ":";
        }
        else if (timeLeft.Minutes > 0)
        {
            timeString += "0" + timeLeft.Minutes + ":";
        }
        else
        {
            timeString += "00:";
        }

        if (timeLeft.Seconds > 9)
        {
            timeString += timeLeft.Seconds;
        }
        else if (timeLeft.Seconds > 0)
        {
            timeString += "0" + timeLeft.Seconds;
        }
        else
        {
            timeString += "00";
        }

        T_TimerLabel.GetComponentInChildren<TextMesh>().text = timeString;
    }

    protected abstract void updateVisuals(float percent);
}
