using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marker : MonoBehaviour {

	public void picked()
    {
        bool rangeHit;
        int layerMask = 1 << LayerMask.NameToLayer("RangeCircle");
        rangeHit = Physics.Raycast(transform.position + new Vector3(0, 5, 0), Vector3.down, 100, layerMask);

        if (rangeHit)
        {
            PlayerScript.points++;
            GameControl.markerPicked = true;
        }
    }
}
