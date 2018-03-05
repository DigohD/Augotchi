using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarkerPark : MarkerPopup {

    private enum ParkType { STICK, RELAX, PICNIC, GATHER }

    private ParkType typeOne;
    private ParkType typeTwo;

    private ParkType typeToExecute;

    protected override void initPopup(GameObject popup)
    {
        int firstType = Random.Range(0, 4);

        if (PetKeeper.pet.currency < 75)
            firstType = 0;

        switch (firstType)
        {
            case 0:
                typeOne = ParkType.GATHER;
                break;
            case 1:
                typeOne = ParkType.PICNIC;
                break;
            case 2:
                typeOne = ParkType.RELAX;
                break;
            case 3:
                typeOne = ParkType.STICK;
                break;
        }

        int secondType = -1;
        do
        {
            secondType = Random.Range(0, 4);
            switch (secondType)
            {
                case 0:
                    typeTwo = ParkType.GATHER;
                    break;
                case 1:
                    typeTwo = ParkType.PICNIC;
                    break;
                case 2:
                    typeTwo = ParkType.RELAX;
                    break;
                case 3:
                    typeTwo = ParkType.STICK;
                    break;
            }
        } while (firstType == secondType);

        Button[] buttons = popup.GetComponentsInChildren<Button>();
        int i = 0;
        foreach(Button b in buttons)
        {
            if(i == 0)
            {
                b.transform.GetChild(0).GetComponent<Text>().text = typeToString(typeOne);
                b.transform.GetChild(2).GetComponent<Text>().text = "x " + typeToPrice(typeOne);
            }
            else
            {
                b.transform.GetChild(0).GetComponent<Text>().text = typeToString(typeTwo);
                b.transform.GetChild(2).GetComponent<Text>().text = "x " + typeToPrice(typeTwo);
            }
            i++;
        }
    }

    protected override void PopupChoiceMade(string m)
    {
        int buttonID = int.Parse(m);
        Debug.Log("Choice: " + buttonID);
        if(buttonID == 0)
        {
            if (PetKeeper.pet.currency > typeToPrice(typeOne))
            {
                PetKeeper.pet.takeCurrency(typeToPrice(typeOne));
                typeToExecute = typeOne;
                executeEffect();
            }
        }
        else
        {
            if (PetKeeper.pet.currency > typeToPrice(typeTwo))
            {
                PetKeeper.pet.takeCurrency(typeToPrice(typeTwo));
                typeToExecute = typeTwo;
                executeEffect();
            }
        }
    }

    protected override void executeEffect()
    {
        GameControl.markerPicked = true;

        Destroy(ConnectedPopup);

        int amount = 0;
        switch (typeToExecute)
        {
            case ParkType.GATHER:
                amount = Random.Range(10, 15) * 10;
                PetKeeper.pet.giveCurrency(amount);
                gc.spawnRewardText("Coins: +" + amount);
                break;
            case ParkType.PICNIC:
                amount = Random.Range(15, 30);
                PetKeeper.pet.addHunger(amount);
                gc.spawnRewardText("Pet Fullness: +" + amount);
                break;
            case ParkType.RELAX:
                amount = Random.Range(15, 30);
                PetKeeper.pet.addHappiness(amount);
                gc.spawnRewardText("Pet Happiness: +" + amount);
                break;
            case ParkType.STICK:
                amount = Random.Range(15, 30);
                PetKeeper.pet.addHealth(amount);
                gc.spawnRewardText("Pet Health: +" + amount);
                break;
        }
    }

    private static string typeToString(ParkType pt)
    {
        switch (pt)
        {
            case ParkType.GATHER:
                return "Search Park";
            case ParkType.STICK:
                return "Apport";
            case ParkType.RELAX:
                return "Relax";
            case ParkType.PICNIC:
                return "Have Picnic";
        }
        return "ERROR";
    }

    private static int typeToPrice(ParkType pt)
    {
        switch (pt)
        {
            case ParkType.GATHER:
                return 0;
            case ParkType.STICK:
                return 75;
            case ParkType.RELAX:
                return 75;
            case ParkType.PICNIC:
                return 75;
        }
        return 9999;
    }
}
