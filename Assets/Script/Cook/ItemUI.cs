using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
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
        CookDataManager.Instance.SendFlavorData(int.Parse(Regex.Replace(item.imgId, @"\D", "")));
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
