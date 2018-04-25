using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class Inventory {

	public enum SeedType {
        CARROT_SEED = 0,
        MEATBALL_SEED = 1,
        GOOSEBERRY_SEED = 2
    }

    public enum ProduceType {
        CARROT = 0,
        MEATBALL = 1,
        GOOSEBERRY = 2
    }

    public int[] seedCounts;

    public Inventory()
    {
        seedCounts = new int[3] { 3, 3, 3 };
    }

    public static Seed getSeedTypeInfo(SeedType type)
    {
        switch (type)
        {
            case SeedType.CARROT_SEED:
                return new Seed(
                    "Carrot Seed",
                    10,
                    "Augotchi/Image/UISeed/Carrot",
                    SeedType.CARROT_SEED,
                    30
                );
            case SeedType.MEATBALL_SEED:
                return new Seed(
                    "Meatball Seed",
                    10,
                    "Augotchi/Image/UISeed/Meatball",
                    SeedType.MEATBALL_SEED,
                    120
                );
            case SeedType.GOOSEBERRY_SEED:
                return new Seed(
                    "Gooseberry Seed",
                    10,
                    "Augotchi/Image/UISeed/Gooseberry",
                    SeedType.GOOSEBERRY_SEED,
                    60
                );
        }

        return null;
    }
}
