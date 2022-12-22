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
        inventoryDict["Potion_Processing_Material_1008"] = 1;
        inventoryDict["Potion_Processing_Material_1009"] = 2;
        inventoryDict["Potion_Processing_Material_1010"] = 3;


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
