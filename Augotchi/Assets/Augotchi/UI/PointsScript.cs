using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointsScript : MonoBehaviour {

	void Update () {
        GetComponent<Text>().text = "Steps: " + PlayerScript.steps + "/" + 100;
        transform.GetChild(0).GetComponent<Text>().text = "Steps: " + PlayerScript.steps + "/" + 100;
    }
}
