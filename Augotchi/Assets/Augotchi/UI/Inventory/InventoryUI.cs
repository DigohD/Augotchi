using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class InventoryUI : MonoBehaviour {

    public static bool reRender = true;

    public GameObject P_SeedItem;
    public GameObject P_ProduceItem;
    public GameObject P_UniqueItem;

    public Transform T_SeedList;
    public GameObject G_SeedSection;

    public Transform T_ProduceList;
    public GameObject G_ProduceSection;

    public Transform T_UniqueList;
    public GameObject G_UniqueSection;

    private void Start()
    {
        reRender = true;
    }

    void FixedUpdate () {
        if (reRender)
        {
            renderUI();
        }
	}

    public void sectionClick(int sectionIndex)
    {
        G_SeedSection.SetActive(false);
        G_ProduceSection.SetActive(false);
        G_UniqueSection.SetActive(false);

        switch (sectionIndex)
        {
            case 0:
                G_SeedSection.SetActive(true);
                break;
            case 1:
                G_ProduceSection.SetActive(true);
                break;
            case 2:
                G_UniqueSection.SetActive(true);
                break;
        }
    }

    private void renderUI()
    {
        reRender = false;

        renderSeedList();
        renderProduceList();
        renderUniqueList();
    }

    private void renderSeedList()
    {
        foreach (Transform t in T_SeedList)
            Destroy(t.gameObject);

        ArrayList seedIndexesToRender = new ArrayList();
        for (int k = 0; k < PetKeeper.pet.inventory.seedCounts.Length; k++)
        {
            Debug.LogWarning("k: " + PetKeeper.pet.inventory.seedCounts[k]);
            if (PetKeeper.pet.inventory.seedCounts[k] > 0)
                seedIndexesToRender.Add(k);
        }

        int SeedCount = seedIndexesToRender.Count;
        int currentSeed = 0;

        T_SeedList.localPosition = Vector3.zero;
        ((RectTransform)T_SeedList).sizeDelta = new Vector2(0, (SeedCount / 3) * 257);

        int j = 0;
        while (currentSeed < SeedCount)
        {
            for (int i = 0; i < 3; i++)
            {
                if (currentSeed >= SeedCount)
                    break;

                GameObject newItem = Instantiate(P_SeedItem, Vector3.zero, Quaternion.identity);
                newItem.transform.SetParent(T_SeedList, false);
                newItem.transform.localPosition = new Vector2(i * 257, j * -257);

                newItem.GetComponent<SeedItem>().initSeedItem(
                    PetKeeper.pet.inventory.seedCounts[(int) seedIndexesToRender[currentSeed]],
                    Inventory.getSeedTypeInfo((Inventory.SeedType)(int)seedIndexesToRender[currentSeed]),
                    transform
                );

                currentSeed++;
            }
            j++;
        }
    }

    private void renderProduceList()
    {
        foreach (Transform t in T_ProduceList)
            Destroy(t.gameObject);

        ArrayList produceIndexesToRender = new ArrayList();
        for (int k = 0; k < PetKeeper.pet.inventory.produceCounts.Length; k++)
        {
            if (PetKeeper.pet.inventory.produceCounts[k] > 0)
                produceIndexesToRender.Add(k);
        }

        int produceCount = produceIndexesToRender.Count;
        int currentProduce = 0;

        T_ProduceList.localPosition = Vector3.zero;
        ((RectTransform)T_ProduceList).sizeDelta = new Vector2(0, 257 + ((produceCount / 3) * 257));

        int j = 0;
        while (currentProduce < produceCount)
        {
            for (int i = 0; i < 3; i++)
            {
                if (currentProduce >= produceCount)
                    break;

                GameObject newItem = Instantiate(P_ProduceItem, Vector3.zero, Quaternion.identity);
                newItem.transform.SetParent(T_ProduceList, false);
                newItem.transform.localPosition = new Vector2(i * 257, j * -257);

                newItem.GetComponent<ProduceItem>().initProduceItem(
                    PetKeeper.pet.inventory.produceCounts[(int)produceIndexesToRender[currentProduce]],
                    Inventory.getProduceTypeInfo((Inventory.ProduceType) (int) produceIndexesToRender[currentProduce])
                );

                currentProduce++;
            }
            j++;
        }
    }

    private void renderUniqueList()
    {
        foreach (Transform t in T_UniqueList)
            Destroy(t.gameObject);

        ArrayList uniqueIndexesToRender = new ArrayList();
        for (int k = 0; k < PetKeeper.pet.inventory.uniqueCounts.Length; k++)
        {
            if (PetKeeper.pet.inventory.uniqueCounts[k] > 0)
                uniqueIndexesToRender.Add(k);
        }

        int uniqueCount = uniqueIndexesToRender.Count;
        int currentUnique = 0;

        T_UniqueList.localPosition = Vector3.zero;
        ((RectTransform)T_UniqueList).sizeDelta = new Vector2(0, (uniqueCount / 3) * 257);

        int j = 0;
        while (currentUnique < uniqueCount)
        {
            for (int i = 0; i < 3; i++)
            {
                if (currentUnique >= uniqueCount)
                    break;

                GameObject newItem = Instantiate(P_UniqueItem, Vector3.zero, Quaternion.identity);
                newItem.transform.SetParent(T_UniqueList, false);
                newItem.transform.localPosition = new Vector2(i * 257, j * -257);

                newItem.GetComponent<UniqueItem>().initUniqueItem(
                    PetKeeper.pet.inventory.uniqueCounts[(int) uniqueIndexesToRender[currentUnique]],
                    Inventory.getUniqueTypeInfo((Inventory.UniqueType)(int) uniqueIndexesToRender[currentUnique])
                );

                currentUnique++;
            }
            j++;
        }
    }
}
