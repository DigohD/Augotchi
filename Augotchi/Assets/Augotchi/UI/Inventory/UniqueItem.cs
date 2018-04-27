using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UniqueItem : MonoBehaviour {

    public Text nameText;
    public Image image;
    public Text countText;

    private Unique uniqueInfo;

    public void initUniqueItem(int count, Unique uniqueInfo)
    {
        nameText.text = uniqueInfo.name;
        countText.text = "x" + count;
        image.sprite = (Sprite)Resources.Load(uniqueInfo.imagePath, typeof(Sprite));

        this.uniqueInfo = uniqueInfo;
    }

    public void onClick()
    {
        
    }
}
