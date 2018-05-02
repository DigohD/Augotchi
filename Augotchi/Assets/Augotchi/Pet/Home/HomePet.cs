using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomePet : MonoBehaviour {

    public GameObject P_PettingEffect;
    public GameObject P_CannedFoodEffect;
    public GameObject P_CandyEffect;
    public GameObject P_VegetableEffect;

    public Animator anim;

    private float animTimer;

    public PetFactory petFactory;

    // Use this for initialization
    void Start () {

    }

    private void Update()
    {
        if (PetKeeper.pet.isDead)
        {
            anim.SetInteger("State", 4);
            transform.localRotation = Quaternion.Euler(0, 235, 0);
            transform.localPosition = new Vector3(5f, 5f, 0);

            petFactory.setDeadEyes();
            return;
        }

        transform.localRotation = Quaternion.Euler(0, 0, 0);
        transform.localPosition = new Vector3(0, 0.83f, 0);

        animTimer -= Time.deltaTime;
        if(animTimer <= 0 && PetKeeper.pet.happiness > 75)
        {
            anim.SetInteger("State", -1);
        }
        else if (animTimer <= 0 && PetKeeper.pet.happiness > 25)
        {
            anim.SetInteger("State", 0);
        }
        else if (animTimer <= 0 && PetKeeper.pet.happiness > 0)
        {
            anim.SetInteger("State", -2);
        }
    }

    private void OnPetting(System.Object sender, EventArgs e)
    {
        Instantiate(P_PettingEffect, transform.position + (Vector3.up * 2.5f), P_PettingEffect.transform.rotation);
        anim.SetInteger("State", 1);

        if(animTimer <= 0)
        {
            animTimer = 1;
        }
    }

    private void OnFeedCandy(System.Object sender, EventArgs e)
    {
        Instantiate(P_CandyEffect, transform.position + (Vector3.up * 2.5f), P_CandyEffect.transform.rotation);
        anim.SetInteger("State", 1);

        if (animTimer <= 0)
        {
            animTimer = 1;
        }
    }

    private void OnFeedCannedFood(System.Object sender, EventArgs e)
    {
        Instantiate(P_CannedFoodEffect, transform.position + (Vector3.up * 2.5f), P_CannedFoodEffect.transform.rotation);
        anim.SetInteger("State", 2);

        if (animTimer <= 0)
        {
            animTimer = 1;
        }
    }

    private void OnFeedVegetables(System.Object sender, EventArgs e)
    {
        Instantiate(P_VegetableEffect, transform.position + (Vector3.up * 2.5f), P_VegetableEffect.transform.rotation);
        anim.SetInteger("State", 3);

        if (animTimer <= 0)
        {
            animTimer = 1;
        }
    }

}
