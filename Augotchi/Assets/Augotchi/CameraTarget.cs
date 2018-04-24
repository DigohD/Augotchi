using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTarget : MonoBehaviour {

    Vector3 targetPos;

    GameObject player;

    float zoomAmount = 1f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        transform.GetChild(0).GetComponent<Camera>().enabled = false;
        transform.GetChild(0).GetComponent<Camera>().enabled = true;

        transform.rotation = GameControl.rotation;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(0, 90 * Time.deltaTime, 0);
            GameControl.rotation = transform.rotation;
        }

        targetPos = player.transform.position;

        if (!GameControl.isZooming)
            transform.position = Vector3.Lerp(transform.position, targetPos, 1f * Time.deltaTime);
        else
            transform.position = player.transform.position;

        // If there are two touches on the device...
        if (Input.touchCount == 2)
        {
            // Store both touches.
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            // Find the position in the previous frame of each touch.
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            // Find the magnitude of the vector (the distance) between the touches in each frame.
            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            // Find the difference in the distances between each frame.
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            zoomAmount += deltaMagnitudeDiff * Time.deltaTime / 5f;

            if(zoomAmount > 4)
            {
                zoomAmount = 4;
            }
            if(zoomAmount < 0.5f)
            {
                zoomAmount = 0.5f;
            }

            transform.localScale = Vector3.one * zoomAmount;
        }
    }



}
