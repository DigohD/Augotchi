using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Marker : MonoBehaviour {

    public GameObject P_MarkerPoof;

    public GameControl gc;

    public enum MarkerType { FOOD, CURRENCY, PARK, CRATE }

    public bool isIndividual = false;

	public virtual void picked()
    {
        bool rangeHit;
        int layerMask = 1 << LayerMask.NameToLayer("RangeCircle");
        rangeHit = Physics.Raycast(transform.position + new Vector3(0, 5, 0), Vector3.down, 100, layerMask);

        if (rangeHit)
        {
            if(!isIndividual)
                GameControl.markerPicked = true;

            Instantiate(P_MarkerPoof, transform.position, Quaternion.identity);

            executeEffect();

            if (isIndividual)
                Destroy(gameObject);
        }
    }

    protected abstract void executeEffect();
}
