using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeFoodUI : MonoBehaviour {

    public Transform T_Content;

    public GameObject P_ProduceItem;

    public static bool reRender = true;

    void Start()
    {
        reRender = true;
    }

    private void FixedUpdate()
    {
        if(reRender)
            render();
    }

    void render()
    {
        reRender = false;

        foreach (Transform t in T_Content)
            Destroy(t.gameObject);

        ArrayList produceIndexesToRender = new ArrayList();
        for (int k = 0; k < PetKeeper.pet.inventory.produceCounts.Length; k++)
        {
            if (PetKeeper.pet.inventory.produceCounts[k] > 0)
                produceIndexesToRender.Add(k);
        }

        int produceCount = produceIndexesToRender.Count;
        int currentProduce = 0;

        T_Content.localPosition = Vector3.zero;
        ((RectTransform)T_Content).sizeDelta = new Vector2(produceCount * 74f, 74f);

        int i = 0;
        while (currentProduce < produceCount)
        {
            if (currentProduce >= produceCount)
                break;

            GameObject newItem = Instantiate(P_ProduceItem, Vector3.zero, Quaternion.identity);
            newItem.transform.SetParent(T_Content, false);
            newItem.transform.localPosition = new Vector2(i * 74f, 0);

            newItem.GetComponent<ProduceItem>().initProduceItem(
                PetKeeper.pet.inventory.produceCounts[(int)produceIndexesToRender[currentProduce]],
                Inventory.getProduceTypeInfo((Inventory.ProduceType)(int)produceIndexesToRender[currentProduce])
            );

            currentProduce++;
            i++;
        }
    }
}
