using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Unity.Editor;
using System.Runtime.Serialization.Formatters.Binary;
using Firebase.Database;
using System.Net.NetworkInformation;

public class JsonPet{
    public int baseTextureIndex;
    public int baseTint;

    public JsonPet(){}

    public JsonPet(int baseTextureIndex, int baseTint){
        this.baseTextureIndex = baseTextureIndex;
        this.baseTint = baseTint;
    }
}

public class TestPetToDatabase : MonoBehaviour {

    DatabaseReference reference;

    // Use this for initialization
    void Start () {
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://augotchi.firebaseio.com/");

        reference = FirebaseDatabase.DefaultInstance.RootReference;
        Debug.Log("Unique ID");
        Debug.Log(SystemInfo.deviceUniqueIdentifier);
        writeNewPet(SystemInfo.deviceUniqueIdentifier, 1337, 9001);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void writeNewPet(string userId, int baseTextureIndex, int baseTint){
        JsonPet user = new JsonPet(baseTextureIndex, baseTint);
        string json = JsonUtility.ToJson(user);

        reference.Child("petData").Child(userId).SetRawJsonValueAsync(json);
    }

    static string GetMacAddress()
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


