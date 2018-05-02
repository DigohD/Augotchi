using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestItem : MonoBehaviour {

    public Text nameText;
    public Text descText;
    public Image image;
    public Text progressText;
    public Text rewardAmountText;

    Quest representedQuest;

    public void initQuestItem(Quest quest)
    {
        nameText.text = quest.title;
        descText.text = quest.description;
        //image.sprite = (Sprite)Resources.Load(produceInfo.imagePath, typeof(Sprite));
        progressText.text = quest.progress + "/" + quest.target;
        rewardAmountText.text = "x" + quest.reward;

        this.representedQuest = quest;
    }

    public void onClick()
    {
        
    }
}
