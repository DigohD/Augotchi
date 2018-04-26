using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BushPlot : FarmPlot {

    public Material planesMat;
    public Material produceMat;

    public GameObject G_PlanesMesh;
    public GameObject G_ProduceMesh;
    public GameObject G_BushMesh;

    override
    public void init(Seed seedInfo, GardenCrop gardenCrop, GameControl gc, bool onPlant)
    {
        base.init(seedInfo, gardenCrop, gc, onPlant);

        G_PlanesMesh.GetComponent<MeshRenderer>().material.mainTextureOffset = new Vector2(0f, 0);

        G_PlanesMesh.GetComponent<MeshRenderer>().materials = new Material[1] { planesMat };
        G_ProduceMesh.GetComponent<MeshRenderer>().materials = new Material[1] { produceMat };
    }

    protected override void updateVisuals(float percent)
    {
        if (percent >= 1)
        {
            G_PlanesMesh.SetActive(false);
            G_BushMesh.SetActive(true);
            G_ProduceMesh.SetActive(true);
        }
        else if (percent > 0.8f)
        {
            G_PlanesMesh.SetActive(false);
            G_BushMesh.SetActive(true);
            G_ProduceMesh.SetActive(false);
        }
        else if (percent > 0.6f)
        {
            G_PlanesMesh.GetComponent<MeshRenderer>().material.mainTextureOffset = new Vector2(0.75f, 0);
        }
        else if (percent > 0.4f)
        {
            G_PlanesMesh.GetComponent<MeshRenderer>().material.mainTextureOffset = new Vector2(0.5f, 0);
        }
        else if (percent > 0.2f)
        {
            G_PlanesMesh.GetComponent<MeshRenderer>().material.mainTextureOffset = new Vector2(0.25f, 0);
        }
        else if (percent > 0)
        {
            G_PlanesMesh.GetComponent<MeshRenderer>().material.mainTextureOffset = new Vector2(0f, 0);
        }
    }
}
