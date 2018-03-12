using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetFactory : MonoBehaviour {

    [Serializable]
    public class TextureList
    {
        public Texture2D[] textures;
    }

    public Mesh[] ears;
    public Mesh[] tails;

    
    public TextureList[] eyes;

    public Texture2D[] whiskers;
    public Texture2D[] noses;

    public SkinnedMeshRenderer baseRenderer;
    public SkinnedMeshRenderer earsRenderer;
    public SkinnedMeshRenderer tailRenderer;

    public Texture2D[] baseTextures;
    public Texture2D[] baseBlends;

    public Texture2D[] overLayBlends;

    public Texture2D[] detailsBlends;

    public Texture2D deadEyes;

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

        Material eyesMat = baseRenderer.materials[3];
        eyesMat.SetTexture("_MainTex", eyes[petVisualData.eyesIndex].textures[petVisualData.eyesSizeIndex]);

        Material noseMat = baseRenderer.materials[1];
        noseMat.SetTexture("_MainTex", noses[petVisualData.noseIndex]);

        Material whiskerMat = baseRenderer.materials[2];
        whiskerMat.SetTexture("_MainTex", whiskers[petVisualData.whiskersIndex]);
        whiskerMat.SetColor("_Color", PetVisualData.palette[petVisualData.DetailsTint]);

        Material[] mats = new Material[] { baseMat, noseMat, whiskerMat, eyesMat};
        baseRenderer.materials = mats;

        Material[] peripheryMats = new Material[] { baseMat };
        earsRenderer.materials = peripheryMats;
        tailRenderer.materials = peripheryMats;
    }

    public void setDeadEyes()
    {
        Material eyesMat = baseRenderer.materials[3];
        eyesMat.SetTexture("_MainTex", deadEyes);

        Material[] mats = new Material[] { baseRenderer.materials[0], baseRenderer.materials[1], baseRenderer.materials[2], eyesMat };

        baseRenderer.materials = mats;
    }
}
