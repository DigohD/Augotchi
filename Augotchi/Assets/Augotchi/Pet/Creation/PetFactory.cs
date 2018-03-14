using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PetFactory : MonoBehaviour {

    [Serializable]
    public class TextureList
    {
        public Texture2D[] textures;
    }

    [Serializable]
    public class GameObjectList
    {
        public GameObject[] gameObjects;
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

    public Transform hatParent;
    public GameObjectList[] hats;

    public Transform faceParent;
    public GameObjectList[] faces;

    private void Start()
    {
        PetGlobal pg = new PetGlobal();

        filterUnlocks(pg.LoadUnlocks());
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

        foreach (Transform t in hatParent)
            Destroy(t.gameObject);

        try
        {
            GameObject hat = (GameObject)Instantiate(hats[petVisualData.hatIndex].gameObjects[petVisualData.hatVariation]);
            hat.transform.SetParent(hatParent, false);
        }
        catch (IndexOutOfRangeException e)
        {
            petVisualData.hatIndex = 0;
            petVisualData.hatVariation = 0;
        }

        foreach (Transform t in faceParent)
            Destroy(t.gameObject);

        try
        {
            GameObject face = (GameObject)Instantiate(faces[petVisualData.faceIndex].gameObjects[petVisualData.faceVariations]);
            face.transform.SetParent(faceParent, false);
        }catch(IndexOutOfRangeException e)
        {
            petVisualData.faceIndex = 0;
            petVisualData.faceVariations = 0;
        }
        
    }

    public void setDeadEyes()
    {
        Material eyesMat = baseRenderer.materials[3];
        eyesMat.SetTexture("_MainTex", deadEyes);

        Material[] mats = new Material[] { baseRenderer.materials[0], baseRenderer.materials[1], baseRenderer.materials[2], eyesMat };

        baseRenderer.materials = mats;
    }

    private void filterUnlocks(PetUnlocksData pud)
    {
        filterUnlockedHats(pud);
        filterUnlockedFaces(pud);
    }

    private void filterUnlockedHats(PetUnlocksData pud)
    {
        GameObjectList[] unlockedHats = new GameObjectList[hats.Length];
        ArrayList extractedHats = new ArrayList();

        unlockedHats[0] = hats[0];

        for (int i = 1; i < hats.Length; i++)
        {
            unlockedHats[i] = new GameObjectList();
            extractedHats.Clear();

            for (int j = 0; j < hats[i].gameObjects.Length; j++)
            {
                if (pud.isHatUnlocked(i, j))
                {
                    extractedHats.Add(hats[i].gameObjects[j]);
                }
            }

            unlockedHats[i].gameObjects = new GameObject[extractedHats.Count];
            for (int k = 0; k < extractedHats.Count; k++)
            {
                unlockedHats[i].gameObjects[k] = (GameObject)extractedHats[k];
            }
        }

        unlockedHats = unlockedHats.Where(hl => hl.gameObjects.Length > 0).ToArray();

        hats = unlockedHats;
    }

    private void filterUnlockedFaces(PetUnlocksData pud)
    {
        GameObjectList[] unlockedFaces = new GameObjectList[faces.Length];
        ArrayList extractedFaces = new ArrayList();

        unlockedFaces[0] = faces[0];

        for (int i = 1; i < faces.Length; i++)
        {
            unlockedFaces[i] = new GameObjectList();
            extractedFaces.Clear();

            for (int j = 0; j < faces[i].gameObjects.Length; j++)
            {
                if (pud.isFaceUnlocked(i, j))
                {
                    extractedFaces.Add(faces[i].gameObjects[j]);
                }
            }

            unlockedFaces[i].gameObjects = new GameObject[extractedFaces.Count];
            for (int k = 0; k < extractedFaces.Count; k++)
            {
                unlockedFaces[i].gameObjects[k] = (GameObject) extractedFaces[k];
            }
        }

        unlockedFaces = unlockedFaces.Where(fl => fl.gameObjects.Length > 0).ToArray();

        faces = unlockedFaces;
    }
}
