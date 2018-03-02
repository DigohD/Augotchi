using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour {

    public static bool markerPicked = true;

    public GameObject P_Marker;
    public GameObject P_RewardText;
    public GameObject P_MarkerPoof;

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

        Application.targetFrameRate = 30;
    }
	
	void Update () {
        if (markerPicked)
        {
            markerPicked = false;

            foreach(GameObject marker in GameObject.FindGameObjectsWithTag("Marker"))
            {
                Instantiate(P_MarkerPoof, marker.transform.position, Quaternion.identity);
                Destroy(marker);
            }

            for(int i = 0; i < dirs.Length; i++)
            {
                GameObject map = GameObject.FindGameObjectWithTag("Map");
                float scaleConversion = map.transform.localScale.x;
                GameObject marker = Instantiate(P_Marker, player.transform.position + (dirs[i].normalized * Random.Range(45.0f * scaleConversion, 120f * scaleConversion)), Quaternion.identity);
                marker.transform.SetParent(augotchiMap.transform);
                Instantiate(P_MarkerPoof, marker.transform.position, Quaternion.identity);

                marker.transform.localScale = new Vector3(1, 1, 1);

                marker.GetComponent<Marker>().gc = this;
            }
        }
	}

    public void spawnRewardText(string message)
    {
        GameObject go = Instantiate(P_RewardText, player.transform.position, Quaternion.identity);
        go.GetComponent<RewardMessage>().setMessage(message);
    }
}
