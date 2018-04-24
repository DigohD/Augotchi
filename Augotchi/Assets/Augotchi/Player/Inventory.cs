using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public int[] seedCounts = new int[3] { 5, 0, 5 };

    public static Seed getSeedTypeInfo(SeedType type)
    {
        switch (type)
        {
            case SeedType.CARROT_SEED:
                return new Seed(
                    "Carrot Seed",
                    10,
                    "Augotchi/Image/whiskers",
                    SeedType.CARROT_SEED
                );
            case SeedType.MEATBALL_SEED:
                return new Seed(
                    "Meatball Seed",
                    10,
                    "Augotchi/Image/whiskers",
                    SeedType.MEATBALL_SEED
                );
            case SeedType.GOOSEBERRY_SEED:
                return new Seed(
                    "Gooseberry Seed",
                    10,
                    "Augotchi/Image/whiskers",
                    SeedType.GOOSEBERRY_SEED
                );
        }

        return null;
    }
}
