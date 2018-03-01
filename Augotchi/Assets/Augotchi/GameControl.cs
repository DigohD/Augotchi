using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour {

    public static bool markerPicked = true;

    public GameObject P_Marker;

    GameObject player;

    GameObject augotchiMap;

    Vector3[] dirs = new Vector3[8]
    {
        new Vector3(1, 0, 0),
        new Vector3(1, 0, 1),
        new Vector3(0, 0, 1),
        new Vector3(-1, 0, 1),
        new Vector3(-1, 0, 0),
        new Vector3(-1, 0, -1),
        new Vector3(0, 0, -1),
        new Vector3(1, 0, -1)
    };

	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");

        augotchiMap = GameObject.FindGameObjectWithTag("AugotchiMap");
    }
	
	void Update () {
        if (markerPicked)
        {
            markerPicked = false;

            foreach(GameObject marker in GameObject.FindGameObjectsWithTag("Marker"))
            {
                Destroy(marker);
            }

            for(int i = 0; i < dirs.Length; i++)
            {
                GameObject map = GameObject.FindGameObjectWithTag("Map");
                float scaleConversion = map.transform.localScale.x;
                GameObject marker = Instantiate(P_Marker, player.transform.position + (dirs[i].normalized * Random.Range(40.0f * scaleConversion, 80.0f * scaleConversion)), Quaternion.identity);
                marker.transform.SetParent(augotchiMap.transform);

                
                marker.transform.localScale = new Vector3(1, 1, 1);
            }
        }
	}
}
