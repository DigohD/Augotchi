using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ApparelUI : MonoBehaviour {

    public AudioClip A_arrowClick;
    public AudioClip A_menuClick;

    public PetFactory petFactory;

    private int stage;

    public PetVisualData pvd;

    public GameObject hatPicker;

	// Use this for initialization
	void Start () {
        GameObject petKeeper = GameObject.FindGameObjectWithTag("PetKeeper");

        hatPicker.transform.GetChild(1).GetComponent<Text>().text = (pvd.hatIndex + 1) + "/" + petFactory.hats.Length;
        hatPicker.transform.GetChild(6).GetComponent<Text>().text = (pvd.hatVariation + 1) + "/" + petFactory.hats[pvd.hatIndex].gameObjects.Length;

        stage = 0;
    }

    public void onNextHat()
    {
        pvd.hatIndex++;
        if (pvd.hatIndex >= petFactory.hats.Length)
        {
            pvd.hatIndex = 0;
        }

        pvd.hatVariation = 0;

        hatPicker.transform.GetChild(1).GetComponent<Text>().text = (pvd.hatIndex + 1) + "/" + petFactory.hats.Length;
        hatPicker.transform.GetChild(6).GetComponent<Text>().text = (pvd.hatVariation + 1) + "/" + petFactory.hats[pvd.hatIndex].gameObjects.Length;

        GetComponent<AudioSource>().PlayOneShot(A_arrowClick);

        petFactory.buildPet(pvd);
    }

    public void onPreviousHat()
    {
        pvd.hatIndex--;
        if (pvd.hatIndex < 0)
        {
            pvd.hatIndex = petFactory.hats.Length - 1;
        }

        pvd.hatVariation = 0;

        hatPicker.transform.GetChild(1).GetComponent<Text>().text = (pvd.hatIndex + 1) + "/" + petFactory.hats.Length;
        hatPicker.transform.GetChild(6).GetComponent<Text>().text = (pvd.hatVariation + 1) + "/" + petFactory.hats[pvd.hatIndex].gameObjects.Length;

        GetComponent<AudioSource>().PlayOneShot(A_arrowClick);

        petFactory.buildPet(pvd);
    }

    public void onNextHatVariation()
    {
        pvd.hatVariation++;
        if (pvd.hatVariation >= petFactory.hats[pvd.hatIndex].gameObjects.Length)
        {
            pvd.hatVariation = 0;
        }

        hatPicker.transform.GetChild(6).GetComponent<Text>().text = (pvd.hatVariation + 1) + "/" + petFactory.hats[pvd.hatIndex].gameObjects.Length;

        GetComponent<AudioSource>().PlayOneShot(A_arrowClick);

        petFactory.buildPet(pvd);
    }

    public void onPreviousHatVariation()
    {
        pvd.hatVariation--;
        if (pvd.hatVariation < 0)
        {
            pvd.hatVariation = petFactory.hats[pvd.hatIndex].gameObjects.Length - 1;
        }

        hatPicker.transform.GetChild(6).GetComponent<Text>().text = (pvd.hatVariation + 1) + "/" + petFactory.hats[pvd.hatIndex].gameObjects.Length;

        GetComponent<AudioSource>().PlayOneShot(A_arrowClick);

        petFactory.buildPet(pvd);
    }
}
