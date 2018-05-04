using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUI : MonoBehaviour {

    public static bool reRender = false;
    public static Shop shop;

    public GameObject P_ShopSellItem;
    public GameObject P_ShopBuyItem;

    public Transform T_ShopSellContent;
    public Transform T_ShopBuyContent;

    private void Start()
    {
        reRender = true;
    }

    void FixedUpdate()
    {
        if (reRender)
        {
            renderUI();
        }
    }

    private void renderUI()
    {
        reRender = false;

        renderSellList();
        renderBuyList();
    }

    private void renderSellList()
    {
        foreach (Transform t in T_ShopSellContent)
            Destroy(t.gameObject);

        ArrayList produceIndexesToRender = new ArrayList();
        for (int k = 0; k < PetKeeper.pet.inventory.produceCounts.Length; k++)
        {
            if (PetKeeper.pet.inventory.produceCounts[k] > 0)
                produceIndexesToRender.Add(k);
        }

        int produceCount = produceIndexesToRender.Count;
        int currentProduce = 0;

        T_ShopSellContent.localPosition = Vector3.zero;
        ((RectTransform)T_ShopSellContent).sizeDelta = new Vector2(0, produceCount * 145f);

        int j = 0;
        while (currentProduce < produceCount)
        {
            GameObject newItem = Instantiate(P_ShopSellItem, Vector3.zero, Quaternion.identity);
            newItem.transform.SetParent(T_ShopSellContent, false);
            newItem.transform.localPosition = new Vector2(0, j * -145f);

            newItem.GetComponent<ShopSellItem>().initShopSellItem(
                PetKeeper.pet.inventory.produceCounts[(int) produceIndexesToRender[currentProduce]],
                Inventory.getProduceTypeInfo((Inventory.ProduceType)(int) produceIndexesToRender[currentProduce])
            );

            currentProduce++;
            j++;
        }
    }

    private void renderBuyList()
    {
        foreach (Transform t in T_ShopBuyContent)
            Destroy(t.gameObject);

        T_ShopBuyContent.localPosition = Vector3.zero;
        ((RectTransform)T_ShopBuyContent).sizeDelta = new Vector2(0, shop.buyList.Count * 145f);

        for(int i = 0; i < shop.buyList.Count; i++)
        {
            GameObject newItem = Instantiate(P_ShopBuyItem, Vector3.zero, Quaternion.identity);
            newItem.transform.SetParent(T_ShopBuyContent, false);
            newItem.transform.localPosition = new Vector2(0, i * -145f);

            newItem.GetComponent<ShopBuyItem>().initShopBuyItem(
                shop.buyList[i]
            );
        }
    }
}
