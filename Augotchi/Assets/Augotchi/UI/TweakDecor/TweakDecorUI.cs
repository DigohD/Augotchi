using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TweakDecorUI : MonoBehaviour {

    public GameControl gc;

    private bool isLeftRotDown;
    private bool isRightRotDown;

    private bool isUpOffsetDown;
    private bool isDownOffsetDown;

    public Text variationText;

    private void Awake()
    {
        GetComponentInChildren<Slider>().value = 0.5f;
        onScaleSliderChange(0.5f);
    }

    public void onLeftRotDown()
    {
        isLeftRotDown = true;
    }

    public void onLeftRotUp()
    {
        isLeftRotDown = false;
    }

    public void onRightRotDown()
    {
        isRightRotDown = true;
    }

    public void onRightRotUp()
    {
        isRightRotDown = false;
    }

    public void onUpOffsetDown()
    {
        isUpOffsetDown = true;
    }

    public void onUpOffsetUp()
    {
        isUpOffsetDown = false;
    }

    public void onDownOffsetDown()
    {
        isDownOffsetDown = true;
    }

    public void onDownOffsetUp()
    {
        isDownOffsetDown = false;
    }

    public void onScaleSliderChange(float newValue)
    {
        gc.tweakGardenDecorScale(0.8f + newValue);
    }

    void Update () {
        if (isLeftRotDown)
        {
            gc.tweakGardenDecorRotation(150f);
        }
        else if (isRightRotDown)
        {
            gc.tweakGardenDecorRotation(-150f);
        }

        if (isUpOffsetDown)
        {
            gc.tweakGardenDecorOffset(8f);
        }
        else if (isDownOffsetDown)
        {
            gc.tweakGardenDecorOffset(-8f);
        }

        GardenDecorWorld gdw = gc.tweakedGardenDecor.GetComponent<GardenDecorWorld>();
        variationText.text = (gdw.representedDecor.variation + 1) + "/" + gdw.variations.Length;
    }
}
