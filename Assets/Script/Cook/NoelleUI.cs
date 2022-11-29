using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NoelleUI : MonoBehaviour
{
    /********************
         # 1 -> # 4
    ********************/
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

        
        PrintDialogNoel();
        
    }

    /********************
        # CDM -> # 2
    ********************/
    [Header("Noelle Text")]
    public TMP_Text noelleText;
    private void PrintDialogNoel()
    {
        noelleText.text = CookDataManager.Instance.DialogNoelle();
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
