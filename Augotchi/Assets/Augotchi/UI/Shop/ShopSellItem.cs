using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopSellItem : MonoBehaviour {

    public Text nameText;
    public Image image;
    public Text countText;

    public Text priceText;

    private Produce produceInfo;

    public AudioClip A_SellSound;

    public void initShopSellItem(int count, Produce produceInfo)
    {
        nameText.text = produceInfo.name;
        countText.text = "x" + count;
        image.sprite = (Sprite) Resources.Load(produceInfo.imagePath, typeof(Sprite));

        priceText.text = "" + produceInfo.price;

        this.produceInfo = produceInfo;
    }

    public void onClick()
    {
        PetKeeper.pet.inventory.produceCounts[(int) produceInfo.produceType] -= 1;
        PetKeeper.pet.giveCurrency(produceInfo.price);

        GameControl.playPostMortemAudioClip(A_SellSound);

        InventoryUI.reRender = true;
        ShopUI.reRender = true;
    }
}
