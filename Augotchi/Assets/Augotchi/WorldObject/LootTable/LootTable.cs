using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootTable {

    private static Inventory.SeedType[] commonSeeds = new Inventory.SeedType[3]
    {
        Inventory.SeedType.CARROT_SEED,
        Inventory.SeedType.MEATBALL_SEED,
        Inventory.SeedType.GOOSEBERRY_SEED
    };
    private static Inventory.SeedType[] rareSeeds = new Inventory.SeedType[3]
    {
        Inventory.SeedType.CABBAGE_SEED,
        Inventory.SeedType.SAUSAGE_SEED,
        Inventory.SeedType.SUGARCUBE_SEED
    };
    private static Inventory.SeedType[] epicSeeds = new Inventory.SeedType[3]
    {
        Inventory.SeedType.POTATO_SEED,
        Inventory.SeedType.SPINACH_SEED,
        Inventory.SeedType.CHILI_SEED
    };
    private static Inventory.SeedType[] amazingSeeds = new Inventory.SeedType[3]
    {
        Inventory.SeedType.ONION_SEED,
        Inventory.SeedType.PIZZA_SEED,
        Inventory.SeedType.NUGGETS_SEED
    };

    public static Inventory.SeedType GenerateRandomSeedType()
    {
        int rnd = Random.Range(0, 1000);

        if(rnd < 750)
        {
            return commonSeeds[Random.Range(0, commonSeeds.Length)];
        }
        else if (rnd < 900)
        {
            return rareSeeds[Random.Range(0, rareSeeds.Length)];
        }
        else if (rnd < 975)
        {
            return epicSeeds[Random.Range(0, epicSeeds.Length)];
        }
        else
        {
            return amazingSeeds[Random.Range(0, amazingSeeds.Length)];
        }
    }

    private static Inventory.ProduceType[] pondLoot = new Inventory.ProduceType[4]
    {
        Inventory.ProduceType.SALMON,
        Inventory.ProduceType.PERCH,
        Inventory.ProduceType.PIKE,
        Inventory.ProduceType.SHRIMP
    };

    public static Inventory.ProduceType GeneratePondLoot()
    {
        return pondLoot[Random.Range(0, pondLoot.Length)];
    }

    private static Inventory.ProduceType[] forestLoot = new Inventory.ProduceType[3]
    {
        Inventory.ProduceType.MUSHROOM,
        Inventory.ProduceType.BLUEBERRY,
        Inventory.ProduceType.EGG
    };

    public static Inventory.ProduceType GenerateForestLoot()
    {
        if(Random.Range(0, 20) == 0)
        {
            return Inventory.ProduceType.TRUFFLE;
        }
        else
        {
            return forestLoot[Random.Range(0, forestLoot.Length)];
        }
    }

    private static Inventory.GardenDecorType[] commonDecor = new Inventory.GardenDecorType[9]
    {
        Inventory.GardenDecorType.BUSH,
        Inventory.GardenDecorType.FENCE,
        Inventory.GardenDecorType.STONEPATH,
        Inventory.GardenDecorType.DRYSTONE,
        Inventory.GardenDecorType.MEDIUMSTONE,
        Inventory.GardenDecorType.SMALLSTONE,
        Inventory.GardenDecorType.FLOWERPATCH,
        Inventory.GardenDecorType.FLOWERBUCKET,
        Inventory.GardenDecorType.FLOWERPOT
    };
    private static Inventory.GardenDecorType[] rareDecor = new Inventory.GardenDecorType[12]
    {
        Inventory.GardenDecorType.OAK,
        Inventory.GardenDecorType.SPRUCE,
        Inventory.GardenDecorType.MUSHROOM,
        Inventory.GardenDecorType.BENCH,
        Inventory.GardenDecorType.FLAGPOLE,
        Inventory.GardenDecorType.TABLE,
        Inventory.GardenDecorType.URN,
        Inventory.GardenDecorType.GOLDPILE,
        Inventory.GardenDecorType.TORCH,
        Inventory.GardenDecorType.FLATSTONE,
        Inventory.GardenDecorType.LARGESTONE,
        Inventory.GardenDecorType.SUNFLOWERS
    };
    private static Inventory.GardenDecorType[] epicDecor = new Inventory.GardenDecorType[5]
    {
        Inventory.GardenDecorType.ARCHWAY,
        Inventory.GardenDecorType.LAMPPOST,
        Inventory.GardenDecorType.TALLSTONE,
        Inventory.GardenDecorType.STONEPILE,
        Inventory.GardenDecorType.PRIZEROSES
    };
    private static Inventory.GardenDecorType[] amazingDecor = new Inventory.GardenDecorType[3]
    {
        Inventory.GardenDecorType.FOUNTAIN,
        Inventory.GardenDecorType.STATUE,
        Inventory.GardenDecorType.POND
    };

    public static Inventory.GardenDecorType GenerateRandomDungeonDecorType(int dungeonDifficulty)
    {
        int rnd = Random.Range(0, 1000);
        int difficultymod = dungeonDifficulty / 3;

        rnd += difficultymod;

        if (rnd < 750)
        {
            return commonDecor[Random.Range(0, commonDecor.Length)];
        }
        else if (rnd < 900)
        {
            return rareDecor[Random.Range(0, rareDecor.Length)];
        }
        else if (rnd < 975)
        {
            return epicDecor[Random.Range(0, epicDecor.Length)];
        }
        else
        {
            return amazingDecor[Random.Range(0, amazingDecor.Length)];
        }
    }

    public static Inventory.GardenDecorType GenerateRandomQuestDecorType()
    {
        int rnd = Random.Range(0, 1000);

        if (rnd < 850)
        {
            return commonDecor[Random.Range(0, commonDecor.Length)];
        }
        else
        {
            return rareDecor[Random.Range(0, rareDecor.Length)];
        }
    }
}
