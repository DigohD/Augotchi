using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopBuyItem : MonoBehaviour {

    public Text nameText;
    public Image image;
    public Text countText;

    Shop.ShopItem item;

    public Button buyButton;

    public AudioClip A_BuySound;

    public void initShopBuyItem(Shop.ShopItem item)
    {
        switch (item.itemType)
        {
            case Shop.ItemType.SEED:
                Seed seedInfo = Inventory.getSeedTypeInfo((Inventory.SeedType) item.itemIndex);
                nameText.text =seedInfo.name;
                countText.text = "x" + item.amount;
                image.sprite = (Sprite)Resources.Load(seedInfo.imagePath, typeof(Sprite));
                item.price = seedInfo.price * 5;
                break;
            case Shop.ItemType.PRODUCE:
                Produce produceInfo = Inventory.getProduceTypeInfo((Inventory.ProduceType) item.itemIndex);
                nameText.text = produceInfo.name;
                countText.text = "x" + item.amount;
                image.sprite = (Sprite)Resources.Load(produceInfo.imagePath, typeof(Sprite));
                item.price = produceInfo.price * 3;
                break;
            case Shop.ItemType.UNIQUE:
                Unique uniqueInfo = Inventory.getUniqueTypeInfo((Inventory.UniqueType) item.itemIndex);
                nameText.text = uniqueInfo.name;
                countText.text = "x" + item.amount;
                image.sprite = (Sprite)Resources.Load(uniqueInfo.imagePath, typeof(Sprite));
                item.price = uniqueInfo.price * 5;
                break;
            case Shop.ItemType.GARDEN_DECOR:
                GardenDecor decorInfo = Inventory.getGardenDecorTypeInfo((Inventory.GardenDecorType) item.itemIndex);
                nameText.text = decorInfo.name;
                countText.text = "x" + item.amount;
                image.sprite = (Sprite) Resources.Load(decorInfo.imagePath, typeof(Sprite));
                item.price = decorInfo.coinCost;
                break;
        }

        buyButton.GetComponentInChildren<Text>().text = "" + (item.price);

        this.item = item;
    }

    private void FixedUpdate()
    {
        if(item != null)
            buyButton.interactable = PetKeeper.pet.currency >= item.price;
    }

    public void onClick()
    {
        switch (item.itemType)
        {
            case Shop.ItemType.SEED:
                Seed seedInfo = Inventory.getSeedTypeInfo((Inventory.SeedType) item.itemIndex);
                PetKeeper.pet.inventory.seedCounts[(int) seedInfo.seedType] += 1;
                break;
            case Shop.ItemType.PRODUCE:
                Produce produceInfo = Inventory.getProduceTypeInfo((Inventory.ProduceType)item.itemIndex);
                PetKeeper.pet.inventory.produceCounts[(int) produceInfo.produceType] += 1;
                break;
            case Shop.ItemType.UNIQUE:
                Unique uniqueInfo = Inventory.getUniqueTypeInfo((Inventory.UniqueType)item.itemIndex);
                PetKeeper.pet.inventory.uniqueCounts[(int) uniqueInfo.uniqueType] += 1;
                break;
            case Shop.ItemType.GARDEN_DECOR:
                GardenDecor decorInfo = Inventory.getGardenDecorTypeInfo((Inventory.GardenDecorType) item.itemIndex);
                PetKeeper.pet.inventory.gardenDecorCounts[(int) decorInfo.gardenDecorType] += 1;
                break;
        }

        PetKeeper.pet.takeCurrency(item.price);

        GameControl.playPostMortemAudioClip(A_BuySound);

        item.amount -= 1;
        if (item.amount <= 0)
        {
            ShopUI.shop.buyList.Remove(item);
        }

        InventoryUI.reRender = true;
        ShopUI.reRender = true;
    }
}
