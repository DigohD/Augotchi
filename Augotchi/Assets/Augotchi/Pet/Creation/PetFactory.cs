using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetFactory : MonoBehaviour {

    public Mesh[] ears;
    public Mesh[] tails;
    public Material[] eyes;
    public Material[] whiskers;
    public Material[] noses;

    public SkinnedMeshRenderer baseRenderer;
    public SkinnedMeshRenderer earsRenderer;
    public SkinnedMeshRenderer tailRenderer;

    public Texture2D[] baseTextures;
    public Texture2D[] baseBlends;

    public Texture2D[] overLayBlends;

    public Texture2D[] detailsBlends;

    private void Start()
    {
        PetGlobal pg = new PetGlobal();

        buildPet(pg.LoadVisuals());
    }

    public void buildPet(PetVisualData petVisualData)
    {
        earsRenderer.sharedMesh = ears[petVisualData.earsIndex];
        tailRenderer.sharedMesh = tails[petVisualData.tailIndex];

        Material baseMat = baseRenderer.materials[0];
        baseMat.SetTexture("_MainTex", baseTextures[petVisualData.baseTextureIndex]);
        baseMat.SetTexture("_OffTex", baseTextures[petVisualData.overlayTextureIndex]);
        baseMat.SetTexture("_MiscTex", baseTextures[petVisualData.detailsTextureIndex]);

        baseMat.SetTexture("_MapTex", baseBlends[0]);
        baseMat.SetTexture("_MapTex2", overLayBlends[petVisualData.overlayBlendIndex]);
        baseMat.SetTexture("_MapTex3", detailsBlends[petVisualData.detailsBlendIndex]);

        baseMat.SetColor("_Color", PetVisualData.palette[petVisualData.baseTint]);
        baseMat.SetColor("_Color2", PetVisualData.palette[petVisualData.overlayTint]);
        baseMat.SetColor("_Color3", PetVisualData.palette[petVisualData.DetailsTint]);

        /*baseMat.SetColor("_Color", PetVisualData.palette[0]);
        baseMat.SetColor("_Color2", PetVisualData.palette[7]);
        baseMat.SetColor("_Color3", PetVisualData.palette[14]);*/

        Material[] mats = new Material[] { baseMat, noses[petVisualData.noseIndex], whiskers[petVisualData.whiskersIndex], eyes[petVisualData.eyesIndex] };
        baseRenderer.materials = mats;

        Material[] peripheryMats = new Material[] { baseMat };
        earsRenderer.materials = peripheryMats;
        tailRenderer.materials = peripheryMats;
    }
}
