using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GardenDecorItem : MonoBehaviour {

    public Text nameText;
    public Image image;
    public Text countText;
    public Text costText;
    public Button buildButton;

    private GardenDecor gardenDecorInfo;

    private Transform inventory;

    public Image rarityFrame;

    public void initGardenDecorItem(int count, GardenDecor gardenDecorInfo, Transform inventory)
    {
        nameText.text = gardenDecorInfo.name;
        countText.text = "x" + count;
        image.sprite = (Sprite) Resources.Load(gardenDecorInfo.imagePath, typeof(Sprite));
        rarityFrame.color = Inventory.getRarityColor(gardenDecorInfo.rarity);
        costText.text = "" + gardenDecorInfo.bmCost;

        this.gardenDecorInfo = gardenDecorInfo;

        if(gardenDecorInfo.bmCost > PetKeeper.pet.buildingMaterials)
        {
            buildButton.interactable = false;
        }

        this.inventory = inventory;
    }

    public void onClick()
    {
        inventory.gameObject.SetActive(false);
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameControl>().startBuildingGardenDecor(gardenDecorInfo);
    }
}
