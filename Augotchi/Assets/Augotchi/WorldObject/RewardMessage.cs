using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardMessage : MonoBehaviour {

    float opacity = 1;

    void Start()
    {
        transform.Translate(0, 5, 0);

        transform.SetParent(GameObject.FindGameObjectWithTag("MainCamera").transform);
    }

    void Update()
    {
        transform.Translate(0, 4 * Time.deltaTime, 0);

        GetComponent<TextMesh>().color = new Color(0.15f, 0.15f, 0.15f, opacity);
        transform.GetChild(0).GetComponent<TextMesh>().color = new Color(1f, 1f, 1f, opacity);

        opacity -= (0.3f * Time.deltaTime);

        if (opacity < 0)
            Destroy(gameObject);
    }

    public void setMessage(string message)
    {
        GetComponent<TextMesh>().text = message;
        transform.GetChild(0).GetComponent<TextMesh>().text = message;
    }
}
