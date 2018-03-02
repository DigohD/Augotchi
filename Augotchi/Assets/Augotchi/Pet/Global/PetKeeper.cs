using PedometerU;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetKeeper : MonoBehaviour {

    public static PetGlobal pet;

    Pedometer pedometer;

    float timer = 0;

    void Start () {
        if (GameObject.FindGameObjectsWithTag("PetKeeper").Length > 1)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        pet = new PetGlobal();
        pet.Load();

        Debug.LogWarning("Hunger: " + pet.hunger);
        Debug.LogWarning("Happiness: " + pet.happiness);
        Debug.LogWarning("HEalth: " + pet.health);

        pedometer = new Pedometer(this.OnStep);
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 10)
        {
            timer = 0;
            pet.degenerateTick();
        }
    }

    void OnStep(int steps, double distance)
    {
        // Display the values
        PlayerScript.points = steps;
        // Display distance in feet
        //distanceText.text = (distance * 3.28084).ToString("F2") + " ft";
    }

}
