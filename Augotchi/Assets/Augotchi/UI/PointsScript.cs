using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointsScript : MonoBehaviour {

	void Update () {
        GetComponent<Text>().text = "Points: " + PlayerScript.points;
        transform.GetChild(0).GetComponent<Text>().text = "Points: " + PlayerScript.points;
    }
}
