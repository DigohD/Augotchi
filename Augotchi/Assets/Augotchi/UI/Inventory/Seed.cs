using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed {

    public string name;
    public int puffleCost;
    public string imagePath;
    public Inventory.SeedType seedType;
    public int growthTime;
    public int price;
    public Inventory.ItemRarity rarity;

    public Seed(string name, int puffleCost, string imagePath, Inventory.SeedType seedType, int growthTime, int price, Inventory.ItemRarity rarity)
    {
        this.name = name;
        this.puffleCost = puffleCost;
        this.imagePath = imagePath;
        this.seedType = seedType;
        this.growthTime = 15;
        this.price = price;
        this.rarity = rarity;
    }

}
