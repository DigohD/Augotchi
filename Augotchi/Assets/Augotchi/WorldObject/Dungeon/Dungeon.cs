using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dungeon {

    public string name;
    public float strengthWeight;
    public float intelligenceWeight;
    public float agilityWeight;
    public int time;

    public Quest.QuestRewardType rewardType;
    public int rewardAmount;

    public int difficultyRating;

    public Dungeon(
        string name,
        float strengthWeight,
        float intelligenceWeight,
        float agilityWeight,
        int time,
        Quest.QuestRewardType rewardType,
        int rewardAmount,
        int difficultyRating
    )
    {
        this.name = name;
        this.strengthWeight = strengthWeight;
        this.intelligenceWeight = intelligenceWeight;
        this.agilityWeight = agilityWeight;
        this.time = time;

        this.rewardType = rewardType;
        this.rewardAmount = rewardAmount;

        this.difficultyRating = difficultyRating;
    }

    public static Dungeon GenerateDungeon()
    {
        Dungeon toReturn;

        float rnd = Random.Range(1, 100);
        float rnd2 = Random.Range(1, 100);
        float rnd3 = Random.Range(1, 100);

        float total = rnd + rnd2 + rnd3;

        float dStrengthWeight = rnd / total;
        float dIntelligenceWeight = rnd2 / total;
        float dAgilityWeight = rnd3 / total;

        int dDifficultyRating = Random.Range(1, 21);

        string dName = GenerateDungeonName(dDifficultyRating);

        // 1800
        int dTime = 1800 * dDifficultyRating;

        Quest.QuestRewardType dRewardType = (Quest.QuestRewardType) Random.Range(0, System.Enum.GetNames(typeof(Quest.QuestRewardType)).Length);
        int dRewardAmount = 100 + (dDifficultyRating * 25 * Quest.getRewardTypeConversionRate(dRewardType));

        dDifficultyRating *= 125;

        toReturn = new Dungeon(
            dName,
            dStrengthWeight,
            dIntelligenceWeight,
            dAgilityWeight,
            dTime,
            dRewardType,
            dRewardAmount,
            dDifficultyRating
        );

        return toReturn;
    }

    private static string[] prefixes = new string[20]
    {
        "Hole of",
        "Tunnel to",
        "Passage to",
        "Path to",
        "Road to",

        "Cave of",
        "Gap of",
        "Cavity of",
        "Cleft of",
        "Shaft of",

        "Chamber of",
        "Crack of",
        "Crater of",
        "Burrow of",
        "Lair of",

        "Den of",
        "Dungeon of",
        "Fracture of",
        "Gorge of",
        "Void of"
    };

    private static string[] suffixes = new string[40]
    {
        "Happiness",
        "Joy",
        "Bliss",
        "Comfort",
        "Glee",

        "Wonder",
        "Festivity",
        "Lui",
        "Luxury",
        "Charm",

        "Adventure",
        "Chances",
        "Risks",
        "Ventures",
        "Curiosity",

        "Exploration",
        "Research",
        "Seeking",
        "Questions",
        "Travels",

        "Uncertainty",
        "Vulnerability",
        "Emergency",
        "Worry",
        "Distrust",

        "Danger",
        "Jeopardy",
        "Instability",
        "Peril",
        "Threat",

        "Pain",
        "Trouble",
        "Discomfort",
        "Strain",
        "Distress",

        "Torment",
        "Terror",
        "Agony",
        "Misery",
        "Annihilation"
    };

    private static string GenerateDungeonName(int difficultyRating)
    {
        string prefix = prefixes[difficultyRating - 1];
        string suffix = suffixes[Random.Range(difficultyRating - 1, difficultyRating + 19)];

        return prefix + " " + suffix;
    }

}
