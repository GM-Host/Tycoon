using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionManager : MonoBehaviour
{
    private Dictionary<string, int> inventoryDict = new Dictionary<string, int>();

    [SerializeField] private Inventory Inventory;

    // Start is called before the first frame update
    void Start()
    {
        // J : 임의로 재료 생성
        inventoryDict["Tomato"] = 2;
        inventoryDict["Tulip"] = 1;
        inventoryDict["Coconut"] = 3;


        SetInventory();
    }

    private void SetInventory()
    {
        foreach (KeyValuePair<string, int> slot in inventoryDict)
        {
            Item item = Resources.Load<Item>("Item/" + slot.Key);
            Inventory.AcquireItem(item, slot.Value);
        }
    }
}
