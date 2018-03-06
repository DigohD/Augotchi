using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PetCreationUI : MonoBehaviour {

    public PetFactory petFactory;

    public PetVisualData pvd;

    public GameObject earPicker;
    public GameObject tailPicker;
    public GameObject colorPicker;
    public GameObject eyePicker;
    public GameObject whiskersPicker;
    public GameObject nosePicker;

    public GameObject backStageButton;
    public GameObject nextStageButton;

    private int stage;

    private void Start()
    {
        PetGlobal pg = new PetGlobal();
        pvd = pg.LoadVisuals();

        Debug.LogWarning(pvd);

        earPicker.transform.GetChild(1).GetComponent<Text>().text = (pvd.earsIndex + 1) + "/" + petFactory.ears.Length;
        eyePicker.transform.GetChild(1).GetComponent<Text>().text = (pvd.eyesIndex + 1) + "/" + petFactory.eyes.Length;
        tailPicker.transform.GetChild(1).GetComponent<Text>().text = (pvd.tailIndex + 1) + "/" + petFactory.tails.Length;
        whiskersPicker.transform.GetChild(1).GetComponent<Text>().text = (pvd.whiskersIndex + 1) + "/" + petFactory.whiskers.Length;
        nosePicker.transform.GetChild(1).GetComponent<Text>().text = (pvd.noseIndex + 1) + "/" + petFactory.noses.Length;

        stage = 0;

        petFactory.buildPet(pvd);
    }

    public void onWorldClick()
    {
        PetGlobal pg = new PetGlobal();
        pg.SaveVisuals(pvd);

        GameControl.markerPicked = true;
        SceneManager.LoadScene("World");
    }

    public void onNextStage()
    {
        stage++;

        updateUI();
    }

    public void onBackStage()
    {
        stage--;

        updateUI();
    }

    private void updateUI()
    {
        earPicker.SetActive(false);
        tailPicker.SetActive(false);
        colorPicker.SetActive(false);
        eyePicker.SetActive(false);
        whiskersPicker.SetActive(false);
        nosePicker.SetActive(false);

        backStageButton.SetActive(true);
        nextStageButton.SetActive(true);

        if (stage == 0)
        {
            backStageButton.SetActive(false);
        }
        if (stage == 5)
        {
            nextStageButton.SetActive(false);
        }

        switch (stage)
        {
            case 0:
                earPicker.SetActive(true);
                break;
            case 1:
                tailPicker.SetActive(true);
                break;
            case 2:
                colorPicker.SetActive(true);
                break;
            case 3:
                eyePicker.SetActive(true);
                break;
            case 4:
                whiskersPicker.SetActive(true);
                break;
            case 5:
                nosePicker.SetActive(true);
                break;
        }
    }

    public void onNextEye()
    {
        pvd.eyesIndex++;
        if (pvd.eyesIndex >= petFactory.eyes.Length)
        {
            pvd.eyesIndex = 0;
        }

        eyePicker.transform.GetChild(1).GetComponent<Text>().text = (pvd.eyesIndex + 1) + "/" + petFactory.eyes.Length;

        petFactory.buildPet(pvd);
    }

    public void onPreviousEye()
    {
        pvd.eyesIndex--;
        if (pvd.eyesIndex < 0)
        {
            pvd.eyesIndex = petFactory.eyes.Length - 1;
        }

        eyePicker.transform.GetChild(1).GetComponent<Text>().text = (pvd.eyesIndex + 1) + "/" + petFactory.eyes.Length;

        petFactory.buildPet(pvd);
    }

    public void onNextTail()
    {
        pvd.tailIndex++;
        if (pvd.tailIndex >= petFactory.tails.Length)
        {
            pvd.tailIndex = 0;
        }

        tailPicker.transform.GetChild(1).GetComponent<Text>().text = (pvd.tailIndex + 1) + "/" + petFactory.tails.Length;

        petFactory.buildPet(pvd);
    }

    public void onPreviousTail()
    {
        pvd.tailIndex--;
        if (pvd.tailIndex < 0)
        {
            pvd.tailIndex = petFactory.tails.Length - 1;
        }

        tailPicker.transform.GetChild(1).GetComponent<Text>().text = (pvd.tailIndex + 1) + "/" + petFactory.tails.Length;

        petFactory.buildPet(pvd);
    }

    public void onNextEars()
    {
        pvd.earsIndex++;
        if(pvd.earsIndex >= petFactory.ears.Length)
        {
            pvd.earsIndex = 0;
        }

        earPicker.transform.GetChild(1).GetComponent<Text>().text = (pvd.earsIndex + 1) + "/" + petFactory.ears.Length;

        petFactory.buildPet(pvd);
    }

    public void onPreviousEars()
    {
        pvd.earsIndex--;
        if (pvd.earsIndex < 0)
        {
            pvd.earsIndex = petFactory.ears.Length - 1;
        }

        earPicker.transform.GetChild(1).GetComponent<Text>().text = (pvd.earsIndex + 1) + "/" + petFactory.ears.Length;

        petFactory.buildPet(pvd);
    }

    public void onNextColor()
    {

    }

    public void onPreviousColor()
    {

    }

    public void onNextWhisker()
    {
        pvd.whiskersIndex++;
        if (pvd.whiskersIndex >= petFactory.whiskers.Length)
        {
            pvd.whiskersIndex = 0;
        }

        whiskersPicker.transform.GetChild(1).GetComponent<Text>().text = (pvd.whiskersIndex + 1) + "/" + petFactory.whiskers.Length;

        petFactory.buildPet(pvd);
    }

    public void onPreviousWhisker()
    {
        pvd.whiskersIndex--;
        if (pvd.whiskersIndex < 0)
        {
            pvd.whiskersIndex = petFactory.whiskers.Length - 1;
        }

        whiskersPicker.transform.GetChild(1).GetComponent<Text>().text = (pvd.whiskersIndex + 1) + "/" + petFactory.whiskers.Length;

        petFactory.buildPet(pvd);
    }

    public void onNextNose()
    {
        pvd.noseIndex++;
        if (pvd.noseIndex >= petFactory.noses.Length)
        {
            pvd.noseIndex = 0;
        }

        nosePicker.transform.GetChild(1).GetComponent<Text>().text = (pvd.noseIndex + 1) + "/" + petFactory.noses.Length;

        petFactory.buildPet(pvd);
    }

    public void onPreviousNose()
    {
        pvd.noseIndex--;
        if (pvd.noseIndex < 0)
        {
            pvd.noseIndex = petFactory.noses.Length - 1;
        }

        nosePicker.transform.GetChild(1).GetComponent<Text>().text = (pvd.noseIndex + 1) + "/" + petFactory.noses.Length;

        petFactory.buildPet(pvd);
    }
}
