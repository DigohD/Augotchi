using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Unity.Editor;
using System.Runtime.Serialization.Formatters.Binary;
using Firebase.Database;
using System.Net.NetworkInformation;
using System;

public class TestPetToDatabase : MonoBehaviour {

    public static bool postData = false;

    void Update()
    {
        if (postData)
        {
            postData = false;
            StartCoroutine(PostPetGlobal());
        }
    }

    string UpdatePetGlobalDbURL = "http://farnorthentertainment.com/Augotchi_Petglobal.php?"; //be sure to add a ? to your url

    public IEnumerator PostPetGlobal ()
    {
        string post_url =  UpdatePetGlobalDbURL + 
                           "id=" + SystemInfo.deviceUniqueIdentifier + 
                           "&hunger=" + PetKeeper.pet.hunger +
                           "&happiness=" + PetKeeper.pet.happiness +
                           "&health=" + PetKeeper.pet.health +
                           "&markers_currency=" + PetKeeper.pet.markersCurrency +
                           "&markers_food=" + PetKeeper.pet.markersFood +
                           "&markers_crate=" + PetKeeper.pet.markersCrate +
                           "&markers_revive=" + PetKeeper.pet.markersRevive +
                           "&pet_death_count=" + PetKeeper.pet.petDeathCount +
                           "&petting_count=" + PetKeeper.pet.pettingCount +
                           "&feed_candy_count=" + PetKeeper.pet.candyFed +
                           "&feed_food_count=" + PetKeeper.pet.foodFed +
                           "&feed_vegetable_count=" + PetKeeper.pet.vegetableFed +
                           "&level=" + PetKeeper.pet.level +
                           "&start_app_count=" + PetKeeper.pet.startAppCount +
                           "&start_game_date=" + DateTime.Now.ToString("yyyy-MM-dd") +
                           "&step_counter=" + PetKeeper.pet.stepCounter +
                           "&active_ticks=" + PetKeeper.pet.activeTicks +
                           "&inactive_ticks=" + PetKeeper.pet.inactiveTicks +
                           "&pet_revival_count=" + PetKeeper.pet.petRevivalCount +
                           "&current_alive_ticks=" + PetKeeper.pet.currentAliveTicks +
                           "&longest_alive_ticks=" + PetKeeper.pet.longestAliveTicks;

        // Post the URL to the site and create a download object to get the result.
        WWW pg_post = new WWW(post_url);
        yield return pg_post; // Wait until the download is done

        if (pg_post.error != null)
        {
            Debug.LogWarning("There was an error posting the pet data: " + pg_post.error);
        }
    }

    public static string GetMacAddress()
    {
        string macAddresses = "";
        foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
        {
            if (nic.NetworkInterfaceType != NetworkInterfaceType.Ethernet) continue;
            if (nic.OperationalStatus == OperationalStatus.Up)
            {
                macAddresses += nic.GetPhysicalAddress().ToString();
                break;
            }
        }
        return macAddresses;
    }
}


