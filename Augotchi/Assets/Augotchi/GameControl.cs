using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour {

    public static Color XPColor = new Color(0.6f, 0.8f, 0.8f);
    public static Color LevelUpColor = new Color(1f, 1f, 0.5f);

    public static bool isZooming;

    public static bool firstStartup = false;

    public static bool markerPicked = true;
    public bool sceneLoaded = false;

    public AudioClip A_RewardPop;

    public GameObject P_MarkerFood;
    public GameObject P_MarkerCurrency;
    public GameObject P_MarkerPark;
    public GameObject P_MarkerRevive;
    public GameObject P_MarkerCrate;

    public GameObject P_RewardText;
    public GameObject P_MarkerPoof;

    public GameObject Introduction;

    GameObject player;

    GameObject augotchiMap;

    private static ArrayList rewardStack = new ArrayList();

    private float rewardTimer;

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

        if (firstStartup)
        {
            Introduction.SetActive(true);
            firstStartup = false;
        }
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

            if((sceneLoaded || PetKeeper.pet.markerSet == null) && !PetKeeper.pet.isDead)
                generateNewMarkers();

            sceneLoaded = true;

            if (!PetKeeper.pet.isDead)
            {
                for (int i = 0; i < dirs.Length; i++)
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
                        case Marker.MarkerType.CRATE:
                            toSpawn = P_MarkerCrate;
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
            else
            {
                for (int i = 0; i < dirs.Length; i++)
                {
                    GameObject toSpawn = P_MarkerRevive;

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

        rewardTimer += Time.deltaTime;
        if(rewardStack.Count > 0 && rewardTimer > 1f)
        {
            rewardTimer = 0;
            spawnRewardText((Reward) rewardStack[0]);
            rewardStack.RemoveAt(0);
        }
	}

    public void queueRewardText(string message, Color c)
    {
        Reward reward = new Reward();
        reward.message = message;
        reward.color = c;
        rewardStack.Add(reward);
    }

    private void spawnRewardText(Reward reward)
    {
        float rot = GameObject.FindGameObjectWithTag("CameraTarget").transform.localRotation.eulerAngles.y;
        GameObject go = Instantiate(P_RewardText, player.transform.position, Quaternion.Euler(30, rot, 0));

        GetComponent<AudioSource>().PlayOneShot(A_RewardPop);

        go.GetComponent<RewardMessage>().setMessage(reward);
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
            else if(rnd < 925)
            {
                newMarkers[i] = Marker.MarkerType.FOOD;
            }
            else
            {
                newMarkers[i] = Marker.MarkerType.CRATE;
            }
        }

        PetKeeper.pet.setMarkers(newMarkers);
    }
}
