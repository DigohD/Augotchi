using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BaseGardenDecor {

    public Inventory.GardenDecorType gardenDecorType;
    public float yRotation;
    public float scale;
    public string longLat;
    public int variation;
    public float yOffset;

    public BaseGardenDecor(Inventory.GardenDecorType gardenDecorType, string longLat, int variation)
    {
        this.gardenDecorType = gardenDecorType;
        this.longLat = longLat;
        this.scale = 1;
        this.yRotation = 0;
        this.yOffset = 0;
        this.variation = variation;
    }
}
