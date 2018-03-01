using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldButton : MonoBehaviour {

    public void onClick()
    {
        GameControl.markerPicked = true;
        SceneManager.LoadScene("World");
    }
}
