using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReviveCount : MonoBehaviour {

    void Update()
    {
        GetComponent<Text>().text = "" + PetKeeper.pet.reviveProgress + "/" + 10;
        transform.GetChild(0).GetComponent<Text>().text = "" + PetKeeper.pet.reviveProgress + "/" + 10;
    }
}
