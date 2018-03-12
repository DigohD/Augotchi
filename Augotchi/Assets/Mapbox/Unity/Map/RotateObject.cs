using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour {

    public float speed;

	void Update () {
        transform.RotateAroundLocal(Vector3.up, speed * Time.deltaTime);
	}
}
