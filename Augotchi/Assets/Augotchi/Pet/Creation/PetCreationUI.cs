using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

        stage = 0;

        petFactory.buildPet(pvd);
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

    }

    public void onPreviousEye()
    {

    }

    public void onNextTail()
    {

    }

    public void onPreviousTail()
    {

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

    }

    public void onPreviousWhisker()
    {

    }

    public void onNextNose()
    {

    }

    public void onPreviousNose()
    {

    }
}
