using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitScript : MonoBehaviour {

	// Use this for initialization
	void Start () {}
	
	// Update is called once per frame
	void Update () {}

    

    public void QuitApp()
    {
        if (!Application.isEditor)
        {
            Application.Quit();
            Debug.Log("Game is exiting");
            //Just to make sure its working
        }
        else{
            Debug.Log("Game is exiting");
            //Just to make sure its working
        }
    }

}
