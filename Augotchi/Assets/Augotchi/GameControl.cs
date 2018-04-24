using Mapbox.Unity.Map;
using Mapbox.Unity.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour {

    private bool isInitialized = false;

    public static float[] markerRelativeDistances = new float[] { 0, 0, 0, 0, 0, 0, 0, 0 };

    public static float zoomValue = 17f;
    public static Quaternion rotation = Quaternion.identity;

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

    public GameObject P_Base;
    public GameObject P_FarmPlot;

    public GameObject Introduction;

    public GameObject[] markerRefs = new GameObject[8];

    GameObject player;

    GameObject Map;
    GameObject augotchiMap;

    private static ArrayList rewardStack = new ArrayList();

    private float rewardTimer;

    private bool markersOutOfRange = false;

    private bool isPlantingSeed = false;
    private Seed seedToPlant = null;

    public GameObject loadingScreen;

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
        Map = GameObject.FindGameObjectWithTag("Map");

        Application.targetFrameRate = 30;

        if (firstStartup)
        {
            Introduction.SetActive(true);
            firstStartup = false;
        }

        test();
    }

    private void initBase()
    {
        if(PetKeeper.pet.Base.longLat == null)
        {
            return;
        }

        GameObject Map = GameObject.FindGameObjectWithTag("Map");

        string[] parts = PetKeeper.pet.Base.longLat.Split(',');
        float longitude = float.Parse(parts[0]);
        float latitude = float.Parse(parts[1]);
        Vector3 fromLongLat = VectorExtensions.AsUnityPosition(new Vector2(latitude, longitude), Map.GetComponent<BasicMap>().CenterMercator, Map.GetComponent<BasicMap>().WorldRelativeScale);

        GameObject newBase = (GameObject) Instantiate(P_Base, fromLongLat, Quaternion.identity);
        newBase.transform.SetParent(Map.transform, false);
        newBase.transform.localScale = Vector3.one;

        foreach(GardenCrop crop in PetKeeper.pet.Base.gardenCrops)
        {
            parts = crop.longLat.Split(',');
            longitude = float.Parse(parts[0]);
            latitude = float.Parse(parts[1]);
            fromLongLat = VectorExtensions.AsUnityPosition(
                new Vector2(latitude, longitude), 
                Map.GetComponent<BasicMap>().CenterMercator, 
                Map.GetComponent<BasicMap>().WorldRelativeScale
            );

            GameObject newPlot = (GameObject) Instantiate(P_FarmPlot, fromLongLat, Quaternion.identity);
            newPlot.transform.SetParent(Map.transform, false);
            newPlot.transform.localScale = Vector3.one;
        }
    }

    private void updateBase()
    {
        GameObject baseObject = GameObject.FindGameObjectWithTag("Base");
        if (baseObject == null || PetKeeper.pet.Base.longLat == null)
            return;

        string[] parts = PetKeeper.pet.Base.longLat.Split(',');
        float longitude = float.Parse(parts[0]);
        float latitude = float.Parse(parts[1]);
        Vector3 fromLongLat = VectorExtensions.AsUnityPosition(
            new Vector2(latitude, longitude), 
            Map.GetComponent<BasicMap>().CenterMercator, 
            Map.GetComponent<BasicMap>().WorldRelativeScale
        );

        Debug.LogWarning("House pos updated");

        baseObject.transform.position = fromLongLat;
    }

    void test()
    {
        StartCoroutine( GetTestMarkerPositions());
    }

    private void FixedUpdate()
    {
        if (markerRefs[0] == null)
            return; 
        float shortestDistance = float.MaxValue;
        float totalDistance = 0;

        GameObject map = GameObject.FindGameObjectWithTag("Map");
        float scaleConversion = map.transform.localScale.x;

        foreach (GameObject go in markerRefs)
        {
            if ((player.transform.position - go.transform.position).magnitude < shortestDistance)
            {
                shortestDistance = (player.transform.position - go.transform.position).magnitude;
            }
            totalDistance += (player.transform.position - go.transform.position).magnitude;
        }

        float avarageDistance = totalDistance / 8f;

        float threshold = 50f * scaleConversion;
        float avarageThreshold = 145f * scaleConversion;

        if (shortestDistance > threshold && avarageDistance > avarageThreshold)
        {
            markersOutOfRange = true;
            markerPicked = true;
        }
    }

    float loadTimer = 0;
    void Update () {
        loadTimer += Time.deltaTime;
        if (!isInitialized && loadTimer > 5f)
        {
            initBase();
            isInitialized = true;
            loadingScreen.SetActive(false);
        }
        else if (loadTimer < 5f)
            return;

        if (markerPicked)
        {
            markerPicked = false;

            foreach(GameObject marker in GameObject.FindGameObjectsWithTag("Marker"))
            {
                Instantiate(P_MarkerPoof, marker.transform.position, Quaternion.identity);
                Destroy(marker);
            }

            if (!markersOutOfRange && (sceneLoaded || PetKeeper.pet.markerSet == null) && !PetKeeper.pet.isDead)
            {
                generateNewMarkers();
                markerRelativeDistances = new float[] { 0, 0, 0, 0, 0, 0, 0, 0 };
            }else if (markersOutOfRange)
            {
                markerRelativeDistances = new float[] { 0, 0, 0, 0, 0, 0, 0, 0 };
                markersOutOfRange = false;
            }

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

                    float distance = markerRelativeDistances[i];
                    if(distance == 0)
                    {
                        distance = UnityEngine.Random.Range(45.0f * scaleConversion, 120f * scaleConversion);
                        markerRelativeDistances[i] = distance;
                    }

                    GameObject marker = Instantiate(toSpawn, player.transform.position + (dirs[i].normalized * distance), Quaternion.identity);
                    //GameObject marker = Instantiate(toSpawn, player.transform.position + (dirs[i].normalized * Random.Range(4.0f * scaleConversion, 12f * scaleConversion)), Quaternion.identity);

                    markerRefs[i] = marker;

                    marker.transform.SetParent(augotchiMap.transform);
                    GameObject poof = (GameObject) Instantiate(P_MarkerPoof, marker.transform.position, Quaternion.identity);
                    poof.transform.SetParent(marker.transform);

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

                    float distance = markerRelativeDistances[i];
                    if (distance == 0)
                    {
                        distance = UnityEngine.Random.Range(45.0f * scaleConversion, 120f * scaleConversion);
                        markerRelativeDistances[i] = distance;
                    }

                    GameObject marker = Instantiate(toSpawn, player.transform.position + (dirs[i].normalized * distance), Quaternion.identity);
                    //GameObject marker = Instantiate(toSpawn, player.transform.position + (dirs[i].normalized * Random.Range(4.0f * scaleConversion, 12f * scaleConversion)), Quaternion.identity);

                    markerRefs[i] = marker;

                    marker.transform.SetParent(augotchiMap.transform);
                    Instantiate(P_MarkerPoof, marker.transform.position, Quaternion.identity);

                    marker.transform.localScale = new Vector3(1, 1, 1);

                    marker.GetComponent<Marker>().gc = this;


                    // TESTING!

                    /*string longLat = VectorExtensions.GetGeoPosition(
                        marker.transform,
                        Map.GetComponent<BasicMap>().CenterMercator,
                        Map.GetComponent<BasicMap>().WorldRelativeScale
                    ).ToString();

                    StartCoroutine(PostTestMarkerPositions(longLat));*/
                    
                    /*toSpawn = P_MarkerCurrency;

                    string[] parts = longLat.Split(',');
                    float longitude = float.Parse(parts[0]);
                    float latitude = float.Parse(parts[1]);
                    Vector3 fromLongLat = VectorExtensions.AsUnityPosition(new Vector2(latitude, longitude), Map.GetComponent<BasicMap>().CenterMercator, Map.GetComponent<BasicMap>().WorldRelativeScale);

                    marker = Instantiate(toSpawn, fromLongLat, Quaternion.identity);
                    //GameObject marker = Instantiate(toSpawn, player.transform.position + (dirs[i].normalized * Random.Range(4.0f * scaleConversion, 12f * scaleConversion)), Quaternion.identity);

                    markerRefs[i] = marker;

                    marker.transform.SetParent(augotchiMap.transform);
                    Instantiate(P_MarkerPoof, marker.transform.position, Quaternion.identity);

                    marker.transform.localScale = new Vector3(1, 1, 1);

                    marker.GetComponent<Marker>().gc = this;*/


                }
            }
        }
        else
        {
            for (int i = 0; i < dirs.Length; i++)
            {
                GameObject map = GameObject.FindGameObjectWithTag("Map");
                float scaleConversion = map.transform.localScale.x;

                markerRelativeDistances[i] = (markerRefs[i].transform.position - player.transform.position).magnitude / scaleConversion;
            }
        }

        rewardTimer += Time.deltaTime;
        if(rewardStack.Count > 0 && rewardTimer > 2f)
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

        if (PetKeeper.pet.happiness < 25)
        {
            for (int i = 0; i < 8; i++)
            {
                int rnd = UnityEngine.Random.Range(0, 1000);
                if (rnd < 350)
                {
                    newMarkers[i] = Marker.MarkerType.CURRENCY;
                }
                else
                {
                    newMarkers[i] = Marker.MarkerType.FOOD;
                }
            }
        }
        else if(PetKeeper.pet.happiness < 75)
        {
            for (int i = 0; i < 8; i++)
            {
                int rnd = UnityEngine.Random.Range(0, 1000);
                if (rnd < 450)
                {
                    newMarkers[i] = Marker.MarkerType.CURRENCY;
                }
                else if (rnd < 925)
                {
                    newMarkers[i] = Marker.MarkerType.FOOD;
                }
                else
                {
                    newMarkers[i] = Marker.MarkerType.CRATE;
                }
            }
        }
        else
        {
            for (int i = 0; i < 8; i++)
            {
                int rnd = UnityEngine.Random.Range(0, 1000);
                if (rnd < 450)
                {
                    newMarkers[i] = Marker.MarkerType.CURRENCY;
                }
                else if (rnd < 850)
                {
                    newMarkers[i] = Marker.MarkerType.FOOD;
                }
                else
                {
                    newMarkers[i] = Marker.MarkerType.CRATE;
                }
            }
        }
        
        PetKeeper.pet.setMarkers(newMarkers);
    }

    public void startPlantingSeed(Seed seedInfo)
    {
        isPlantingSeed = true;
        seedToPlant = seedInfo;
    }

    public void tryPlantSeed(Vector3 plantPos)
    {
        if (!isPlantingSeed)
            return;

        GameObject Map = GameObject.FindGameObjectWithTag("Map");

        Vector3 plantPosLocal = plantPos / Map.transform.localScale.x;

        RaycastHit hit;
        int layerMask = 1 << LayerMask.NameToLayer("House") | 1 << LayerMask.NameToLayer("FarmPlot");
        if(Physics.SphereCast(plantPosLocal + (Vector3.up * 50), 6.5f * Map.transform.localScale.x, Vector3.down, out hit, 1000, layerMask))
        {
            queueRewardText("Something is blocking!", new Color(0.8f, 0.5f, 0.5f));
            return;
        }

        queueRewardText("Planted seed!", new Color(0.8f, 0.5f, 0.5f));

        GameObject farmPlot = Instantiate(P_FarmPlot, plantPosLocal, Quaternion.identity);
        farmPlot.transform.SetParent(Map.transform, false);
        farmPlot.transform.localScale = Vector3.one;

        string longLat = VectorExtensions.GetGeoPosition(
            farmPlot.transform.position,
            Map.GetComponent<BasicMap>().CenterMercator,
            Map.GetComponent<BasicMap>().WorldRelativeScale
        ).ToString();

        GardenCrop newCrop = new GardenCrop(seedToPlant.seedType, DateTime.Now.Ticks, longLat);
        PetKeeper.pet.Base.gardenCrops.Add(newCrop);

        isPlantingSeed = false;
        seedToPlant = null;

        PetKeeper.pet.Save(false);
    }

    public void moveHouse()
    {
        GameObject Map = GameObject.FindGameObjectWithTag("Map");
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        GameObject baseObject = GameObject.FindGameObjectWithTag("Base");

        Destroy(baseObject);

        string longLat = VectorExtensions.GetGeoPosition(
            player.transform,
            Map.GetComponent<BasicMap>().CenterMercator,
            Map.GetComponent<BasicMap>().WorldRelativeScale
        ).ToString();

        PetKeeper.pet.Base.longLat = longLat;

        PetKeeper.pet.Save(false);

        initBase();
    }

    // BACKEND!

    public GameObject cube;

    string InsertTestMarkerPositionURL = "http://farnorthentertainment.com/Augotchi_WorldPosTest.php?"; //be sure to add a ? to your url

    public IEnumerator PostTestMarkerPositions(string position)
    {
        string post_url = InsertTestMarkerPositionURL +
                           "player_id=" + SystemInfo.deviceUniqueIdentifier +
                           "&position=" + position
                           ;

        Debug.LogWarning(post_url);
        // Post the URL to the site and create a download object to get the result.
        WWW pg_post = new WWW(post_url);
        yield return pg_post; // Wait until the download is done

        if (pg_post.error != null)
        {
            Debug.LogWarning("There was an error posting the pet data: " + pg_post.error);
        }
    }

    string GetTestMarkerPositionURL = "http://farnorthentertainment.com/Augotchi_WorldPosTestGet.php?"; //be sure to add a ? to your url

    public IEnumerator GetTestMarkerPositions()
    {
        string post_url = GetTestMarkerPositionURL +
                           "player_id=" + SystemInfo.deviceUniqueIdentifier
                           ;
        Debug.LogWarning(post_url);

        // Post the URL to the site and create a download object to get the result.
        WWW pg_post = new WWW(post_url);
        yield return pg_post; // Wait until the download is done

        if (pg_post.error != null)
        {
            Debug.LogWarning("There was an error posting the pet data: " + pg_post.error);
        }
        else
        {
            Debug.LogWarning(pg_post.text);
            TestMarkerPosData testData = JsonUtility.FromJson<TestMarkerPosData>(pg_post.text);
            Debug.LogWarning(JsonUtility.ToJson(testData));
            Debug.LogWarning(testData.TestMarkerPositions[0].Position);
        }
    }
}
