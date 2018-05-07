using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldUI : MonoBehaviour {

    public GameObject StepCounter;
    public GameObject ReviveCounter;

	void Update () {
        StepCounter.SetActive(!PetKeeper.pet.isDead);
	}
}
