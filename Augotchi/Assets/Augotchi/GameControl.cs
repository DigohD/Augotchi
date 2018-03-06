using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour {

    public static bool isZooming;

    public static bool markerPicked = true;
    public bool sceneLoaded = false;

    public GameObject P_MarkerFood;
    public GameObject P_MarkerCurrency;
    public GameObject P_MarkerPark;
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

            if(sceneLoaded || PetKeeper.pet.markerSet == null)
                generateNewMarkers();

            sceneLoaded = true;

            for(int i = 0; i < dirs.Length; i++)
            {
                GameObject toSpawn = null;

                switch (PetKeeper.pet.markerSet[i])
                {
                    case Marker.MarkerType.CURRENCY:
                        toSpawn = P_MarkerCurrency;
                        break;
                    case Marker.MarkerType.FOOD:
                        toSpawn = P_MarkerFood;
                        break;
                    case Marker.MarkerType.PARK:
                        toSpawn = P_MarkerPark;
                        break;
                }

                GameObject map = GameObject.FindGameObjectWithTag("Map");
                float scaleConversion = map.transform.localScale.x;
                GameObject marker = Instantiate(toSpawn, player.transform.position + (dirs[i].normalized * Random.Range(45.0f * scaleConversion, 120f * scaleConversion)), Quaternion.identity);
                //GameObject marker = Instantiate(toSpawn, player.transform.position + (dirs[i].normalized * Random.Range(4.0f * scaleConversion, 12f * scaleConversion)), Quaternion.identity);

                marker.transform.SetParent(augotchiMap.transform);
                Instantiate(P_MarkerPoof, marker.transform.position, Quaternion.identity);

                marker.transform.localScale = new Vector3(1, 1, 1);

                marker.GetComponent<Marker>().gc = this;
            }
        }
	}

    public void spawnRewardText(string message)
    {
        Quaternion rot = GameObject.FindGameObjectWithTag("CameraTarget").transform.localRotation;
        GameObject go = Instantiate(P_RewardText, player.transform.position, rot);
        go.GetComponent<RewardMessage>().setMessage(message);
    }

    private void generateNewMarkers()
    {
        Marker.MarkerType[] newMarkers = new Marker.MarkerType[8];
        for (int i = 0; i < 8; i++)
        {
            int rnd = Random.Range(0, 1000);

            if(rnd < 600)
            {
                newMarkers[i] = Marker.MarkerType.CURRENCY;
            }
            else if(rnd < 950)
            {
                newMarkers[i] = Marker.MarkerType.FOOD;
            }
            else
            {
                newMarkers[i] = Marker.MarkerType.PARK;
            }
        }

        PetKeeper.pet.setMarkers(newMarkers);
    }
}
