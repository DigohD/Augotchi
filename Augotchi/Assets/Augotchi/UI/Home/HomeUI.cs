using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeUI : MonoBehaviour
{

    public GameObject G_FoodUI;
    public GameObject G_ReviveUI;

    private void Awake()
    {
        if (PetKeeper.pet.isDead)
        {
            G_FoodUI.SetActive(false);
            G_ReviveUI.SetActive(true);
        }
        else
        {
            G_FoodUI.SetActive(true);
            G_ReviveUI.SetActive(false);
        }
    }

    void Update()
    {
        if (PetKeeper.pet.isDead)
        {
            G_FoodUI.SetActive(false);
            G_ReviveUI.SetActive(true);
        }
        else
        {
            G_FoodUI.SetActive(true);
            G_ReviveUI.SetActive(false);
        }
    }

}
