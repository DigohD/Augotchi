using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinusWave : MonoBehaviour {

    public float amplitude;
    public float frequency;
    public float offset;

    public float timer;

    private Vector3 origin;

    private void Start()
    {
        origin = transform.localPosition;
    }

    void Update () {
        timer += Time.deltaTime;
        transform.localPosition = origin + amplitude * (Vector3.forward * Mathf.Sin((frequency * timer * 6.28f) + (offset * 6.28f)));
	}
}
