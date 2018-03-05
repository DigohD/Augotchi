using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomePet : MonoBehaviour {

    public GameObject P_PettingEffect;
    public GameObject P_CannedFoodEffect;
    public GameObject P_CandyEffect;
    public GameObject P_VegetableEffect;

    // Use this for initialization
    void Start () {
        PetKeeper.pet.OnPetting += this.OnPetting;
        PetKeeper.pet.OnFeedCandy += this.OnFeedCandy;
        PetKeeper.pet.OnFeedCannedFood += this.OnFeedCannedFood;
        PetKeeper.pet.OnFeedVegetables += this.OnFeedVegetables;
    }
	
	private void OnPetting(System.Object sender, EventArgs e)
    {
        Instantiate(P_PettingEffect, transform.position + (Vector3.up * 2.5f), P_PettingEffect.transform.rotation);
    }

    private void OnFeedCandy(System.Object sender, EventArgs e)
    {
        Instantiate(P_CandyEffect, transform.position + (Vector3.up * 2.5f), P_CandyEffect.transform.rotation);
    }

    private void OnFeedCannedFood(System.Object sender, EventArgs e)
    {
        Instantiate(P_CannedFoodEffect, transform.position + (Vector3.up * 2.5f), P_CannedFoodEffect.transform.rotation);
    }

    private void OnFeedVegetables(System.Object sender, EventArgs e)
    {
        Instantiate(P_VegetableEffect, transform.position + (Vector3.up * 2.5f), P_VegetableEffect.transform.rotation);
    }

}
