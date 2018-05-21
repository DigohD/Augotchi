using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop {

    public enum ItemType { SEED, PRODUCE, UNIQUE, GARDEN_DECOR };

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

        int items = Random.Range(3, 7);
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
                case ItemType.GARDEN_DECOR:
                    toAdd.itemType = ItemType.GARDEN_DECOR;
                    toAdd.itemIndex = (int) LootTable.GenerateRandomQuestDecorType();
                    toAdd.amount = 1;

                    if (Inventory.getGardenDecorTypeInfo((Inventory.GardenDecorType) toAdd.itemIndex).coinCost == -1)
                        continue;

                    break;
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
