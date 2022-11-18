using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookUI : MonoBehaviour
{
    [Header("C/D Buttons")]
    private bool actionBtn_Noel = false;
    public GameObject cleanBtn;
    public GameObject deleteBtn;
    public void ClickedNoel()
    {
        // clean, delete 버튼 ON/OFF
        actionBtn_Noel = !actionBtn_Noel;
        cleanBtn.SetActive(actionBtn_Noel);
        deleteBtn.SetActive(actionBtn_Noel);
        
    }

    public void PrintDialogNoel()
    {
        //CookManager.
    }

    [Header("Cooking Buttons")]
    private bool actionBtn_NPC = false;
    public GameObject boilingBtn, steamingBtn, boildownBtn, roastingBtn, fryingBtn, stirfryingBtn, mixingBtn;
    public void ClickedNPC()
    {
        // 요리 버튼 ON/OFF
        actionBtn_NPC = !actionBtn_NPC;
        boilingBtn.SetActive(actionBtn_NPC); steamingBtn.SetActive(actionBtn_NPC); boildownBtn.SetActive(actionBtn_NPC); 
        roastingBtn.SetActive(actionBtn_NPC); fryingBtn.SetActive(actionBtn_NPC); stirfryingBtn.SetActive(actionBtn_NPC);
        mixingBtn.SetActive(actionBtn_NPC);
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
