using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestUI : MonoBehaviour {

    public static bool reRender = true;

    public GameObject P_QuestItem;

    public Transform T_QuestContent;

    private void Start()
    {
        reRender = true;
    }

    void FixedUpdate()
    {
        if (reRender)
        {
            renderUI();
        }
    }

    private void renderUI()
    {
        reRender = false;

        renderQuestList();
    }

    private void renderQuestList()
    {
        foreach (Transform t in T_QuestContent)
            Destroy(t.gameObject);

        List<Quest> questLog = PetKeeper.pet.questLog;

        T_QuestContent.localPosition = Vector3.zero;
        ((RectTransform) T_QuestContent).sizeDelta = new Vector2(0, questLog.Count * 315f);

        int i = 0;
        foreach (Quest q in questLog)
        {
            GameObject newItem = Instantiate(P_QuestItem, Vector3.zero, Quaternion.identity);
            newItem.transform.SetParent(T_QuestContent, false);
            newItem.transform.localPosition = new Vector2(0, i * -315f);

            newItem.GetComponent<QuestItem>().initQuestItem(q);
            i++;
        }
    }
}
