using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerPoop : Marker {
    protected override void executeEffect()
    {
        int amount = Random.Range(1, 5) * 10;
        PetKeeper.pet.giveCurrency(amount);
        GameControl gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameControl>();
        gc.queueRewardText("Coins: +" + amount, new Color(1, 0.85f, 0.2f));
        if(Random.Range(0, 1000) == 0)
            PetKeeper.pet.unlockUniqueHat(14, 0);
        PetKeeper.pet.grantXP(50);
    }


    float startScale;
    private void Start()
    {
        startScale = transform.localScale.x;
    }

    float timer;
    void Update()
    {
        timer += Time.deltaTime;

        if(timer > 19.5f)
        {
            transform.localScale = transform.localScale - (Vector3.one * startScale * Time.deltaTime * 2);
        }

        if(timer > 20f)
        {
            Destroy(gameObject);
        }
    }
}
