using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardenDecor {

    public string name;
    public int bmCost;
    public int coinCost;
    public string imagePath;
    public Inventory.GardenDecorType gardenDecorType;
    public Inventory.ItemRarity rarity;

    public GardenDecor(string name, int bmCost, int coinCost, string imagePath, Inventory.GardenDecorType gardenDecorType, Inventory.ItemRarity rarity)
    {
        this.name = name;
        this.bmCost = bmCost;
        this.coinCost = coinCost;
        this.imagePath = imagePath;
        this.gardenDecorType = gardenDecorType;
        this.rarity = rarity;
    }
}
