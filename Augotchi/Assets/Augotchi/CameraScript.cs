using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    Vector3 offsetVector;

    Vector3 targetPos;

    GameObject player;

	void Start () {


        player = GameObject.FindGameObjectWithTag("Player");

        offsetVector = transform.position - player.transform.position;
	}
	
	void Update () {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            targetPos = Quaternion.Euler(0, -45 * Time.deltaTime, 0) * offsetVector;
        }

        targetPos = player.transform.position + offsetVector;

        transform.position = Vector3.Lerp(transform.position, targetPos, 1f * Time.deltaTime);
	}
}
