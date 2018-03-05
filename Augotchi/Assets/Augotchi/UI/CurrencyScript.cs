using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyScript : MonoBehaviour {

    void Update()
    {
        GetComponent<Text>().text = "x " + PetKeeper.pet.currency;
        transform.GetChild(0).GetComponent<Text>().text = "x " + PetKeeper.pet.currency;
    }
}
