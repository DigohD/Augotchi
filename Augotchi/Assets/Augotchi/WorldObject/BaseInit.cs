using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseInit : MonoBehaviour {

    public AudioClip A_GardenGrow;
    public AudioClip A_HouseLand;

    public GameObject G_House;
    public GameObject G_Garden;

    float timer;
    float houseTimer;

    float initgardenScale;
    float initHouseY;

    bool houseBoucning;
    float houseVelY;

    void Start()
    {
        initgardenScale = G_Garden.transform.localScale.x;
        G_Garden.transform.localScale = Vector3.zero;

        GetComponent<AudioSource>().PlayOneShot(A_GardenGrow);

        initHouseY = G_House.transform.localPosition.y;
        G_House.transform.localPosition += Vector3.up * 300;
    }

    void Update () {
        timer += Time.deltaTime;

        float gardenTimer = timer * 2;

        if(gardenTimer < 0.5f)
        {
            float gardenScaleBase = 0;
            if (gardenTimer < 0.5f)
                gardenScaleBase = gardenTimer / 0.5f * initgardenScale;
            else
                gardenScaleBase = initgardenScale;
            float newScale = gardenScaleBase + ( Mathf.Sin(gardenTimer * Mathf.PI / 0.5f) * 75f);
            G_Garden.transform.localScale = Vector3.one * newScale;
        }else if(gardenTimer < 1f)
        {
            float newScale = initgardenScale + (Mathf.Sin((gardenTimer * Mathf.PI * 4f) + Mathf.PI) * 10.5f * (1f - (gardenTimer - 0.5f)));
            G_Garden.transform.localScale = Vector3.one * newScale;
        }
        else
        {
            G_Garden.transform.localScale = Vector3.one * initgardenScale;
        }

        if(timer > 0.25f)
        {
            if (!houseBoucning)
            {
                houseTimer = timer - 0.25f;

                houseVelY = -houseTimer * 40f;
                G_House.transform.localPosition += Vector3.up * houseVelY;

                if (G_House.transform.localPosition.y < initHouseY)
                {
                    G_House.transform.localPosition = new Vector3(
                        G_House.transform.localPosition.x,
                        initHouseY,
                        G_House.transform.localPosition.z
                    );

                    G_House.GetComponentInChildren<ParticleSystem>().Play();

                    GetComponent<AudioSource>().PlayOneShot(A_HouseLand);

                    houseVelY = -houseVelY * 0.1f;
                    houseBoucning = true;
                    houseTimer = timer;
                }
            }
            else
            {
                float bounceTimer = timer - houseTimer;

                houseVelY -= 20f * Time.deltaTime;

                G_House.transform.localPosition = new Vector3(
                    G_House.transform.localPosition.x,
                    G_House.transform.localPosition.y + houseVelY,
                    G_House.transform.localPosition.z
                );

                if(G_House.transform.localPosition.y < initHouseY)
                {
                    G_House.transform.localPosition = new Vector3(
                        G_House.transform.localPosition.x,
                        initHouseY,
                        G_House.transform.localPosition.z
                    );

                    houseVelY = -houseVelY * 0.5f;

                    if (houseVelY < 1)
                    {
                        G_House.transform.localPosition = new Vector3(
                            G_House.transform.localPosition.x,
                            initHouseY,
                            G_House.transform.localPosition.z
                        );

                        Destroy(this);
                    }
                }
            }
        }
	}
}
