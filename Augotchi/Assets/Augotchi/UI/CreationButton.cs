using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreationButton : MonoBehaviour {

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            onClick();
        }
    }

    public void onClick()
    {
        GameState.mapZoom = GameObject.FindGameObjectWithTag("Map").transform.localScale.x;
        EditModeUI.isClothingMode = true;

        SceneManager.LoadScene("Creation");
    }
}
