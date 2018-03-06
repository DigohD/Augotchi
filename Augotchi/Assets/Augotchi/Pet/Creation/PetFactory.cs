using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetFactory : MonoBehaviour {

    public Mesh[] ears;
    public Material[] eyes;

    public SkinnedMeshRenderer baseRenderer;
    public SkinnedMeshRenderer earsRenderer;

    public void buildPet(PetVisualData petVisualData)
    {
        earsRenderer.sharedMesh = ears[petVisualData.earsIndex];

        Material[] mats = new Material[] { baseRenderer.sharedMaterials[0], baseRenderer.sharedMaterials[1], baseRenderer.sharedMaterials[2], eyes[petVisualData.eyesIndex] };
        baseRenderer.materials = mats;
    }
}
