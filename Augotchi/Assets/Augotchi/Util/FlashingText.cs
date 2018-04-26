using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashingText : MonoBehaviour {

    public Text t;

    float timer;
	void FixedUpdate () {
        timer += Time.fixedDeltaTime;

        t.color = new Color(t.color.r, t.color.g, t.color.b, (Mathf.Sin(timer * 4) * 0.5f) + 0.5f);
	}
}
