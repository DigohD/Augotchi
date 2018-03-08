using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

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

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                Transform objectHit = hit.transform;

                if(objectHit.tag.Equals("Marker"))
                {
                    objectHit.GetComponent<Marker>().picked();
                }
            }
        }
    }
}
