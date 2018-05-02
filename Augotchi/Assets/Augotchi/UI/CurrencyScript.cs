using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyScript : MonoBehaviour {

    public enum CurrencyTyoe { COINS, BUILDING_MATERIALS }

    public CurrencyTyoe currencyType;

    void Update()
    {
        switch (currencyType)
        {
            case CurrencyTyoe.COINS:
                GetComponent<Text>().text = "x " + PetKeeper.pet.currency;
                transform.GetChild(0).GetComponent<Text>().text = "x " + PetKeeper.pet.currency;
                break;
            case CurrencyTyoe.BUILDING_MATERIALS:
                GetComponent<Text>().text = "x " + PetKeeper.pet.buildingMaterials;
                transform.GetChild(0).GetComponent<Text>().text = "x " + PetKeeper.pet.buildingMaterials;
                break;
        }
        
    }
}
