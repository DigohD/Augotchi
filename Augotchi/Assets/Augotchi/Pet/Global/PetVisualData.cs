using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PetVisualData {

    public int earsIndex;
    public int tailIndex;
    public int colorIndex;

    public int eyesIndex;
    public int eyesSizeIndex;

    public int whiskersIndex;
    public int noseIndex;

    public int baseTextureIndex;
    public int overlayTextureIndex;
    public int detailsTextureIndex;

    public int overlayBlendIndex;
    public int detailsBlendIndex;

    public int baseTint;
    public int overlayTint;
    public int DetailsTint;

    public static Color[] palette = new Color[28] {
        new Color(1f, 0.45f, 0.35f, 1),
        new Color(0.5f, 0.25f, 0.15f, 1),
        new Color(0.95f, 0.45f, 0.25f, 1),
        new Color(0.75f, 0.75f, 0.4f, 1),
        new Color(0.95f, 0.95f, 0.6f, 1),
        new Color(1f, 1f, 0.25f, 1),
        new Color(0.5f, 0.5f, 0.15f, 1),
        new Color(0.45f, 0.8f, 0.4f, 1),
        new Color(0.25f, 0.65f, 0.25f, 1),
        new Color(0.5f, 0.95f, 0.5f, 1),
        new Color(0.4f, 0.8f, 0.6f, 1),
        new Color(0.4f, 0.75f, 0.75f, 1),
        new Color(0.5f, 0.95f, 0.95f, 1),
        new Color(0.4f, 0.5f, 0.75f, 1),
        new Color(0.4f, 0.4f, 0.95f, 1),
        new Color(0.2f, 0.35f, 0.6f, 1),
        new Color(0.6f, 0.4f, 0.8f, 1),
        new Color(0.5f, 0.25f, 0.5f, 1),
        new Color(0.95f, 0.5f, 0.95f, 1),
        new Color(0.8f, 0.4f, 0.55f, 1),
        new Color(0.6f, 0.2f, 1f, 1),
        new Color(0.8f, 0.4f, 0.4f, 1),
        new Color(0.95f, 0.4f, 0.4f, 1),
        new Color(0.9f, 0.9f, 0.9f, 1),
        new Color(0.7f, 0.7f, 0.7f, 1),
        new Color(0.5f, 0.5f, 0.5f, 1),
        new Color(0.3f, 0.3f, 0.3f, 1),
        new Color(0.1f, 0.1f, 0.1f, 1),
    };

    public PetVisualData()
    {
        
    }

}
