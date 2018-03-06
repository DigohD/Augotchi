using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetFactory : MonoBehaviour {

    public Mesh[] ears;

    public SkinnedMeshRenderer earsRenderer;

	void Start () {
        
	}
	
	void Update () {
		
	}

    public void buildPet(PetVisualData petVisualData)
    {
        earsRenderer.sharedMesh = ears[petVisualData.earsIndex];
    }
}
