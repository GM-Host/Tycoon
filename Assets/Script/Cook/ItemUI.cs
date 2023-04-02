using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUI : MonoBehaviour
{
    public CookDataManager.Inventory item;
    /********************
        # 8 -> # CDM
    ********************/
    public void ClickedItem()
    {
        CookDataManager.Instance.ItemSelected(item);
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
