using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour {

	// Use this for initialization
	void Start () {
        if (GameObject.FindGameObjectsWithTag("MusicPlayer").Length > 1)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
	}
}
