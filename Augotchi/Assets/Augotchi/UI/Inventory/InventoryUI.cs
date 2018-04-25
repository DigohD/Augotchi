using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class InventoryUI : MonoBehaviour {

    public static bool reRender = true;

    public GameObject P_SeedItem;

    public Transform T_SeedList;
    public GameObject G_SeedSection;

    public Transform T_ProduceList;
    public GameObject G_ProduceSection;

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

        switch (sectionIndex)
        {
            case 0:
                G_SeedSection.SetActive(true);
                break;
            case 1:
                G_ProduceSection.SetActive(true);
                break;
        }
    }

    private void renderUI()
    {
        reRender = false;

        renderSeedList();  
    }

    private void renderSeedList()
    {
        foreach (Transform t in T_SeedList)
            Destroy(t.gameObject);

        ArrayList seedIndexesToRender = new ArrayList();
        for (int k = 0; k < PetKeeper.pet.inventory.seedCounts.Length; k++)
        {
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
                    PetKeeper.pet.inventory.seedCounts[(int)seedIndexesToRender[currentSeed]],
                    Inventory.getSeedTypeInfo((Inventory.SeedType)(int)seedIndexesToRender[currentSeed]),
                    transform
                );

                currentSeed++;
            }
            j++;
        }
    }
}
