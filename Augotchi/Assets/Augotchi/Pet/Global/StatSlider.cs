using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatSlider : MonoBehaviour {

    public enum SliderStat { HUNGER, HEALTH, HAPPINESS }
    public SliderStat stat;

	void Update () {
        switch (stat)
        {
            case SliderStat.HUNGER:
                GetComponent<Slider>().value = PetKeeper.pet.hunger / 100f;
                break;
            case SliderStat.HEALTH:
                GetComponent<Slider>().value = PetKeeper.pet.health / 100f;
                break;
            case SliderStat.HAPPINESS:
                GetComponent<Slider>().value = PetKeeper.pet.happiness / 100f;
                break;
        }
	}
}
