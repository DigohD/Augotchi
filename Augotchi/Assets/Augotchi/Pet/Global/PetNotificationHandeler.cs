using Assets.SimpleAndroidNotifications;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetNotificationHandeler {

    private static int messageType = -1;

    public static void PlanNotification(PetGlobal pet)
    {
        NotificationManager.CancelAll();

        TimeSpan deathTimespan;

        if (pet.isDead)
        {
            NotificationManager.SendWithAppIcon(new TimeSpan(23, 0, 0), pet.name + " has passed out!", "Revive by taking a walk!", new Color(1f, 0f, 0f), NotificationIcon.Clock);
            return;
        }

        if (pet.hunger <= 25 || pet.health <= 25 || pet.happiness <= 25)
        {
            NotificationManager.SendWithAppIcon(new TimeSpan(12, 0, 0), pet.name + " is in critical condition!", "Oh no, that is not good...", new Color(1f, 0f, 0f), NotificationIcon.Clock);
            return;
        }
            

        TimeSpan smallestTimespan = degenerationPrediction(pet);

        Debug.LogWarning("Crisis message in: " + smallestTimespan.ToString());

        // Determined what notification to send, following is sending the notification:

        if (messageType == 1)
        {
            NotificationManager.SendWithAppIcon(smallestTimespan, pet.name + " is hungry!", "Feed " + pet.name + " some food!", new Color(1f,0f,0f), NotificationIcon.Clock);
        }
        else if (messageType == 2)
        {
            NotificationManager.SendWithAppIcon(smallestTimespan, pet.name + " is sad!", "Pet " + pet.name + " or use some candy!", new Color(1f, 0f, 0f), NotificationIcon.Clock);
        }
        else if (messageType == 3)
        {
            NotificationManager.SendWithAppIcon(smallestTimespan, pet.name + " needs exercise!", "Take " + pet.name + " for a walk and improve both of your health!", new Color(1f, 0f, 0f), NotificationIcon.Clock);
        }
        else
        {
            Debug.LogWarning("Something went horribly wrong with sending a notification! Could not determine smallest!");
        }

        NotificationManager.SendWithAppIcon(new TimeSpan(12, 0, 0), pet.name + " is in critical condition!", "Oh no, that is not good...", new Color(1f, 0f, 0f), NotificationIcon.Clock);
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

        if (hunger < 25)
            messageType = 1;

        if (happiness < 25)
            messageType = 2;

        if (health < 25)
            messageType = 3;

        return new TimeSpan(ticks * TimeSpan.TicksPerSecond * 10);
    }
}
