using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsOverviewUI : MonoBehaviour {

    public Text timeAlive;
    public Text longestStreak;
    public Text passOutCount;
    public Text currencyMarkers;
    public Text foodMarkers;
    public Text crateMarkers;
    public Text reviveMarkers;
    public Text totalSteps;
    public Text foodConsumed;
    public Text petCreationDate;

    void FixedUpdate () {
        int aliveSeconds = PetKeeper.pet.currentAliveTicks * 10;
        TimeSpan ts = new TimeSpan(aliveSeconds * TimeSpan.TicksPerSecond);
        timeAlive.text = ts.Days + "d " + ts.Hours + "h " + ts.Seconds + "s";

        int streakSeconds = PetKeeper.pet.longestAliveTicks * 10;
        ts = new TimeSpan(streakSeconds * TimeSpan.TicksPerSecond);
        longestStreak.text = ts.Days + "d " + ts.Hours + "h " + ts.Seconds + "s";

        passOutCount.text = "" + PetKeeper.pet.petDeathCount;

        currencyMarkers.text = "x " + PetKeeper.pet.markersCurrency;
        foodMarkers.text = "x " + PetKeeper.pet.markersFood;
        crateMarkers.text = "x " + PetKeeper.pet.markersCrate;
        reviveMarkers.text = "x " + PetKeeper.pet.markersRevive;

        totalSteps.text = "" + PetKeeper.pet.stepCounter;

        foodConsumed.text = "" + (PetKeeper.pet.candyFed + PetKeeper.pet.foodFed + PetKeeper.pet.vegetableFed);
    }
}
