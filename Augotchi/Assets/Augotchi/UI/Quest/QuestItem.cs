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

    public GameObject G_ClaimRewardButton;

    Quest representedQuest;

    public void initQuestItem(Quest quest)
    {
        nameText.text = quest.title;
        descText.text = quest.description;
        //image.sprite = (Sprite)Resources.Load(produceInfo.imagePath, typeof(Sprite));
        progressText.text = quest.progress + "/" + quest.target;
        rewardAmountText.text = "x" + quest.rewardAmount;

        this.representedQuest = quest;

        if(quest.progress >= quest.target)
        {
            progressText.gameObject.SetActive(false);
            G_ClaimRewardButton.SetActive(true);
        }
    }

    public void onClick()
    {
        PetKeeper.pet.giveCurrency(representedQuest.rewardAmount);

        PetKeeper.pet.questLog.Remove(representedQuest);

        QuestUI.reRender = true;
    }
}
