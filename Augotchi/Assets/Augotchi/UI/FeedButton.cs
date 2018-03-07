using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FeedButton : MonoBehaviour {

    public enum FeedType { CANDY, FOOD, VEGETABLES }

    public FeedType feedType;

    void Update()
    {
        switch (feedType)
        {
            case FeedType.CANDY:
                transform.GetChild(4).GetComponent<Text>().text = "" + PetKeeper.pet.candy;
                break;
            case FeedType.FOOD:
                transform.GetChild(4).GetComponent<Text>().text = "" + PetKeeper.pet.food;
                break;
            case FeedType.VEGETABLES:
                transform.GetChild(4).GetComponent<Text>().text = "" + PetKeeper.pet.vegetables;
                break;
        }

        
    }

    public void onClick()
    {
        switch (feedType)
        {
            case FeedType.CANDY:
                PetKeeper.pet.feedCandy();
                break;
            case FeedType.FOOD:
                PetKeeper.pet.feedFood();
                break;
            case FeedType.VEGETABLES:
                PetKeeper.pet.feedVegetables();
                break;
        }
    }
}
