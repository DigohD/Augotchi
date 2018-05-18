using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerScript : NetworkBehaviour {

    public Animator anim;

    public static int steps = 0;

    GameObject playerTarget;

    public Camera camera;

    public SkinnedMeshRenderer ren;
    public Texture2D baseTex;
    public Texture2D overlay;

    void Start () {
        playerTarget = GameObject.FindGameObjectWithTag("PlayerTarget");
    }
	
	void Update () {
        Vector3 prePos = transform.position;
        transform.position = Vector3.Lerp(transform.position, playerTarget.transform.position, 1f * Time.deltaTime);
        Vector3 diff = transform.position - prePos;
        float dist = diff.magnitude;

        if(dist > 0.3333f)
        {
            anim.speed = 4;
        }
        else
        {
            anim.speed = (dist * 3 * 3) + 1;
        }

        if(dist > 0.25f * Time.deltaTime)
        {
            anim.SetInteger("State", 1);
        }
        else
        {
            anim.SetInteger("State", 0);
        }

        if(!diff.normalized.Equals(Vector3.zero))
            anim.transform.forward = diff.normalized;

        Ray ray = camera.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Transform objectHit = hit.transform;

                if(objectHit.tag.Equals("Marker"))
                {
                    objectHit.GetComponent<Marker>().picked();
                    return;
                }
            }

            int layerMask;
            if (GameObject.FindGameObjectWithTag("GameController").GetComponent<GameControl>().isRemovingGardenDecor)
            {
                layerMask = 1 << LayerMask.NameToLayer("DecorRemove");

                if (Physics.Raycast(ray, out hit, 1000f, layerMask))
                {
                    Transform objectHit = hit.transform;

                    Debug.LogWarning(objectHit.name);

                    objectHit.GetComponentInParent<GardenDecorWorld>().remove();
                }

                return;
            }

            layerMask = 1 << LayerMask.NameToLayer("FarmPlotInteract");

            if (Physics.Raycast(ray, out hit, 1000f, layerMask))
            {
                Transform objectHit = hit.transform;

                Debug.LogWarning(objectHit.name);

                objectHit.GetComponentInParent<FarmPlot>().onPress();
            }

            layerMask = 1 << LayerMask.NameToLayer("PlantRange");

            if (Physics.Raycast(ray, out hit, 1000f, layerMask))
            {
                Transform objectHit = hit.transform;
                Vector3 hitPoint = hit.point;

                bool rangeHit;
                layerMask = 1 << LayerMask.NameToLayer("GardenCircle");
                rangeHit = Physics.Raycast(hitPoint + new Vector3(0, 5, 0), Vector3.down, 100, layerMask);

                if (rangeHit)
                {
                    GameObject.FindGameObjectWithTag("GameController").GetComponent<GameControl>().tryPlantSeed(hitPoint);
                }

                layerMask = 1 << LayerMask.NameToLayer("GardenDecorCircle");
                rangeHit = Physics.Raycast(hitPoint + new Vector3(0, 5, 0), Vector3.down, 250, layerMask);

                if (rangeHit)
                {
                    GameObject.FindGameObjectWithTag("GameController").GetComponent<GameControl>().tryBuildgardenDecor(hitPoint);
                }
            }
        }
    }
}
