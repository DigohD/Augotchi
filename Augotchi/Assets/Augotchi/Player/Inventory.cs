using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class Inventory {

    public enum ItemRarity { COMMON, RARE, EPIC, AMAZING }

	public enum SeedType {
        CARROT_SEED = 0,
        MEATBALL_SEED = 1,
        GOOSEBERRY_SEED = 2,
        CABBAGE_SEED = 3,
        SAUSAGE_SEED = 4,
        SUGARCUBE_SEED = 5,
        POTATO_SEED = 6,
        SPINACH_SEED = 7,
        CHILI_SEED = 8,
        ONION_SEED = 9,
        PIZZA_SEED = 10,
        NUGGETS_SEED = 11
    }

    public enum ProduceType {
        CARROT = 0,
        MEATBALL = 1,
        GOOSEBERRY = 2,
        CABBAGE = 3,
        SAUSAGE = 4,
        SUGARCUBE = 5,
        POTATO = 6,
        SPINACH = 7,
        CHILI = 8,
        ONION = 9,
        PIZZA = 10,
        MUSHROOM = 11,
        BLUEBERRY = 12,
        EGG = 13,
        SALMON = 14,
        PERCH = 15,
        PIKE = 16,
        SHRIMP = 17,
        NUGGETS = 18,
        TRUFFLE = 19
    }

    public enum GardenDecorType
    {
        OAK = 0,
        SPRUCE = 1,
        BUSH = 2,
        MUSHROOM = 3,
        FENCE = 4,
        DRYSTONE = 5,
        STONEPATH = 6,
        FOUNTAIN = 7,
        BENCH = 8,
        FLAGPOLE = 9,
        TABLE = 10,
        STATUE = 11,
        POND = 12,
        URN = 13,
        GOLDPILE = 14,
        ARCHWAY = 15,
        TORCH = 16,
        LAMPPOST = 17,
        FLATSTONE = 18,
        LARGESTONE = 19,
        MEDIUMSTONE = 20,
        SMALLSTONE = 21,
        TALLSTONE = 22,
        STONEPILE = 23,
        FLOWERPATCH = 24,
        FLOWERBUCKET = 25,
        SUNFLOWERS = 26,
        PRIZEROSES = 27,
        FLOWERPOT = 28
    }

    public enum UniqueType
    {
        WRONGWORLD_ROOTS = 0
    }

    public int[] seedCounts;
    public int[] produceCounts;
    public int[] gardenDecorCounts;
    public int[] uniqueCounts;

    public Inventory()
    {
        seedCounts = new int[12] {
            3, 3, 3, 0, 0,
            0, 0, 0, 0, 0,
            0, 0
        };

        produceCounts = new int[20] {
            0, 0, 0, 0, 0,
            0, 0, 0, 0, 0,
            0, 0, 0, 0, 0,
            0, 0, 0, 0, 0
        };

        gardenDecorCounts = new int[29]
        {
            0, 0, 0, 0, 0,
            0, 0, 0, 0, 0,
            0, 0, 0, 0, 0,
            0, 0, 0, 0, 0,

            0, 0, 0, 0, 0,
            0, 0, 0, 0
        };

        uniqueCounts = new int[1] { 0 };
    }

    public Inventory(int[] oldSeedCounts, int[] oldProduceCounts, int[] oldGardenDecorCounts, int[] oldUniqueCounts)
    {
        seedCounts = new int[12];
        produceCounts = new int[20];
        gardenDecorCounts = new int[29];
        uniqueCounts = new int[1];

        for (int i = 0; i < oldSeedCounts.Length; i++)
        {
            seedCounts[i] = oldSeedCounts[i];
        }

        for (int i = 0; i < oldProduceCounts.Length; i++)
        {
            produceCounts[i] = oldProduceCounts[i];
        }

        for (int i = 0; i < oldGardenDecorCounts.Length; i++)
        {
            gardenDecorCounts[i] = oldGardenDecorCounts[i];
        }

        for (int i = 0; i < oldUniqueCounts.Length; i++)
        {
            uniqueCounts[i] = oldUniqueCounts[i];
        }
    }

    public static Seed getSeedTypeInfo(SeedType type)
    {
        switch (type)
        {
            case SeedType.CARROT_SEED:
                return new Seed(
                    "Carrot Seed",
                    10,
                    "Augotchi/Image/UISeed/Seedbag_Carrot",
                    SeedType.CARROT_SEED,
                    3600,
                    4,
                    ItemRarity.COMMON
                );
            case SeedType.MEATBALL_SEED:
                return new Seed(
                    "Meatball Seed",
                    10,
                    "Augotchi/Image/UISeed/Seedbag_Meatball",
                    SeedType.MEATBALL_SEED,
                    9000,
                    5,
                    ItemRarity.COMMON
                );
            case SeedType.GOOSEBERRY_SEED:
                return new Seed(
                    "Gooseberry Seed",
                    10,
                    "Augotchi/Image/UISeed/Seedbag_Gooseberry",
                    SeedType.GOOSEBERRY_SEED,
                    7200,
                    3,
                    ItemRarity.COMMON
                );
            case SeedType.CABBAGE_SEED:
                return new Seed(
                    "Cabbage Seed",
                    10,
                    "Augotchi/Image/UISeed/Seedbag_Cabbage",
                    SeedType.CABBAGE_SEED,
                    18000,
                    25,
                    ItemRarity.RARE
                );
            case SeedType.SAUSAGE_SEED:
                return new Seed(
                    "Sausage Seed",
                    10,
                    "Augotchi/Image/UISeed/Seedbag_Sausage",
                    SeedType.SAUSAGE_SEED,
                    19800,
                    30,
                    ItemRarity.RARE
                );
            case SeedType.SUGARCUBE_SEED:
                return new Seed(
                    "Sugarcube Seed",
                    10,
                    "Augotchi/Image/UISeed/Seedbag_Sugarcubes",
                    SeedType.SUGARCUBE_SEED,
                    16200,
                    25,
                    ItemRarity.RARE
                );
            case SeedType.POTATO_SEED:
                return new Seed(
                    "Potato Seed",
                    10,
                    "Augotchi/Image/UISeed/Seedbag_Potato",
                    SeedType.POTATO_SEED,
                    28800,
                    75,
                    ItemRarity.EPIC
                );
            case SeedType.SPINACH_SEED:
                return new Seed(
                    "Spinach Seed",
                    10,
                    "Augotchi/Image/UISeed/Seedbag_Spinach",
                    SeedType.SPINACH_SEED,
                    36000,
                    75,
                    ItemRarity.EPIC
                );
            case SeedType.CHILI_SEED:
                return new Seed(
                    "Chili Seed",
                    10,
                    "Augotchi/Image/UISeed/Seedbag_Chili",
                    SeedType.CHILI_SEED,
                    43200,
                    75,
                    ItemRarity.EPIC
                );
            case SeedType.ONION_SEED:
                return new Seed(
                    "Onion Seed",
                    10,
                    "Augotchi/Image/UISeed/Seedbag_Onion",
                    SeedType.ONION_SEED,
                    64800,
                    100,
                    ItemRarity.AMAZING
                );
            case SeedType.PIZZA_SEED:
                return new Seed(
                    "Pizza Seed",
                    10,
                    "Augotchi/Image/UISeed/Seedbag_Pizza",
                    SeedType.PIZZA_SEED,
                    86400,
                    150,
                    ItemRarity.AMAZING
                );
            case SeedType.NUGGETS_SEED:
                return new Seed(
                    "Nugget Seed",
                    10,
                    "Augotchi/Image/UISeed/Seedbag_Nugget",
                    SeedType.NUGGETS_SEED,
                    172800,
                    250,
                    ItemRarity.AMAZING
                );
        }

        return null;
    }

    public static Produce getProduceTypeInfo(ProduceType type)
    {
        switch (type)
        {
            case ProduceType.CARROT:
                return new Produce(
                    "Carrot",
                    "Augotchi/Image/UIProduce/ProduceIcon_Carrot",
                    ProduceType.CARROT,
                    1.5f,
                    3.5f,
                    0f,
                    0f,
                    0f,
                    0f,
                    10,
                    true,
                    ItemRarity.COMMON
                );
            case ProduceType.MEATBALL:
                return new Produce(
                    "Meatball",
                    "Augotchi/Image/UIProduce/ProduceIcon_Meatball",
                    ProduceType.MEATBALL,
                    5f,
                    0f,
                    0f,
                    0f,
                    0f,
                    0f,
                    15,
                    true,
                    ItemRarity.COMMON
                );
            case ProduceType.GOOSEBERRY:
                return new Produce(
                    "Gooseberry",
                    "Augotchi/Image/UIProduce/ProduceIcon_Gooseberries",
                    ProduceType.GOOSEBERRY,
                    0f,
                    1.5f,
                    3.5f,
                    0f,
                    0f,
                    0f,
                    8,
                    true,
                    ItemRarity.COMMON
                );
            case ProduceType.CABBAGE:
                return new Produce(
                    "Cabbage",
                    "Augotchi/Image/UIProduce/ProduceIcon_Cabbage",
                    ProduceType.CABBAGE,
                    5f,
                    10f,
                    0f,
                    0f,
                    0f,
                    0f,
                    16,
                    true,
                    ItemRarity.RARE
                );
            case ProduceType.SAUSAGE:
                return new Produce(
                    "Sausage",
                    "Augotchi/Image/UIProduce/ProduceIcon_Sausage",
                    ProduceType.SAUSAGE,
                    15f,
                    0f,
                    0f,
                    0f,
                    0f,
                    0f,
                    20,
                    true,
                    ItemRarity.RARE
                );
            case ProduceType.SUGARCUBE:
                return new Produce(
                    "Sugar cube",
                    "Augotchi/Image/UIProduce/ProduceIcon_Sugarcubes",
                    ProduceType.SUGARCUBE,
                    5f,
                    0f,
                    10f,
                    0f,
                    0f,
                    0f,
                    14,
                    true,
                    ItemRarity.RARE
                );
            case ProduceType.POTATO:
                return new Produce(
                    "Potato",
                    "Augotchi/Image/UIProduce/ProduceIcon_Potato",
                    ProduceType.POTATO,
                    10f,
                    5f,
                    0f,
                    0f,
                    15f,
                    0f,
                    22,
                    true,
                    ItemRarity.EPIC
                );
            case ProduceType.SPINACH:
                return new Produce(
                    "Spinach",
                    "Augotchi/Image/UIProduce/ProduceIcon_Spinach",
                    ProduceType.SPINACH,
                    5f,
                    10f,
                    0f,
                    15f,
                    0f,
                    0f,
                    22,
                    true,
                    ItemRarity.EPIC
                );
            case ProduceType.CHILI:
                return new Produce(
                    "Chili",
                    "Augotchi/Image/UIProduce/ProduceIcon_Chili",
                    ProduceType.CHILI,
                    5f,
                    0f,
                    10f,
                    0f,
                    0f,
                    15f,
                    22,
                    true,
                    ItemRarity.EPIC
                );
            case ProduceType.ONION:
                return new Produce(
                    "Onion",
                    "Augotchi/Image/UIProduce/ProduceIcon_Onion",
                    ProduceType.ONION,
                    15f,
                    15f,
                    15f,
                    5f,
                    5f,
                    5f,
                    30,
                    true,
                    ItemRarity.AMAZING
                );
            case ProduceType.PIZZA:
                return new Produce(
                    "Pizza",
                    "Augotchi/Image/UIProduce/ProduceIcon_Pizza",
                    ProduceType.PIZZA,
                    10f,
                    0f,
                    0f,
                    20f,
                    20f,
                    20f,
                    40,
                    true,
                    ItemRarity.AMAZING
                );
            case ProduceType.MUSHROOM:
                return new Produce(
                    "Mushroom",
                    "Augotchi/Image/UIProduce/ProduceIcon_Mushroom",
                    ProduceType.MUSHROOM,
                    5f,
                    0f,
                    0f,
                    0f,
                    0f,
                    10f,
                    18,
                    true,
                    ItemRarity.RARE
                );
            case ProduceType.BLUEBERRY:
                return new Produce(
                    "Blueberry",
                    "Augotchi/Image/UIProduce/ProduceIcon_Blueberry",
                    ProduceType.BLUEBERRY,
                    5f,
                    0f,
                    0f,
                    0f,
                    10f,
                    0f,
                    18,
                    true,
                    ItemRarity.RARE
                );
            case ProduceType.EGG:
                return new Produce(
                    "Egg",
                    "Augotchi/Image/UIProduce/ProduceIcon_Egg",
                    ProduceType.EGG,
                    5f,
                    0f,
                    0f,
                    10f,
                    0f,
                    0f,
                    18,
                    true,
                    ItemRarity.RARE
                );
            case ProduceType.SALMON:
                return new Produce(
                    "Salmon",
                    "Augotchi/Image/UIProduce/ProduceIcon_Salmon",
                    ProduceType.SALMON,
                    5f,
                    0f,
                    0f,
                    0f,
                    10f,
                    10f,
                    25,
                    true,
                    ItemRarity.EPIC
                );
            case ProduceType.PERCH:
                return new Produce(
                    "Perch",
                    "Augotchi/Image/UIProduce/ProduceIcon_Perch",
                    ProduceType.PERCH,
                    5f,
                    0f,
                    0f,
                    10f,
                    10f,
                    0f,
                    25,
                    true,
                    ItemRarity.EPIC
                );
            case ProduceType.PIKE:
                return new Produce(
                    "Pike",
                    "Augotchi/Image/UIProduce/ProduceIcon_Pike",
                    ProduceType.PIKE,
                    5f,
                    0f,
                    0f,
                    10f,
                    0f,
                    10f,
                    25,
                    true,
                    ItemRarity.EPIC
                );
            case ProduceType.SHRIMP:
                return new Produce(
                    "Shrimp",
                    "Augotchi/Image/UIProduce/ProduceIcon_Schrimp",
                    ProduceType.SHRIMP,
                    5f,
                    0f,
                    0f,
                    5f,
                    5f,
                    5f,
                    25,
                    true,
                    ItemRarity.EPIC
                );
            case ProduceType.NUGGETS:
                return new Produce(
                    "Gold nugget",
                    "Augotchi/Image/UIProduce/ProduceIcon_Nugget",
                    ProduceType.NUGGETS,
                    0f,
                    0f,
                    0f,
                    0f,
                    0f,
                    0f,
                    250,
                    false,
                    ItemRarity.AMAZING
                );
            case ProduceType.TRUFFLE:
                return new Produce(
                    "Truffle",
                    "Augotchi/Image/UIProduce/ProduceIcon_Truffle",
                    ProduceType.TRUFFLE,
                    0f,
                    0f,
                    0f,
                    0f,
                    0f,
                    0f,
                    400,
                    false,
                    ItemRarity.AMAZING
                );
        }

        return null;
    }

    public static GardenDecor getGardenDecorTypeInfo(GardenDecorType type)
    {
        switch (type)
        {
            case GardenDecorType.OAK:
                return new GardenDecor(
                    "Oak Tree",
                    1000,
                    2000,
                    "Augotchi/Image/UIDecor/DecorIcon_Oak",
                    GardenDecorType.OAK,
                    ItemRarity.RARE
                );
            case GardenDecorType.SPRUCE:
                return new GardenDecor(
                    "Spruce Tree",
                    1000,
                    2000,
                    "Augotchi/Image/UIDecor/DecorIcon_Spruce",
                    GardenDecorType.SPRUCE,
                    ItemRarity.RARE
                );
            case GardenDecorType.BUSH:
                return new GardenDecor(
                    "Bush",
                    250,
                    500,
                    "Augotchi/Image/UIDecor/DecorIcon_Bush",
                    GardenDecorType.BUSH,
                    ItemRarity.COMMON
                );
            case GardenDecorType.MUSHROOM:
                return new GardenDecor(
                    "Mushroom",
                    500,
                    -1,
                    "Augotchi/Image/UIDecor/DecorIcon_Mushroom",
                    GardenDecorType.MUSHROOM,
                    ItemRarity.RARE
                );
            case GardenDecorType.FENCE:
                return new GardenDecor(
                    "Fence",
                    250,
                    500,
                    "Augotchi/Image/UIDecor/DecorIcon_Fence",
                    GardenDecorType.FENCE,
                    ItemRarity.COMMON
                );
            case GardenDecorType.STONEPATH:
                return new GardenDecor(
                    "Stone Path",
                    250,
                    500,
                    "Augotchi/Image/UIDecor/DecorIcon_StonePath",
                    GardenDecorType.STONEPATH,
                    ItemRarity.COMMON
                );
            case GardenDecorType.DRYSTONE:
                return new GardenDecor(
                    "Dry Stone",
                    250,
                    500,
                    "Augotchi/Image/UIDecor/DecorIcon_Drystone",
                    GardenDecorType.DRYSTONE,
                    ItemRarity.COMMON
                );
            case GardenDecorType.FOUNTAIN:
                return new GardenDecor(
                    "Fountain",
                    25000,
                    -1,
                    "Augotchi/Image/UIDecor/DecorIcon_Fountain",
                    GardenDecorType.FOUNTAIN,
                    ItemRarity.AMAZING
                );
            case GardenDecorType.BENCH:
                return new GardenDecor(
                    "Bench",
                    750,
                    1500,
                    "Augotchi/Image/UIDecor/DecorIcon_Bench",
                    GardenDecorType.BENCH,
                    ItemRarity.RARE
                );
            case GardenDecorType.FLAGPOLE:
                return new GardenDecor(
                    "Flagpole",
                    12500,
                    -1,
                    "Augotchi/Image/UIDecor/DecorIcon_Flagpole",
                    GardenDecorType.FLAGPOLE,
                    ItemRarity.RARE
                );
            case GardenDecorType.TABLE:
                return new GardenDecor(
                    "Table",
                    1000,
                    2000,
                    "Augotchi/Image/UIDecor/DecorIcon_Table",
                    GardenDecorType.TABLE,
                    ItemRarity.RARE
                );
            case GardenDecorType.STATUE:
                return new GardenDecor(
                    "Statue",
                    25000,
                    -1,
                    "Augotchi/Image/UIDecor/DecorIcon_Statue",
                    GardenDecorType.STATUE,
                    ItemRarity.AMAZING
                );
            case GardenDecorType.POND:
                return new GardenDecor(
                    "Pond",
                    25000,
                    -1,
                    "Augotchi/Image/UIDecor/DecorIcon_Pond",
                    GardenDecorType.POND,
                    ItemRarity.AMAZING
                );
            case GardenDecorType.URN:
                return new GardenDecor(
                    "Urn",
                    500,
                    -1,
                    "Augotchi/Image/UIDecor/DecorIcon_Urn",
                    GardenDecorType.URN,
                    ItemRarity.RARE
                );
            case GardenDecorType.GOLDPILE:
                return new GardenDecor(
                    "Gold Pile",
                    2000,
                    -1,
                    "Augotchi/Image/UIDecor/DecorIcon_GoldPile",
                    GardenDecorType.GOLDPILE,
                    ItemRarity.RARE
                );
            case GardenDecorType.ARCHWAY:
                return new GardenDecor(
                    "Archway",
                    5000,
                    -1,
                    "Augotchi/Image/UIDecor/DecorIcon_Archway",
                    GardenDecorType.ARCHWAY,
                    ItemRarity.EPIC
                );
            case GardenDecorType.TORCH:
                return new GardenDecor(
                    "Torch",
                    1000,
                    -1,
                    "Augotchi/Image/UIDecor/DecorIcon_Torch",
                    GardenDecorType.TORCH,
                    ItemRarity.RARE
                );
            case GardenDecorType.LAMPPOST:
                return new GardenDecor(
                    "Lamp Pole",
                    5000,
                    -1,
                    "Augotchi/Image/UIDecor/DecorIcon_LampPost",
                    GardenDecorType.LAMPPOST,
                    ItemRarity.EPIC
                );
            case GardenDecorType.FLATSTONE:
                return new GardenDecor(
                    "Flat Stone",
                    1000,
                    2000,
                    "Augotchi/Image/UIDecor/DecorIcon_FlatStone",
                    GardenDecorType.FLATSTONE,
                    ItemRarity.RARE
                );
            case GardenDecorType.LARGESTONE:
                return new GardenDecor(
                    "Large Stone",
                    1000,
                    2000,
                    "Augotchi/Image/UIDecor/DecorIcon_LargeStone",
                    GardenDecorType.LARGESTONE,
                    ItemRarity.RARE
                );
            case GardenDecorType.MEDIUMSTONE:
                return new GardenDecor(
                    "Medium Stone",
                    500,
                    1000,
                    "Augotchi/Image/UIDecor/DecorIcon_MediumStone",
                    GardenDecorType.MEDIUMSTONE,
                    ItemRarity.COMMON
                );
            case GardenDecorType.SMALLSTONE:
                return new GardenDecor(
                    "Small Stone",
                    250,
                    500,
                    "Augotchi/Image/UIDecor/DecorIcon_SmallStone",
                    GardenDecorType.SMALLSTONE,
                    ItemRarity.COMMON
                );
            case GardenDecorType.TALLSTONE:
                return new GardenDecor(
                    "Tall Stone",
                    2500,
                    5000,
                    "Augotchi/Image/UIDecor/DecorIcon_TallStone",
                    GardenDecorType.TALLSTONE,
                    ItemRarity.EPIC
                );
            case GardenDecorType.STONEPILE:
                return new GardenDecor(
                    "Stone Pile",
                    2500,
                    5000,
                    "Augotchi/Image/UIDecor/DecorIcon_StonePile",
                    GardenDecorType.STONEPILE,
                    ItemRarity.EPIC
                );
            case GardenDecorType.FLOWERPATCH:
                return new GardenDecor(
                    "Flower Patch",
                    250,
                    500,
                    "Augotchi/Image/UIDecor/DecorIcon_FlowerPatch",
                    GardenDecorType.FLOWERPATCH,
                    ItemRarity.COMMON
                );
            case GardenDecorType.FLOWERPOT:
                return new GardenDecor(
                    "Flower Pot",
                    250,
                    500,
                    "Augotchi/Image/UIDecor/DecorIcon_FlowerPot",
                    GardenDecorType.FLOWERPOT,
                    ItemRarity.COMMON
                );
            case GardenDecorType.SUNFLOWERS:
                return new GardenDecor(
                    "Sunflowers",
                    500,
                    1000,
                    "Augotchi/Image/UIDecor/DecorIcon_Sunflowers",
                    GardenDecorType.SUNFLOWERS,
                    ItemRarity.RARE
                );
            case GardenDecorType.PRIZEROSES:
                return new GardenDecor(
                    "Prized Roses",
                    2500,
                    5000,
                    "Augotchi/Image/UIDecor/DecorIcon_PrizeRoses",
                    GardenDecorType.PRIZEROSES,
                    ItemRarity.EPIC
                );
            case GardenDecorType.FLOWERBUCKET:
                return new GardenDecor(
                    "Flower Bucket",
                    250,
                    500,
                    "Augotchi/Image/UIDecor/DecorIcon_FlowerBucket",
                    GardenDecorType.FLOWERBUCKET,
                    ItemRarity.COMMON
                );
        }

        return null;
    }

    public static Unique getUniqueTypeInfo(UniqueType type)
    {
        switch (type)
        {
            case UniqueType.WRONGWORLD_ROOTS:
                return new Unique(
                    "Wrongworld Roots",
                    "Augotchi/Image/UIUnique/Wrongworldroot",
                    UniqueType.WRONGWORLD_ROOTS,
                    500
                );
        }

        return null;
    }

    public static Color getHarvestColor()
    {
        return new Color(0.25f, 0.85f, 0.4f);
    }

    public static Color getRarityColor(ItemRarity rarity)
    {
        switch (rarity)
        {
            case ItemRarity.COMMON:
                return new Color(0.7f, 0.7f, 0.7f, 1);
            case ItemRarity.RARE:
                return new Color(0.2f, 0.95f, 0.25f, 1);
            case ItemRarity.EPIC:
                return new Color(0.25f, 0.6f, 0.9f, 1);
            case ItemRarity.AMAZING:
                return new Color(0.6f, 0.15f, 0.85f, 1);
        }

        return new Color(0.7f, 0.7f, 0.7f, 1);
    }
}
