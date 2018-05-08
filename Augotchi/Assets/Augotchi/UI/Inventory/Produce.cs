using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Produce {

    public string name;
    public string imagePath;
    public Inventory.ProduceType produceType;

    public float hungerYield;
    public float healthYield;
    public float happinessYield;

    public float strengthYield;
    public float intelligenceYield;
    public float agilityYield;

    public int price;

    public bool isEdible;
    public Inventory.ItemRarity rarity;

    public Produce(
        string name, 
        string imagePath, 
        Inventory.ProduceType produceType,
        float hungerYield,
        float healthYield,
        float happinessYield,
        float strengthYield,
        float intelligenceYield,
        float agilityYield,
        int price,
        bool isEdible,
        Inventory.ItemRarity rarity
    )
    {
        this.name = name;
        this.imagePath = imagePath;
        this.produceType = produceType;

        this.hungerYield = hungerYield;
        this.healthYield = healthYield;
        this.happinessYield = happinessYield;

        this.strengthYield = strengthYield;
        this.intelligenceYield = intelligenceYield;
        this.agilityYield = agilityYield;

        this.price = price;

        this.isEdible = isEdible;
        this.rarity = rarity;
    }
}
