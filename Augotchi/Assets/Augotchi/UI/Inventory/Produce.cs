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

    public int price;

    public Produce(
        string name, 
        string imagePath, 
        Inventory.ProduceType produceType,
        float hungerYield,
        float healthYield,
        float happinessYield,
        int price
    )
    {
        this.name = name;
        this.imagePath = imagePath;
        this.produceType = produceType;

        this.hungerYield = hungerYield;
        this.healthYield = healthYield;
        this.happinessYield = happinessYield;

        this.price = price;
    }
}
