using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetNotificationHandeler : MonoBehaviour { 

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public static void PlanNotification(PetGlobal pg)
    {
        

    }
    public static TimeSpan CalculateHungerTime(float hungerValue)
    {
        long ticksRemaining = 0;

        if (hungerValue > 75)
        {
            //-0.015
        }
        else
        {
            //-0.01
        }

        return new TimeSpan(ticksRemaining);
    }
    public static TimeSpan CalculateHappinessTime(float happinessValue, float hungerValue, float healthValue)
    {
        long ticksRemaining = 0;

        if (hungerValue > 75)
        {
            //-0.015
        }
        else
        {
            //-0.01
        }

        return new TimeSpan(ticksRemaining);
    }
    public static TimeSpan CalculateHealthTime(float healthValue)
    {
        long ticksRemaining = 0;

        if (healthValue > 75)
        {
            //-0.013
        }
        else
        {
            //-0.003
        }

        return new TimeSpan(ticksRemaining);
    }
}
