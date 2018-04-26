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

    public Produce(
        string name, 
        string imagePath, 
        Inventory.ProduceType produceType,
        float hungerYield,
        float healthYield,
        float happinessYield
    )
    {
        this.name = name;
        this.imagePath = imagePath;
        this.produceType = produceType;

        this.hungerYield = hungerYield;
        this.healthYield = healthYield;
        this.happinessYield = happinessYield;
    }
}
