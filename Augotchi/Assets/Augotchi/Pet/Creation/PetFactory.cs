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

    private void Start()
    {
        PetGlobal pg = new PetGlobal();

        buildPet(pg.LoadVisuals());
    }

    public void buildPet(PetVisualData petVisualData)
    {
        earsRenderer.sharedMesh = ears[petVisualData.earsIndex];
        tailRenderer.sharedMesh = tails[petVisualData.tailIndex];

        Material[] mats = new Material[] { baseRenderer.sharedMaterials[0], noses[petVisualData.noseIndex], whiskers[petVisualData.whiskersIndex], eyes[petVisualData.eyesIndex] };
        baseRenderer.materials = mats;
    }
}
