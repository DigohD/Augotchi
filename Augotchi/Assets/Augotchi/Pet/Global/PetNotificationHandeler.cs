using Assets.SimpleAndroidNotifications;
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
        int whatNotificationToSend = 0; // 1= Hunger, 2= happiness, 3=Health

        TimeSpan smallestTimespan = CalculateHungerTime(pg.hunger);
        whatNotificationToSend = 1;

        if(CalculateHappinessTime(pg.happiness, pg.hunger, pg.health) < smallestTimespan)
        {
            smallestTimespan = CalculateHappinessTime(pg.happiness, pg.hunger, pg.health);
            whatNotificationToSend = 2;
        }
        if(CalculateHealthTime(pg.health) < smallestTimespan)
        {
            smallestTimespan = CalculateHealthTime(pg.health);
            whatNotificationToSend = 3;
        }

        //Determined what notification to send, following is sending the notification:

        if (whatNotificationToSend == 1)
        {
            NotificationManager.SendWithAppIcon(smallestTimespan, "Augotchi hunger", "Your pet is literary starving rn", new Color(1f,0f,0f), NotificationIcon.Clock);
        }
        else if (whatNotificationToSend == 2)
        {
            NotificationManager.SendWithAppIcon(smallestTimespan, "Augotchi happiness", "Your pet is literary suicidal rn", new Color(1f, 0f, 0f), NotificationIcon.Clock);
        }
        else if (whatNotificationToSend == 3)
        {
            NotificationManager.SendWithAppIcon(smallestTimespan, "Augotchi health", "Your pet is literary drowning in fat rn", new Color(1f, 0f, 0f), NotificationIcon.Clock);
        }
        else
        {
            Debug.LogWarning("Something went horribly wrong with sending a notification! Could not determine smallest!");
        }



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
