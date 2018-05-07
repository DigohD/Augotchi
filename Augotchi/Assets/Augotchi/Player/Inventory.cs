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

    public enum UniqueType
    {
        WRONGWORLD_ROOTS = 0
    }

    public int[] seedCounts;
    public int[] produceCounts;
    public int[] uniqueCounts;

    public Inventory()
    {
        seedCounts = new int[3] { 3, 3, 3 };
        produceCounts = new int[3] { 0, 0, 0 };
        uniqueCounts = new int[1] { 0 };
    }

    public Inventory(int[] oldSeedCounts, int[] oldProduceCounts, int[] oldUniqueCounts)
    {
        seedCounts = new int[3];
        produceCounts = new int[3];
        uniqueCounts = new int[1];

        for (int i = 0; i < oldSeedCounts.Length; i++)
        {
            seedCounts[i] = oldSeedCounts[i];
        }

        for (int i = 0; i < oldProduceCounts.Length; i++)
        {
            produceCounts[i] = oldProduceCounts[i];
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
                    "Augotchi/Image/UISeed/Carrot",
                    SeedType.CARROT_SEED,
                    3600,
                    4
                );
            case SeedType.MEATBALL_SEED:
                return new Seed(
                    "Meatball Seed",
                    10,
                    "Augotchi/Image/UISeed/Meatball",
                    SeedType.MEATBALL_SEED,
                    9000,
                    5
                );
            case SeedType.GOOSEBERRY_SEED:
                return new Seed(
                    "Gooseberry Seed",
                    10,
                    "Augotchi/Image/UISeed/Gooseberry",
                    SeedType.GOOSEBERRY_SEED,
                    7200,
                    3
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
                    10
                );
            case ProduceType.MEATBALL:
                return new Produce(
                    "Meatball",
                    "Augotchi/Image/UIProduce/ProduceIcon_Meatball",
                    ProduceType.MEATBALL,
                    5f,
                    0f,
                    0f,
                    15
                );
            case ProduceType.GOOSEBERRY:
                return new Produce(
                    "Gooseberry",
                    "Augotchi/Image/UIProduce/ProduceIcon_Gooseberries",
                    ProduceType.GOOSEBERRY,
                    0f,
                    1.5f,
                    3.5f,
                    8
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
}
