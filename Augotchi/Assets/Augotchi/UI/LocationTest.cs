using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocationTest : MonoBehaviour {


    LocationService ls;

    void Start()
    {
        ls = new LocationService();
        ls.Start(1, 1);
    }

    void Update () {
        GetComponent<Text>().text = "" + ls.lastData.timestamp;
	}
}
