using Mapbox.Unity.Map;
using Mapbox.Unity.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSynchWorldButton : MonoBehaviour {

    public GameControl gc;

	public void onClick()
    {
        GameObject Map = GameObject.FindGameObjectWithTag("Map");
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        string longLat = VectorExtensions.GetGeoPosition(
                        player.transform,
                        Map.GetComponent<BasicMap>().CenterMercator,
                        Map.GetComponent<BasicMap>().WorldRelativeScale
                    ).ToString();

        StartCoroutine(gc.PostTestMarkerPositions(longLat));
    }
}
