using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class MarkerPopup : Marker {

    public GameObject P_Popup;

    protected GameObject ConnectedPopup;

    public override void picked()
    {
        bool rangeHit;
        int layerMask = 1 << LayerMask.NameToLayer("RangeCircle");
        rangeHit = Physics.Raycast(transform.position + new Vector3(0, 5, 0), Vector3.down, 100, layerMask);

        if (rangeHit)
        {
            GameObject newPopup = Instantiate(P_Popup, P_Popup.transform.position, P_Popup.transform.rotation);
            newPopup.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, true);
            newPopup.transform.localScale = P_Popup.transform.localScale;
            newPopup.transform.localPosition = P_Popup.transform.localPosition;

            ConnectedPopup = newPopup;

            initPopup(newPopup);
            int i = 0;
            foreach(Button b in newPopup.GetComponentsInChildren<Button>())
            {
                Debug.LogWarning("Init button " + i);
                

                switch (i)
                {
                    case 0:
                        b.onClick.AddListener(delegate { PopupChoiceMade("" + 0); });
                        break;
                    case 1:
                        b.onClick.AddListener(delegate { PopupChoiceMade("" + 1); });
                        break;
                }

                i++;
            }
        }
    }

    protected abstract void PopupChoiceMade(string m);

    protected abstract void initPopup(GameObject popup);

}
