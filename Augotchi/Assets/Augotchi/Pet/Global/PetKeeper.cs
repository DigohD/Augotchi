using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetKeeper : MonoBehaviour {

    public static PetGlobal pet;

    float timer = 0;

    void Start () {
        pet = new PetGlobal();
        pet.Load();

        Debug.LogWarning("Hunger: " + pet.hunger);
        Debug.LogWarning("Happiness: " + pet.happiness);
        Debug.LogWarning("HEalth: " + pet.health);
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

}
