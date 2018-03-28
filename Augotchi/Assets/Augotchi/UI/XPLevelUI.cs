using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XPLevelUI : MonoBehaviour {

    public Image xpBar;
    public Text xpText;
    public Text levelText;

	void FixedUpdate () {
        xpBar.fillAmount = PetKeeper.pet.getXpRatio();
        int xpToNextLevel = (int) (PetKeeper.pet.level * PetKeeper.pet.level / 0.05f);
        xpText.text = PetKeeper.pet.xp + "/" + PetKeeper.pet.xpToNextLevel(PetKeeper.pet.level);
        xpText.transform.GetChild(0).GetComponent<Text>().text = PetKeeper.pet.xp + "/" + PetKeeper.pet.xpToNextLevel(PetKeeper.pet.level);
        levelText.text = "" + PetKeeper.pet.level;
	}
}
