using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;

public class MuteSoundButton : MonoBehaviour {

    private static bool soundIsOn = true;

    public Sprite onSprite, offSprite;

    public void Start()
    {
        BinaryFormatter bf = new BinaryFormatter();

        if (File.Exists(Application.persistentDataPath + "/AugotchiSettings.gd"))
        {
            FileStream file = File.Open(Application.persistentDataPath + "/AugotchiSettings.gd", FileMode.Open);
            soundIsOn = (bool) bf.Deserialize(file);
            file.Close();
        }
        else
        {
            FileStream file = File.Create(Application.persistentDataPath + "/AugotchiSettings.gd");
            bf.Serialize(file, soundIsOn);
            file.Close();
        }

        if (soundIsOn)
        {
            AudioListener.volume = 1;
            GetComponent<Image>().sprite = onSprite;
        }
        else
        {
            AudioListener.volume = 0;
            GetComponent<Image>().sprite = offSprite;
        }
    }

    public void soundToggle()
    {
        if (!soundIsOn)
        {
            soundIsOn = true;
            AudioListener.volume = 1;
            GetComponent<Image>().sprite = onSprite;
        }
        else
        {
            soundIsOn = false;
            AudioListener.volume = 0;
            GetComponent<Image>().sprite = offSprite;
        }

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/AugotchiSettings.gd");
        bf.Serialize(file, soundIsOn);

        file.Close();
    }
}
