﻿using Mapbox.Unity.Map;
using Mapbox.Unity.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControl : MonoBehaviour {

    private bool isInitialized = false;

    public static bool playPostMortemSound;
    public static AudioClip A_postMortemSound;

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

    public GameObject P_MarkerGrass;
    public GameObject P_MarkerCrate;
    public GameObject P_MarkerRoot;

    public GameObject P_RewardText;
    public GameObject P_MarkerPoof;

    public GameObject P_Base;
    public GameObject P_FarmPlot;

    public GameObject[] P_FarmPlots;

    public GameObject G_UIButtons;
    public GameObject G_CloseButton;
    public GameObject G_ModeHint;

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

    public GameObject buildBaseButton;

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

	void Awake () {
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
            buildBaseButton.SetActive(true);
            return;
        }

        buildBaseButton.SetActive(false);

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

            GameObject newPlot = (GameObject) Instantiate(P_FarmPlots[(int) crop.seedType], fromLongLat + (Vector3.up * 0.2f), Quaternion.identity);
            newPlot.transform.SetParent(Map.transform, false);
            newPlot.transform.localScale = Vector3.one;

            newPlot.GetComponent<FarmPlot>().init(Inventory.getSeedTypeInfo(crop.seedType), crop, this, false);
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
        //StartCoroutine( GetTestMarkerPositions());
    }

    private void FixedUpdate()
    {
        if (playPostMortemSound)
        {
            playPostMortemSound = false;
            GetComponent<AudioSource>().PlayOneShot(A_postMortemSound);
        }

        /*if (markerRefs[0] == null)
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
        }*/
    }

    float loadTimer = 0;
    void Update () {
        loadTimer += Time.deltaTime;
        if (!isInitialized && loadTimer > 2.5f)
        {
            initBase();
            isInitialized = true;
            loadingScreen.SetActive(false);
            generateMarkerRing();
        }
        else if (loadTimer < 2.5f)
            return;



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

        rewardTimer += Time.deltaTime;
        if(rewardStack.Count > 0 && rewardTimer > 0.6f)
        {
            rewardTimer = 0;
            spawnRewardText((Reward) rewardStack[0]);
            rewardStack.RemoveAt(0);
        }
	}

    private void generateMarkerRing()
    {
        for (int i = 0; i < dirs.Length; i++){
            float distance = UnityEngine.Random.Range(85f * 1, 125f * 1);
            GameObject marker = Instantiate(P_MarkerGrass, player.transform.position + (dirs[i].normalized * distance), Quaternion.identity);
            marker.GetComponent<Marker>().gc = this;
        }

        Debug.LogWarning(PetKeeper.pet.inventory);
        Debug.LogWarning(PetKeeper.pet.inventory.uniqueCounts);
        if (PetKeeper.pet.inventory.uniqueCounts[(int) Inventory.UniqueType.WRONGWORLD_ROOTS] < 1)
            generateSpecificMarker(P_MarkerRoot);
        else
            generateRandomMarker();

        for (int i = 0; i < 15; i++)
        {
            generateRandomMarker();
        }
    }

    private void generateRandomMarker()
    {
        Vector3 dir = new Vector3(UnityEngine.Random.Range(-1.0f, 1.0f), 0, UnityEngine.Random.Range(-1.0f, 1.0f)).normalized;
        float distance = UnityEngine.Random.Range(75f * 1, 165f * 1);

        GameObject toSpawn = null;
        switch (UnityEngine.Random.Range(0, 2))
        {
            case 0:
                toSpawn = P_MarkerGrass;
                break;
            case 1:
                toSpawn = P_MarkerCrate;
                break;
        }

        GameObject marker = Instantiate(toSpawn, player.transform.position + (dir * distance), Quaternion.identity);
        marker.GetComponent<Marker>().gc = this;
    }

    private void generateSpecificMarker(GameObject toSpawn)
    {
        Vector3 dir = new Vector3(UnityEngine.Random.Range(-1.0f, 1.0f), 0, UnityEngine.Random.Range(-1.0f, 1.0f)).normalized;
        float distance = UnityEngine.Random.Range(75f * 1, 165f * 1);

        GameObject newMarker = Instantiate(toSpawn, player.transform.position + (dir * distance), Quaternion.identity);
        newMarker.GetComponent<Marker>().gc = this;
    }

    public void respawnMarker(Marker marker)
    {
        if(marker is MarkerRoot && PetKeeper.pet.inventory.uniqueCounts[(int) Inventory.UniqueType.WRONGWORLD_ROOTS] < 1)
        {
            generateSpecificMarker(P_MarkerRoot);
            return;
        }

        generateRandomMarker();
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

    public void startPlantingSeed(Seed seedInfo)
    {
        isPlantingSeed = true;
        seedToPlant = seedInfo;

        G_UIButtons.SetActive(false);
        G_CloseButton.SetActive(true);
        G_ModeHint.SetActive(true);

        G_ModeHint.GetComponentInChildren<Text>().text = "Plant Seed!";
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

        GameObject farmPlot = Instantiate(P_FarmPlots[(int) seedToPlant.seedType], plantPosLocal + (Vector3.up * 0.2f), Quaternion.identity);
        farmPlot.transform.SetParent(Map.transform, false);
        farmPlot.transform.localScale = Vector3.one;
        
        string longLat = VectorExtensions.GetGeoPosition(
            farmPlot.transform.position,
            Map.GetComponent<BasicMap>().CenterMercator,
            Map.GetComponent<BasicMap>().WorldRelativeScale
        ).ToString();

        GardenCrop newCrop = new GardenCrop(seedToPlant.seedType, DateTime.Now.Ticks, longLat);
        PetKeeper.pet.Base.gardenCrops.Add(newCrop);

        farmPlot.GetComponent<FarmPlot>().init(seedToPlant, newCrop, this, true);

        PetKeeper.pet.inventory.seedCounts[(int) seedToPlant.seedType] -= 1;

        InventoryUI.reRender = true;

        PetKeeper.pet.Save(false);

        exitModePressed();
    }

    public void moveHouse()
    {
        GameObject Map = GameObject.FindGameObjectWithTag("Map");
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        GameObject baseObject = GameObject.FindGameObjectWithTag("Base");

        foreach (GameObject fp in GameObject.FindGameObjectsWithTag("FarmPlot"))
            Destroy(fp);

        Destroy(baseObject);

        PetKeeper.pet.Base.gardenCrops.Clear();

        string longLat = VectorExtensions.GetGeoPosition(
            player.transform,
            Map.GetComponent<BasicMap>().CenterMercator,
            Map.GetComponent<BasicMap>().WorldRelativeScale
        ).ToString();

        PetKeeper.pet.Base.longLat = longLat;

        PetKeeper.pet.Save(false);

        initBase();
    }

    public void exitModePressed()
    {
        seedToPlant = null;
        isPlantingSeed = false;

        G_UIButtons.SetActive(true);
        G_CloseButton.SetActive(false);
        G_ModeHint.SetActive(false);
    }

    public static void playPostMortemAudioClip(AudioClip clip)
    {
        playPostMortemSound = true;
        A_postMortemSound = clip;
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
