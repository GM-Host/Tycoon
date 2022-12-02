using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookUI : MonoBehaviour
{
    /********************
         # 3 -> # 7
    ********************/
    [Header("Cooking Buttons")]
    private bool actionBtn_NPC = false;
    public GameObject boilingBtn, steamingBtn, boildownBtn, roastingBtn, fryingBtn, stirfryingBtn, mixingBtn, inventory, flavorTxt;
    public void ClickedNPC()
    {
        // 요리 버튼 ON/OFF
        actionBtn_NPC = !actionBtn_NPC;
        boilingBtn.SetActive(actionBtn_NPC); steamingBtn.SetActive(actionBtn_NPC); boildownBtn.SetActive(actionBtn_NPC); 
        roastingBtn.SetActive(actionBtn_NPC); fryingBtn.SetActive(actionBtn_NPC); stirfryingBtn.SetActive(actionBtn_NPC);
        mixingBtn.SetActive(actionBtn_NPC); inventory.SetActive(actionBtn_NPC); flavorTxt.SetActive(actionBtn_NPC);
        
    }

    /********************
         # 4 -> # 6
    ********************/
    [Header("History")]
    public GameObject history;
    public void cleanHistory()
    {
        Transform[] childList = history.GetComponentsInChildren<Transform>();
        if(childList != null)
        {
            foreach(RectTransform child in childList)
            {
                if(child != history.transform)
                    Destroy(child.gameObject);
            }
        }
    }

    public void deleteHistory(string lastId)
    {
        Transform lastChild = history.transform.Find(lastId);
        Destroy(lastChild.gameObject);
    }

    
    public GameObject dialog;
}
