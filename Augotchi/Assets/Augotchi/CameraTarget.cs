using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTarget : MonoBehaviour {

    Vector3 targetPos;

    GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        transform.GetChild(0).GetComponent<Camera>().enabled = false;
        transform.GetChild(0).GetComponent<Camera>().enabled = true;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(0, 90 * Time.deltaTime, 0);
        }

        targetPos = player.transform.position;

        if (!GameControl.isZooming)
            transform.position = Vector3.Lerp(transform.position, targetPos, 1f * Time.deltaTime);
        else
            transform.position = player.transform.position;
    }
}
