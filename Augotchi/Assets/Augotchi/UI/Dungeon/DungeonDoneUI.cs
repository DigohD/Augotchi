using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DungeonDoneUI : MonoBehaviour {

    protected static Dungeon rDungeon;
    protected static bool reRender;

    public Text nameText;

    public Image rewardImage;
    public Text rewardAmountText;

    public GameObject G_SuccessText;
    public GameObject G_FailureText;
    public GameObject G_FailureOverlay;

    static bool succeeded;


    void Update()
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
        G_SuccessText.SetActive(false);
        G_FailureText.SetActive(false);
        G_FailureOverlay.SetActive(false);

        nameText.text = rDungeon.name;

        rewardImage.sprite = (Sprite)Resources.Load(Quest.getRewardTypeImagePath(rDungeon.rewardType), typeof(Sprite));
        rewardAmountText.text = "x" + rDungeon.rewardAmount;

        if (succeeded)
        {
            G_SuccessText.SetActive(true);
            G_SuccessText.GetComponent<Text>().text = PetKeeper.pet.name + " has successfully traversed the dungeon!";
        }
        else
        {
            G_FailureText.SetActive(true);
            G_FailureText.GetComponent<Text>().text = "Unfortunately, " + PetKeeper.pet.name + " did not succeed...";
            G_FailureOverlay.SetActive(true);
        }
    }

    public static void ReRender(Dungeon dungeon, bool succeeded)
    {
        reRender = true;
        rDungeon = dungeon;

        DungeonDoneUI.succeeded = succeeded;
    }
}
