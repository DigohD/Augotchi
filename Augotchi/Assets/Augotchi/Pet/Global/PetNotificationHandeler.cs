using Assets.SimpleAndroidNotifications;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetNotificationHandeler {

    public static void PlanNotification(PetGlobal pet)
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

    private static TimeSpan degenerationPrediction(PetGlobal pet)
    {
        long ticks = 0;

        float hunger = pet.hunger;
        float happiness = pet.happiness;
        float health = pet.health;

        while(hunger > 25f && health > 25f && happiness > 35f)
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

            ticks += 360;
        }

        return new TimeSpan(ticks * TimeSpan.TicksPerSecond * 10);
    }
}
