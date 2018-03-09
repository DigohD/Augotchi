using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pedestal : MonoBehaviour {

    private Vector3 previousMousePos;

    private float timerSinceLastTick;

	void Update () {
        timerSinceLastTick += Time.deltaTime;
        if (!Input.GetMouseButton(0))
            return;

        float top = Screen.height * 0.35f;
        float bottom = Screen.height - (Screen.height * 0.15f);

        Vector3 newMousePos = Input.mousePosition;

        if (timerSinceLastTick > 0.2f)
        {
            previousMousePos = newMousePos;
            timerSinceLastTick = 0;
            return;
        }

        
        Vector3 diff = newMousePos - previousMousePos;
        previousMousePos = newMousePos;

        transform.RotateAroundLocal(Vector3.up, -diff.x / Screen.currentResolution.width * 10);

        timerSinceLastTick = 0;
    }
}
