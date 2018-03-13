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
    public GameObject facePicker;

    // Use this for initialization
    void Start () {
        GameObject petKeeper = GameObject.FindGameObjectWithTag("PetKeeper");

        hatPicker.transform.GetChild(1).GetComponent<Text>().text = (pvd.hatIndex + 1) + "/" + petFactory.hats.Length;
        hatPicker.transform.GetChild(6).GetComponent<Text>().text = (pvd.hatVariation + 1) + "/" + petFactory.hats[pvd.hatIndex].gameObjects.Length;

        facePicker.transform.GetChild(1).GetComponent<Text>().text = (pvd.faceIndex + 1) + "/" + petFactory.faces.Length;
        facePicker.transform.GetChild(6).GetComponent<Text>().text = (pvd.faceVariations + 1) + "/" + petFactory.faces[pvd.faceIndex].gameObjects.Length;

        stage = 0;
    }

    public void onSectionClick(int newStage)
    {
        stage = newStage;

        GetComponent<AudioSource>().PlayOneShot(A_menuClick);

        updateUI();
    }

    private void updateUI()
    {
        hatPicker.SetActive(false);
        facePicker.SetActive(false);

        switch (stage)
        {
            case 0:
                hatPicker.SetActive(true);
                break;
            case 1:
                facePicker.SetActive(true);
                break;
        }
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

    public void onNextFace()
    {
        pvd.faceIndex++;
        if (pvd.faceIndex >= petFactory.faces.Length)
        {
            pvd.faceIndex = 0;
        }

        pvd.faceVariations = 0;

        facePicker.transform.GetChild(1).GetComponent<Text>().text = (pvd.faceIndex + 1) + "/" + petFactory.faces.Length;
        facePicker.transform.GetChild(6).GetComponent<Text>().text = (pvd.faceVariations + 1) + "/" + petFactory.faces[pvd.faceIndex].gameObjects.Length;

        GetComponent<AudioSource>().PlayOneShot(A_arrowClick);

        petFactory.buildPet(pvd);
    }

    public void onPreviousFace()
    {
        pvd.faceIndex--;
        if (pvd.faceIndex < 0)
        {
            pvd.faceIndex = petFactory.faces.Length - 1;
        }

        pvd.faceVariations = 0;

        hatPicker.transform.GetChild(1).GetComponent<Text>().text = (pvd.faceIndex + 1) + "/" + petFactory.faces.Length;
        hatPicker.transform.GetChild(6).GetComponent<Text>().text = (pvd.faceVariations + 1) + "/" + petFactory.faces[pvd.faceIndex].gameObjects.Length;

        GetComponent<AudioSource>().PlayOneShot(A_arrowClick);

        petFactory.buildPet(pvd);
    }

    public void onNextFaceVariation()
    {
        pvd.faceVariations++;
        if (pvd.faceVariations >= petFactory.faces[pvd.faceIndex].gameObjects.Length)
        {
            pvd.faceVariations = 0;
        }

        facePicker.transform.GetChild(6).GetComponent<Text>().text = (pvd.faceVariations + 1) + "/" + petFactory.faces[pvd.faceIndex].gameObjects.Length;

        GetComponent<AudioSource>().PlayOneShot(A_arrowClick);

        petFactory.buildPet(pvd);
    }

    public void onPreviousFaceVariation()
    {
        pvd.faceVariations--;
        if (pvd.faceVariations < 0)
        {
            pvd.faceVariations = petFactory.faces[pvd.faceIndex].gameObjects.Length - 1;
        }

        facePicker.transform.GetChild(6).GetComponent<Text>().text = (pvd.faceVariations + 1) + "/" + petFactory.faces[pvd.faceIndex].gameObjects.Length;

        GetComponent<AudioSource>().PlayOneShot(A_arrowClick);

        petFactory.buildPet(pvd);
    }
}
