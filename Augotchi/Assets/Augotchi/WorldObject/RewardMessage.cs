using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardMessage : MonoBehaviour {

    float opacity = 1;

    void Start()
    {
        transform.position = transform.position + (Vector3.up * 5);

        transform.SetParent(GameObject.FindGameObjectWithTag("MainCamera").transform);
    }

    void Update()
    {
        transform.position = transform.position + (Vector3.up * 12 * Time.deltaTime);

        GetComponent<TextMesh>().color = new Color(0.15f, 0.15f, 0.15f, opacity);
        Color oldColor = transform.GetChild(0).GetComponent<TextMesh>().color;
        transform.GetChild(0).GetComponent<TextMesh>().color = new Color(oldColor.r, oldColor.g, oldColor.b, opacity);

        opacity -= (0.5f * Time.deltaTime);

        if (opacity < 0)
            Destroy(gameObject);
    }

    public void setMessage(Reward reward)
    {
        GetComponent<TextMesh>().text = reward.message;
        transform.GetChild(0).GetComponent<TextMesh>().text = reward.message;
        transform.GetChild(0).GetComponent<TextMesh>().color = reward.color;
    }
}
