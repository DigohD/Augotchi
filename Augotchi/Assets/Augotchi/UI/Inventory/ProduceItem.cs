﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProduceItem : MonoBehaviour {

    public Text nameText;
    public Image image;
    public Text countText;

    public bool isInteractable;

    private Produce produceInfo;

    private Transform inventory;

    public void initProduceItem(int count, Produce produceInfo)
    {
        nameText.text = produceInfo.name;
        countText.text = "x" + count;
        image.sprite = (Sprite) Resources.Load(produceInfo.imagePath, typeof(Sprite));

        this.produceInfo = produceInfo;

        GetComponent<Button>().interactable = isInteractable;
    }

    public void onClick()
    {
        PetKeeper.pet.inventory.produceCounts[(int) produceInfo.produceType] -= 1;

        HomeFoodUI.reRender = true;

        PetKeeper.pet.addHunger((int) produceInfo.hungerYield);
        PetKeeper.pet.addHealth((int) produceInfo.healthYield);
        PetKeeper.pet.addHappiness((int) produceInfo.happinessYield);
    }
}