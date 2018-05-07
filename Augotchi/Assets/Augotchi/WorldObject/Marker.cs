using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Marker : MonoBehaviour {

    public AudioClip A_MarkerAppear;
    public AudioClip A_OnPicked;

    public GameObject P_MarkerPoof;

    public GameObject G_Visuals;
    protected float visualInitScale;

    public GameControl gc;

    public enum MarkerType { GRASS, CRATE }

    public bool isIndividual = false;

    GameObject player;

    bool isRevealing;
    float revealTimer;

    public bool isAnimated;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        visualInitScale = G_Visuals.transform.localScale.x;

        transform.localRotation = Quaternion.Euler(0, Random.Range(0f, 360f), 0);
    }

    public virtual void picked()
    {
        bool rangeHit;
        int layerMask = 1 << LayerMask.NameToLayer("RangeCircle");
        rangeHit = Physics.Raycast(transform.position + new Vector3(0, 5, 0), Vector3.down, 100, layerMask);

        if (rangeHit)
        {
            if(!isIndividual)
                GameControl.markerPicked = true;

            Instantiate(P_MarkerPoof, transform.position, Quaternion.identity);

            PetKeeper.pet.onMarkerPicked();
            executeEffect();

            if (A_OnPicked != null)
                GameControl.playPostMortemAudioClip(A_OnPicked);

            try
            {
                gc.respawnMarker(this);
            }catch(System.Exception e)
            {

                Debug.LogError("Marker not have gamecontrol ref. " + name);
                return;
            }
            

            if (isIndividual)
                Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        if (!player)
            player = GameObject.FindGameObjectWithTag("Player");

        if(!isRevealing && (transform.position - player.transform.position).magnitude < 45)
        {
            Instantiate(P_MarkerPoof, transform.position, Quaternion.identity);

            G_Visuals.transform.localScale = Vector3.zero;
            G_Visuals.SetActive(true);

            isRevealing = true;

            GetComponent<AudioSource>().PlayOneShot(A_MarkerAppear);
        }else if(!isRevealing && (transform.position - player.transform.position).magnitude > 125)
        {
            gc.respawnMarker(this);

            Destroy(gameObject);
        }
        else if (isRevealing && (transform.position - player.transform.position).magnitude > 200)
        {
            gc.respawnMarker(this);

            Destroy(gameObject);
        }
        else if (isRevealing)
        {
            revealTimer += Time.deltaTime;
            if (revealTimer < 0.5f)
            {
                float gardenScaleBase = 0;
                if (revealTimer < 0.5f)
                    gardenScaleBase = revealTimer / 0.5f * visualInitScale;
                else
                    gardenScaleBase = visualInitScale;
                float scaleSin = isAnimated ? 0.1f : 75f;
                float newScale = gardenScaleBase + (Mathf.Sin(revealTimer * Mathf.PI / 0.5f) * scaleSin);
                G_Visuals.transform.localScale = Vector3.one * newScale;
            }
            else if (revealTimer < 1f)
            {
                float scaleSin = isAnimated ? 0.02f : 10.5f;
                float newScale = visualInitScale + (Mathf.Sin((revealTimer * Mathf.PI * 4f) + Mathf.PI) * scaleSin * (1f - (revealTimer - 0.5f)));
                G_Visuals.transform.localScale = Vector3.one * newScale;
            }
            else
            {
                G_Visuals.transform.localScale = Vector3.one * visualInitScale;
            }
        }
    }

    protected abstract void executeEffect();
}
