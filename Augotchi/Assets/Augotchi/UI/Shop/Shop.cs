using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop {

    public enum ItemType { SEED, PRODUCE, UNIQUE };

    public class ShopItem{
        public ItemType itemType;
        public int itemIndex;
        public int amount;
        public int price;
    }

    public List<ShopItem> buyList;

    public Shop()
    {
        buyList = new List<ShopItem>();

        int items = Random.Range(2, 4);
        for(int i = 0; i < items; i++)
        {
            addBuyItemToList(buyList);
        }
    }

    public static void addBuyItemToList(List<ShopItem> buyList)
    {
        ShopItem toAdd = new ShopItem();

        bool itemDone = false;
        while (!itemDone)
        {
            ItemType rndType = (ItemType)Random.Range(0, System.Enum.GetNames(typeof(ItemType)).Length);

            switch (rndType)
            {
                case ItemType.SEED:
                    toAdd.itemType = ItemType.SEED;
                    toAdd.itemIndex = Random.Range(0, PetKeeper.pet.inventory.seedCounts.Length);
                    toAdd.amount = Random.Range(1, 6);
                    break;
                case ItemType.PRODUCE:
                    toAdd.itemType = ItemType.PRODUCE;
                    toAdd.itemIndex = Random.Range(0, PetKeeper.pet.inventory.produceCounts.Length);
                    toAdd.amount = Random.Range(1, 6);
                    break;
                case ItemType.UNIQUE:
                    continue;
            }

            bool alreadyExists = false;
            foreach(ShopItem item in buyList)
            {
                if(item.itemType == toAdd.itemType && item.itemIndex == toAdd.itemIndex)
                {
                    alreadyExists = true;
                }
            }

            if (alreadyExists)
            {
                itemDone = false;
            }
            else
            {
                itemDone = true;
            }
        }

        buyList.Add(toAdd);
    }


}
