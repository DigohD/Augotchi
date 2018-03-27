using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetNotificationHandeler {

    public static void PlanNotification(PetGlobal pet)
    {
        
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
