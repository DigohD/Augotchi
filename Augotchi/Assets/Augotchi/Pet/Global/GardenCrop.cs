using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GardenCrop {

    public Inventory.SeedType seedType;
    public long plantedTimeStamp;
    public string longLat;

    public GardenCrop(Inventory.SeedType seedType, long plantedTimeStamp, string longLat)
    {
        this.seedType = seedType;
        this.plantedTimeStamp = plantedTimeStamp;
        this.longLat = longLat;
    }

}
