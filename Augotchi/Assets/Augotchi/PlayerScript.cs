using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    public static int points = 0;

    GameObject playerTarget;

    public Camera camera;



    void Start () {
        playerTarget = GameObject.FindGameObjectWithTag("PlayerTarget");
	}
	
	void Update () {
        transform.position = Vector3.Lerp(transform.position, playerTarget.transform.position, 1f * Time.deltaTime);
        transform.rotation = playerTarget.transform.rotation;

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
