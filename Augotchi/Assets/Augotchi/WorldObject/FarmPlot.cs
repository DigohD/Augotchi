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

    private GardenCrop representedCrop;
    private int initialGrowthTime;

    public virtual void init(Seed seedInfo, GardenCrop gardenCrop)
    {
        initialGrowthTime = seedInfo.growthTime;

        long growthTimeTicks = TimeSpan.TicksPerSecond * seedInfo.growthTime;
        long finishedTimeStamp = gardenCrop.plantedTimeStamp + growthTimeTicks;
        TimeSpan diff = new TimeSpan(DateTime.Now.Ticks - finishedTimeStamp);
        timeLeft = diff.Duration();

        if (timeLeft.TotalSeconds <= 0)
        {
            T_FinishedLabel.gameObject.SetActive(true);
        }
        else
        {
            T_TimerLabel.gameObject.SetActive(true);
        }

        representedCrop = gardenCrop;

        cameraT = GameObject.FindGameObjectWithTag("MainCamera").transform.parent;
    }

    float timer = 0;
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
	}

    public virtual void onPress()
    {
        PetKeeper.pet.Base.gardenCrops.Remove(representedCrop);

        Destroy(gameObject);

        PetKeeper.pet.Save(false);
    }

    private void setLabel()
    {
        string timeString = "";

        if (timeLeft.Hours > 0)
        {
            timeString += timeLeft.Hours + ":";
        }

        if (timeLeft.Minutes > 0)
        {
            timeString += timeLeft.Minutes + ":";
        }
        else
        {
            timeString += "00:";
        }

        if (timeLeft.Seconds > 0)
        {
            timeString += timeLeft.Seconds;
        }
        else
        {
            timeString += "00";
        }

        T_TimerLabel.GetComponentInChildren<TextMesh>().text = timeString;
    }

    protected abstract void updateVisuals(float percent);
}
