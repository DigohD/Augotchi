using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeedItem : MonoBehaviour {

    public Text costText;
    public Text nameText;
    public Image image;
    public Text countText;

    private int puffleCost;
    private Seed seedInfo;

    private Transform inventory;

    public void initSeedItem(int count, Seed seedInfo, Transform inventory)
    {
        costText.text = "" + seedInfo.puffleCost;
        nameText.text = seedInfo.name;
        countText.text = "x" + count;
        image.sprite = (Sprite) Resources.Load(seedInfo.imagePath, typeof(Sprite));

        this.puffleCost = seedInfo.puffleCost;
        this.seedInfo = seedInfo;

        this.inventory = inventory;
    }

    public void onClick()
    {
        if(PetKeeper.pet.puffles >= puffleCost)
        {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameControl>().startPlantingSeed(seedInfo);
            inventory.gameObject.SetActive(false);
        }
    }
}
