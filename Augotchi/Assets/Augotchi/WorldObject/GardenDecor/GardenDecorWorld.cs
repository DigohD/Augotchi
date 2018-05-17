using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardenDecorWorld : MonoBehaviour {

    public AudioClip A_GardenDecorGrow;
    public AudioClip A_GardenDecorRemove;

    public BaseGardenDecor representedDecor;

    protected GameControl gc;

    private float delayedSpawnTime;
    public bool hasSpawned;
    float spawnTimer = 0;

    public GameObject G_Removal;

    public GameObject[] variations;

    public virtual void init(BaseGardenDecor representedDecor)
    {
        this.representedDecor = representedDecor;

        delayedSpawnTime = UnityEngine.Random.Range(0.5f, 1.5f);

        updateVisual();
    }

    void FixedUpdate()
    {
        spawnTimer += Time.fixedDeltaTime;

        if (representedDecor == null)
            return;
        
        if (!hasSpawned && spawnTimer > delayedSpawnTime)
        {
            float spawnTimer2 = spawnTimer - delayedSpawnTime;

            float scale = Mathf.Sin(Mathf.PI * spawnTimer2) * representedDecor.scale;

            if (!GetComponent<AudioSource>().isPlaying)
                GetComponent<AudioSource>().PlayOneShot(A_GardenDecorGrow);

            if (spawnTimer2 >= 0.5f)
            {
                scale = representedDecor.scale;
                hasSpawned = true;
            }

            transform.localScale = Vector3.one * scale;
        }
    }

    public void showRemoval()
    {
        G_Removal.SetActive(true);
    }

    public void hideRemoval()
    {
        G_Removal.SetActive(false);
    }

    public void remove()
    {
        PetKeeper.pet.Base.baseGardenDecors.Remove(representedDecor);

        GameControl.playPostMortemAudioClip(A_GardenDecorRemove);

        Destroy(gameObject);

        GardenDecor decorInfo = Inventory.getGardenDecorTypeInfo(representedDecor.gardenDecorType);

        PetKeeper.pet.buildingMaterials += decorInfo.bmCost;
        PetKeeper.pet.inventory.gardenDecorCounts[(int) decorInfo.gardenDecorType] += 1;

        InventoryUI.reRender = true;

        PetKeeper.pet.Save(false);
    }

    public void nextVariation()
    {
        representedDecor.variation++;
        if (representedDecor.variation >= variations.Length)
            representedDecor.variation = 0;

        updateVisual();
    }

    public void previousVariation()
    {
        representedDecor.variation--;
        if (representedDecor.variation < 0)
            representedDecor.variation = variations.Length - 1;

        updateVisual();
    }

    public void updateVisual()
    {
        foreach(GameObject go in variations)
        {
            go.SetActive(false);
        }

        variations[representedDecor.variation].SetActive(true);
    }
}
