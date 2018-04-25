using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundPlanesPlot : FarmPlot {

    public Material planesMat;
    public Material produceMat;

    public GameObject G_PlanesMesh;
    public GameObject G_ProduceMesh;

    override
    public void init(Seed seedInfo, GardenCrop gardenCrop)
    {
        base.init(seedInfo, gardenCrop);

        G_PlanesMesh.GetComponent<MeshRenderer>().material.mainTextureOffset = new Vector2(0f, 0);

        G_PlanesMesh.GetComponent<MeshRenderer>().materials = new Material[1] { planesMat };
        G_ProduceMesh.GetComponent<MeshRenderer>().materials = new Material[1] { produceMat };
    }

    protected override void updateVisuals(float percent)
    {
        if(percent >= 1)
        {
            G_PlanesMesh.SetActive(false);
            G_ProduceMesh.SetActive(true);
        }
        else if(percent > 0.75f)
        {
            G_PlanesMesh.GetComponent<MeshRenderer>().material.mainTextureOffset = new Vector2(0.75f, 0);
        }
        else if(percent > 0.5f)
        {
            G_PlanesMesh.GetComponent<MeshRenderer>().material.mainTextureOffset = new Vector2(0.5f, 0);
        }
        else if(percent > 0.25f)
        {
            G_PlanesMesh.GetComponent<MeshRenderer>().material.mainTextureOffset = new Vector2(0.25f, 0);
        }
        else if(percent > 0)
        {
            G_PlanesMesh.GetComponent<MeshRenderer>().material.mainTextureOffset = new Vector2(0f, 0);
        }
    }
}
