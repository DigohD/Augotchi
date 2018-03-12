using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PetCreationUI : MonoBehaviour {

    public PetFactory petFactory;

    public PetVisualData pvd;

    public AudioClip A_arrowClick;
    public AudioClip A_menuClick;

    public GameObject earPicker;
    public GameObject tailPicker;
    public GameObject basePicker;
    public GameObject eyePicker;
    public GameObject whiskersPicker;
    public GameObject nosePicker;
    public GameObject overlayPicker;
    public GameObject detailsPicker;

    public GameObject backStageButton;
    public GameObject nextStageButton;

    private int stage;

    private void Start()
    {
        PetGlobal pg;
        GameObject petKeeper = GameObject.FindGameObjectWithTag("PetKeeper");
        if (petKeeper == null)
        {
            pg = new PetGlobal();
            pvd = pg.LoadVisuals();
        }
        else
        {
            pvd = PetKeeper.pet.LoadVisuals();
        }

        earPicker.transform.GetChild(1).GetComponent<Text>().text = (pvd.earsIndex + 1) + "/" + petFactory.ears.Length;

        eyePicker.transform.GetChild(3).GetComponent<Text>().text = (pvd.eyesIndex + 1) + "/" + petFactory.eyes.Length;
        eyePicker.transform.GetChild(7).GetComponent<Text>().text = (pvd.eyesSizeIndex + 1) + "/" + petFactory.eyes[pvd.eyesIndex].textures.Length;

        tailPicker.transform.GetChild(1).GetComponent<Text>().text = (pvd.tailIndex + 1) + "/" + petFactory.tails.Length;
        whiskersPicker.transform.GetChild(1).GetComponent<Text>().text = (pvd.whiskersIndex + 1) + "/" + petFactory.whiskers.Length;
        nosePicker.transform.GetChild(1).GetComponent<Text>().text = (pvd.noseIndex + 1) + "/" + petFactory.noses.Length;

        basePicker.transform.GetChild(2).GetComponent<Text>().text = (pvd.baseTextureIndex + 1) + "/" + petFactory.baseTextures.Length;
        basePicker.transform.GetChild(5).GetComponent<Text>().text = (pvd.baseTint + 1) + "/" + PetVisualData.palette.Length;
        basePicker.transform.GetChild(8).GetComponent<Image>().color = PetVisualData.palette[pvd.baseTint];

        overlayPicker.transform.GetChild(2).GetComponent<Text>().text = (pvd.overlayBlendIndex + 1) + "/" + petFactory.overLayBlends.Length;
        overlayPicker.transform.GetChild(5).GetComponent<Text>().text = (pvd.overlayTint + 1) + "/" + PetVisualData.palette.Length;
        overlayPicker.transform.GetChild(8).GetComponent<Image>().color = PetVisualData.palette[pvd.overlayTint];
        overlayPicker.transform.GetChild(10).GetComponent<Text>().text = (pvd.overlayTextureIndex + 1) + "/" + petFactory.baseTextures.Length;

        detailsPicker.transform.GetChild(2).GetComponent<Text>().text = (pvd.detailsBlendIndex + 1) + "/" + petFactory.detailsBlends.Length;
        detailsPicker.transform.GetChild(5).GetComponent<Text>().text = (pvd.DetailsTint + 1) + "/" + PetVisualData.palette.Length;
        detailsPicker.transform.GetChild(8).GetComponent<Image>().color = PetVisualData.palette[pvd.DetailsTint];
        detailsPicker.transform.GetChild(9).GetComponent<Text>().text = (pvd.detailsTextureIndex + 1) + "/" + petFactory.baseTextures.Length;

        stage = 0;

        petFactory.buildPet(pvd);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            onWorldClick();
        }
    }

    public void randomize()
    {
        pvd.earsIndex = Random.Range(0, petFactory.ears.Length);
        earPicker.transform.GetChild(1).GetComponent<Text>().text = (pvd.earsIndex + 1) + "/" + petFactory.ears.Length;

        pvd.eyesIndex = Random.Range(0, petFactory.eyes.Length);
        eyePicker.transform.GetChild(3).GetComponent<Text>().text = (pvd.eyesIndex + 1) + "/" + petFactory.eyes.Length;

        pvd.eyesSizeIndex = Random.Range(0, petFactory.eyes[pvd.eyesIndex].textures.Length);
        eyePicker.transform.GetChild(7).GetComponent<Text>().text = (pvd.eyesSizeIndex + 1) + "/" + petFactory.eyes[pvd.eyesIndex].textures.Length;

        pvd.tailIndex = Random.Range(0, petFactory.tails.Length);
        tailPicker.transform.GetChild(1).GetComponent<Text>().text = (pvd.tailIndex + 1) + "/" + petFactory.tails.Length;
        pvd.whiskersIndex = Random.Range(0, petFactory.whiskers.Length);
        whiskersPicker.transform.GetChild(1).GetComponent<Text>().text = (pvd.whiskersIndex + 1) + "/" + petFactory.whiskers.Length;
        pvd.noseIndex = Random.Range(0, petFactory.noses.Length);
        nosePicker.transform.GetChild(1).GetComponent<Text>().text = (pvd.noseIndex + 1) + "/" + petFactory.noses.Length;

        pvd.baseTextureIndex = Random.Range(0, petFactory.baseTextures.Length);
        basePicker.transform.GetChild(2).GetComponent<Text>().text = (pvd.baseTextureIndex + 1) + "/" + petFactory.baseTextures.Length;
        pvd.baseTint = Random.Range(0, PetVisualData.palette.Length);
        basePicker.transform.GetChild(5).GetComponent<Text>().text = (pvd.baseTint + 1) + "/" + PetVisualData.palette.Length;
        basePicker.transform.GetChild(8).GetComponent<Image>().color = PetVisualData.palette[pvd.baseTint];

        pvd.overlayBlendIndex = Random.Range(0, petFactory.overLayBlends.Length);
        overlayPicker.transform.GetChild(2).GetComponent<Text>().text = (pvd.overlayBlendIndex + 1) + "/" + petFactory.overLayBlends.Length;
        pvd.overlayTint = Random.Range(0, PetVisualData.palette.Length);
        overlayPicker.transform.GetChild(5).GetComponent<Text>().text = (pvd.overlayTint + 1) + "/" + PetVisualData.palette.Length;
        overlayPicker.transform.GetChild(8).GetComponent<Image>().color = PetVisualData.palette[pvd.overlayTint];
        pvd.overlayTextureIndex = Random.Range(0, petFactory.baseTextures.Length);
        overlayPicker.transform.GetChild(10).GetComponent<Text>().text = (pvd.overlayTextureIndex + 1) + "/" + petFactory.baseTextures.Length;

        pvd.detailsBlendIndex = Random.Range(0, petFactory.detailsBlends.Length);
        detailsPicker.transform.GetChild(2).GetComponent<Text>().text = (pvd.detailsBlendIndex + 1) + "/" + petFactory.detailsBlends.Length;
        pvd.DetailsTint = Random.Range(0, PetVisualData.palette.Length);
        detailsPicker.transform.GetChild(5).GetComponent<Text>().text = (pvd.DetailsTint + 1) + "/" + PetVisualData.palette.Length;
        detailsPicker.transform.GetChild(8).GetComponent<Image>().color = PetVisualData.palette[pvd.DetailsTint];
        pvd.detailsTextureIndex = Random.Range(0, petFactory.baseTextures.Length);
        detailsPicker.transform.GetChild(9).GetComponent<Text>().text = (pvd.detailsTextureIndex + 1) + "/" + petFactory.baseTextures.Length;

        petFactory.buildPet(pvd);
    }

    public void onWorldClick()
    {
        PetGlobal pg;
        GameObject petKeeper = GameObject.FindGameObjectWithTag("PetKeeper");
        if (petKeeper == null)
        {
            pg = new PetGlobal();
            pg.SaveVisuals(pvd);
        }
        else
        {
            PetKeeper.pet.SaveVisuals(pvd);
        }

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
        basePicker.SetActive(false);
        eyePicker.SetActive(false);
        whiskersPicker.SetActive(false);
        nosePicker.SetActive(false);
        overlayPicker.SetActive(false);
        detailsPicker.SetActive(false);

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
                basePicker.SetActive(true);
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
            case 6:
                overlayPicker.SetActive(true);
                break;
            case 7:
                detailsPicker.SetActive(true);
                break;
        }
    }

    public void onSectionClick(int newStage)
    {
        stage = newStage;

        GetComponent<AudioSource>().PlayOneShot(A_menuClick);

        updateUI();
    }

    public void onNextEye()
    {
        pvd.eyesIndex++;
        if (pvd.eyesIndex >= petFactory.eyes.Length)
        {
            pvd.eyesIndex = 0;
        }

        pvd.eyesSizeIndex = 0;

        eyePicker.transform.GetChild(3).GetComponent<Text>().text = (pvd.eyesIndex + 1) + "/" + petFactory.eyes.Length;
        eyePicker.transform.GetChild(7).GetComponent<Text>().text = (pvd.eyesSizeIndex + 1) + "/" + petFactory.eyes[pvd.eyesIndex].textures.Length;

        GetComponent<AudioSource>().PlayOneShot(A_arrowClick);

        petFactory.buildPet(pvd);
    }

    public void onPreviousEye()
    {
        pvd.eyesIndex--;
        if (pvd.eyesIndex < 0)
        {
            pvd.eyesIndex = petFactory.eyes.Length - 1;
        }

        pvd.eyesSizeIndex = 0;

        eyePicker.transform.GetChild(3).GetComponent<Text>().text = (pvd.eyesIndex + 1) + "/" + petFactory.eyes.Length;
        eyePicker.transform.GetChild(7).GetComponent<Text>().text = (pvd.eyesSizeIndex + 1) + "/" + petFactory.eyes[pvd.eyesIndex].textures.Length;

        GetComponent<AudioSource>().PlayOneShot(A_arrowClick);

        petFactory.buildPet(pvd);
    }

    public void onNextEyeSize()
    {
        pvd.eyesSizeIndex++;
        if (pvd.eyesSizeIndex >= petFactory.eyes[pvd.eyesIndex].textures.Length)
        {
            pvd.eyesSizeIndex = 0;
        }

        eyePicker.transform.GetChild(7).GetComponent<Text>().text = (pvd.eyesSizeIndex + 1) + "/" + petFactory.eyes[pvd.eyesIndex].textures.Length;

        GetComponent<AudioSource>().PlayOneShot(A_arrowClick);

        petFactory.buildPet(pvd);
    }

    public void onPreviousEyeSize()
    {
        pvd.eyesSizeIndex--;
        if (pvd.eyesSizeIndex < 0)
        {
            pvd.eyesSizeIndex = petFactory.eyes[pvd.eyesIndex].textures.Length - 1;
        }

        eyePicker.transform.GetChild(7).GetComponent<Text>().text = (pvd.eyesSizeIndex + 1) + "/" + petFactory.eyes[pvd.eyesIndex].textures.Length;

        GetComponent<AudioSource>().PlayOneShot(A_arrowClick);

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

        GetComponent<AudioSource>().PlayOneShot(A_arrowClick);

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

        GetComponent<AudioSource>().PlayOneShot(A_arrowClick);

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

        GetComponent<AudioSource>().PlayOneShot(A_arrowClick);

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

        GetComponent<AudioSource>().PlayOneShot(A_arrowClick);

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

        GetComponent<AudioSource>().PlayOneShot(A_arrowClick);

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

        GetComponent<AudioSource>().PlayOneShot(A_arrowClick);

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

        GetComponent<AudioSource>().PlayOneShot(A_arrowClick);

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

        GetComponent<AudioSource>().PlayOneShot(A_arrowClick);

        petFactory.buildPet(pvd);
    }

    public void onNextBaseTexture()
    {
        pvd.baseTextureIndex++;
        if (pvd.baseTextureIndex >= petFactory.baseTextures.Length)
        {
            pvd.baseTextureIndex = 0;
        }

        basePicker.transform.GetChild(2).GetComponent<Text>().text = (pvd.baseTextureIndex + 1) + "/" + petFactory.baseTextures.Length;

        GetComponent<AudioSource>().PlayOneShot(A_arrowClick);

        petFactory.buildPet(pvd);
    }

    public void onPreviousBaseTexture()
    {
        pvd.baseTextureIndex--;
        if (pvd.baseTextureIndex < 0)
        {
            pvd.baseTextureIndex = petFactory.baseTextures.Length - 1;
        }

        basePicker.transform.GetChild(2).GetComponent<Text>().text = (pvd.baseTextureIndex + 1) + "/" + petFactory.baseTextures.Length;

        GetComponent<AudioSource>().PlayOneShot(A_arrowClick);

        petFactory.buildPet(pvd);
    }

    public void onNextOverlayBlend()
    {
        pvd.overlayBlendIndex++;
        if (pvd.overlayBlendIndex >= petFactory.overLayBlends.Length)
        {
            pvd.overlayBlendIndex = 0;
        }

        overlayPicker.transform.GetChild(2).GetComponent<Text>().text = (pvd.overlayBlendIndex + 1) + "/" + petFactory.overLayBlends.Length;

        GetComponent<AudioSource>().PlayOneShot(A_arrowClick);

        petFactory.buildPet(pvd);
    }

    public void onPreviousOverlayBlend()
    {
        pvd.overlayBlendIndex--;
        if (pvd.overlayBlendIndex < 0)
        {
            pvd.overlayBlendIndex = petFactory.overLayBlends.Length - 1;
        }

        overlayPicker.transform.GetChild(2).GetComponent<Text>().text = (pvd.overlayBlendIndex + 1) + "/" + petFactory.overLayBlends.Length;

        GetComponent<AudioSource>().PlayOneShot(A_arrowClick);

        petFactory.buildPet(pvd);
    }

    public void onNextDetailsBlend()
    {
        pvd.detailsBlendIndex++;
        if (pvd.detailsBlendIndex >= petFactory.detailsBlends.Length)
        {
            pvd.detailsBlendIndex = 0;
        }

        detailsPicker.transform.GetChild(2).GetComponent<Text>().text = (pvd.detailsBlendIndex + 1) + "/" + petFactory.detailsBlends.Length;

        GetComponent<AudioSource>().PlayOneShot(A_arrowClick);

        petFactory.buildPet(pvd);
    }

    public void onPreviousDetailsBlend()
    {
        pvd.detailsBlendIndex--;
        if (pvd.detailsBlendIndex < 0)
        {
            pvd.detailsBlendIndex = petFactory.detailsBlends.Length - 1;
        }

        detailsPicker.transform.GetChild(2).GetComponent<Text>().text = (pvd.detailsBlendIndex + 1) + "/" + petFactory.detailsBlends.Length;

        GetComponent<AudioSource>().PlayOneShot(A_arrowClick);

        petFactory.buildPet(pvd);
    }

    public void onNextBaseColor()
    {
        pvd.baseTint++;
        if (pvd.baseTint >= PetVisualData.palette.Length)
        {
            pvd.baseTint = 0;
        }

        basePicker.transform.GetChild(5).GetComponent<Text>().text = (pvd.baseTint + 1) + "/" + PetVisualData.palette.Length;
        basePicker.transform.GetChild(8).GetComponent<Image>().color = PetVisualData.palette[pvd.baseTint];

        GetComponent<AudioSource>().PlayOneShot(A_arrowClick);

        petFactory.buildPet(pvd);
    }

    public void onPreviousBaseColor()
    {
        pvd.baseTint--;
        if (pvd.baseTint < 0)
        {
            pvd.baseTint = PetVisualData.palette.Length - 1;
        }

        basePicker.transform.GetChild(5).GetComponent<Text>().text = (pvd.baseTint + 1) + "/" + PetVisualData.palette.Length;
        basePicker.transform.GetChild(8).GetComponent<Image>().color = PetVisualData.palette[pvd.baseTint];

        GetComponent<AudioSource>().PlayOneShot(A_arrowClick);

        petFactory.buildPet(pvd);
    }

    public void onNextOverlayColor()
    {
        pvd.overlayTint++;
        if (pvd.overlayTint >= PetVisualData.palette.Length)
        {
            pvd.overlayTint = 0;
        }

        overlayPicker.transform.GetChild(5).GetComponent<Text>().text = (pvd.overlayTint + 1) + "/" + PetVisualData.palette.Length;
        overlayPicker.transform.GetChild(8).GetComponent<Image>().color = PetVisualData.palette[pvd.overlayTint];

        GetComponent<AudioSource>().PlayOneShot(A_arrowClick);

        petFactory.buildPet(pvd);
    }

    public void onPreviousOverlayColor()
    {
        pvd.overlayTint--;
        if (pvd.overlayTint < 0)
        {
            pvd.overlayTint = PetVisualData.palette.Length - 1;
        }

        overlayPicker.transform.GetChild(5).GetComponent<Text>().text = (pvd.overlayTint + 1) + "/" + PetVisualData.palette.Length;
        overlayPicker.transform.GetChild(8).GetComponent<Image>().color = PetVisualData.palette[pvd.overlayTint];

        GetComponent<AudioSource>().PlayOneShot(A_arrowClick);

        petFactory.buildPet(pvd);
    }

    public void onNextDetailsColor()
    {
        pvd.DetailsTint++;
        if (pvd.DetailsTint >= PetVisualData.palette.Length)
        {
            pvd.DetailsTint = 0;
        }

        detailsPicker.transform.GetChild(5).GetComponent<Text>().text = (pvd.DetailsTint + 1) + "/" + PetVisualData.palette.Length;
        detailsPicker.transform.GetChild(8).GetComponent<Image>().color = PetVisualData.palette[pvd.DetailsTint];

        GetComponent<AudioSource>().PlayOneShot(A_arrowClick);

        petFactory.buildPet(pvd);
    }

    public void onPreviousDetailsColor()
    {
        pvd.DetailsTint--;
        if (pvd.DetailsTint < 0)
        {
            pvd.DetailsTint = PetVisualData.palette.Length - 1;
        }

        detailsPicker.transform.GetChild(5).GetComponent<Text>().text = (pvd.DetailsTint + 1) + "/" + PetVisualData.palette.Length;
        detailsPicker.transform.GetChild(8).GetComponent<Image>().color = PetVisualData.palette[pvd.DetailsTint];

        GetComponent<AudioSource>().PlayOneShot(A_arrowClick);

        petFactory.buildPet(pvd);
    }

    public void onNextDetailsTexture()
    {
        pvd.detailsTextureIndex++;
        if (pvd.detailsTextureIndex >= petFactory.baseTextures.Length)
        {
            pvd.detailsTextureIndex = 0;
        }

        detailsPicker.transform.GetChild(9).GetComponent<Text>().text = (pvd.detailsTextureIndex + 1) + "/" + petFactory.baseTextures.Length;

        GetComponent<AudioSource>().PlayOneShot(A_arrowClick);

        petFactory.buildPet(pvd);
    }

    public void onPreviousDetailsTexture()
    {
        pvd.detailsTextureIndex--;
        if (pvd.detailsTextureIndex < 0)
        {
            pvd.detailsTextureIndex = petFactory.baseTextures.Length - 1;
        }

        detailsPicker.transform.GetChild(9).GetComponent<Text>().text = (pvd.detailsTextureIndex + 1) + "/" + petFactory.baseTextures.Length;

        GetComponent<AudioSource>().PlayOneShot(A_arrowClick);

        petFactory.buildPet(pvd);
    }

    public void onNextOverlayTexture()
    {
        pvd.overlayTextureIndex++;
        if (pvd.overlayTextureIndex >= petFactory.baseTextures.Length)
        {
            pvd.overlayTextureIndex = 0;
        }

        overlayPicker.transform.GetChild(10).GetComponent<Text>().text = (pvd.overlayTextureIndex + 1) + "/" + petFactory.baseTextures.Length;

        GetComponent<AudioSource>().PlayOneShot(A_arrowClick);

        petFactory.buildPet(pvd);
    }

    public void onPreviousOverlayTexture()
    {
        pvd.overlayTextureIndex--;
        if (pvd.overlayTextureIndex < 0)
        {
            pvd.overlayTextureIndex = petFactory.baseTextures.Length - 1;
        }

        overlayPicker.transform.GetChild(10).GetComponent<Text>().text = (pvd.overlayTextureIndex + 1) + "/" + petFactory.baseTextures.Length;

        GetComponent<AudioSource>().PlayOneShot(A_arrowClick);

        petFactory.buildPet(pvd);
    }
}
