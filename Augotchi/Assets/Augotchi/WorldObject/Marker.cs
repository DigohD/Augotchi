using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marker : MonoBehaviour {

    public GameObject P_MarkerPoof;

    public GameControl gc;

	public void picked()
    {
        bool rangeHit;
        int layerMask = 1 << LayerMask.NameToLayer("RangeCircle");
        rangeHit = Physics.Raycast(transform.position + new Vector3(0, 5, 0), Vector3.down, 100, layerMask);

        if (rangeHit)
        {
            GameControl.markerPicked = true;

            Instantiate(P_MarkerPoof, transform.position, Quaternion.identity);

            switch (Random.Range(0, 3))
            {
                case 0:
                    PetKeeper.pet.candy += 1;
                    gc.spawnRewardText("Candy +1");
                    break;
                case 1:
                    PetKeeper.pet.food += 1;
                    gc.spawnRewardText("Canned Food +1");
                    break;
                case 2:
                    PetKeeper.pet.vegetables += 1;
                    gc.spawnRewardText("Vegetable +1");
                    break;
            }
        }
    }
}
