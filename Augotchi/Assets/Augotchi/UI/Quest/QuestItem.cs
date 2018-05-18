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

    public AudioClip A_QuestDone;

    public GameObject G_ClaimRewardButton;

    Quest representedQuest;

    public void initQuestItem(Quest quest)
    {
        nameText.text = quest.title;
        descText.text = quest.description;
        image.sprite = (Sprite) Resources.Load(quest.rewardImagePath, typeof(Sprite));
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
        PetKeeper.pet.questLog.Remove(representedQuest);

        GameControl.playPostMortemAudioClip(A_QuestDone);

        switch (representedQuest.rewardType)
        {
            case Quest.QuestRewardType.BUILDING_MATERIALS:
                PetKeeper.pet.giveCurrency(representedQuest.rewardAmount);
                break;
            case Quest.QuestRewardType.COINS:
                PetKeeper.pet.giveBuildingMaterials(representedQuest.rewardAmount);
                break;
            case Quest.QuestRewardType.EXPERIENCE:
                PetKeeper.pet.grantXP(representedQuest.rewardAmount);
                break;
            case Quest.QuestRewardType.GARDEN_DECOR:
                PetKeeper.pet.addGardenDecor(LootTable.GenerateRandomQuestDecorType(), 1);
                QuestUI.reRender = true;
                break;
        }

        QuestUI.reRender = true;
    }
}
