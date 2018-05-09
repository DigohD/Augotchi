using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DungeonUI : MonoBehaviour {

    protected static Dungeon rDungeon;
    protected static bool reRender;

    public Text nameText;

    public Text difficultyText;
    public Image difficultyBG;

    public Text strengthText;
    public Text intelligenceText;
    public Text agilityText;

    public Text successText;
    public Image successBG;

    public Image rewardImage;
    public Text rewardAmountText;

    public Text timeText;

    public Button goButton;

    void FixedUpdate()
    {
        if (reRender)
        {
            reRenderUI();
            gameObject.SetActive(true);
            reRender = false;
        }   
    }

    private void reRenderUI()
    {
        if (PetKeeper.pet.isDungeoneering)
            goButton.interactable = false;

        nameText.text = rDungeon.name;

        float difficultyPercent = (float) rDungeon.difficultyRating / 2500f;
        Color DifficultyColor = new Color(difficultyPercent, 1 - difficultyPercent, 0, 1);
        difficultyText.text = "" + rDungeon.difficultyRating;
        difficultyBG.color = DifficultyColor;

        int addUp =
            (int) rDungeon.strengthWeight +
            (int) rDungeon.intelligenceWeight +
            (int) rDungeon.agilityWeight
                
            < 100
            
            ?
                1
            :
                0
        ;

        strengthText.text = (int) (rDungeon.strengthWeight * 100 + addUp) + "%";
        intelligenceText.text = (int) (rDungeon.intelligenceWeight * 100) + "%";
        agilityText.text = (int) (rDungeon.agilityWeight * 100) + "%";

        int petPower = (int)
            ((PetKeeper.pet.strength * rDungeon.strengthWeight) +
            (PetKeeper.pet.intelligence * rDungeon.intelligenceWeight) +
            (PetKeeper.pet.agility * rDungeon.agilityWeight));

        

        float petDungeonRate = (float) petPower / (float) rDungeon.difficultyRating;

        // 1 - o.9/(1 + p/d)
        float successRate = 1f - (0.9f / (1f + petDungeonRate));

        successText.text = (int) (successRate * 100) + "%";
        successBG.color = new Color(1 - successRate, successRate, 0, 1);

        
        rewardImage.sprite = (Sprite) Resources.Load(Quest.getRewardTypeImagePath(rDungeon.rewardType), typeof(Sprite));
        rewardAmountText.text = "x" + rDungeon.rewardAmount;

        if (rDungeon.time % 3600 == 0)
            timeText.text = rDungeon.time / 3600 + " H";
        else
            timeText.text = rDungeon.time / 3600 + ".5 H";
    }

    public void onGo()
    {
        PetKeeper.pet.startDungeon(rDungeon);
    }

    public static void ReRender(Dungeon dungeon)
    {
        reRender = true;
        rDungeon = dungeon;
    }
}
