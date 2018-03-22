using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VersionScript : MonoBehaviour {
    void FixedUpdate()
    {
        GetComponent<Text>().text = "V " + Application.version;
    }
}
