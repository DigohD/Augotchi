using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatNumber : MonoBehaviour {

    public enum NumberStat { STRENGTH, INTELLIGENCE, AGILITY }
    public NumberStat stat;

    void Update()
    {
        switch (stat)
        {
            case NumberStat.STRENGTH:
                GetComponentInChildren<Text>().text = (int) PetKeeper.pet.strength + "";
                break;
            case NumberStat.INTELLIGENCE:
                GetComponentInChildren<Text>().text = (int) PetKeeper.pet.intelligence + "";
                break;
            case NumberStat.AGILITY:
                GetComponentInChildren<Text>().text = (int) PetKeeper.pet.agility + "";
                break;
        }
    }
}
