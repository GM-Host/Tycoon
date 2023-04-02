using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookDataManager : MonoBehaviour
{
    public class CookObject {
        public string id;

    }
    public List<CookObject> curCook = new List<CookObject>();
    public static CookDataManager Instance;
    private List<Dictionary<string, object>> data;
    // Start is called before the first frame update
    void Awake()
    {
        // Singleton
        Instance = this;

        // Read Noelle Dialog Database
        data = CSVReader.Read("NoelleDialog");

        // test
        Inventory food0001 = new Inventory();
        food0001.imgId = "food0001";
        food0001.count = 4;
        food0001.name = "건식 전투식량 블럭";
        curInv.Add(food0001);
    }

    /**************************
        # 2 -> # CDM -> # 2
    **************************/
    public string DialogNoelle()
    {
        int random = Random.Range(0, data.Count);
        
        return data[random]["대사"].ToString();
    }

    /**************************
        # 4 -> # CDM -> # 6
    **************************/
    public void CleanHistory(int cleanAll)
    {
        CookUI cookUI = FindObjectOfType<CookUI>();

        print(curCook.Count);

        if(cleanAll == 1)
        {
            cookUI.cleanHistory();
            // remove history data
            curCook.Clear();
        }
        else
        {
            if(curCook.Count != 0)
            {
                cookUI.deleteHistory(curCook[curCook.Count - 1].id);
                // remove history data
                curCook.RemoveAt(curCook.Count - 1);
            }
        }
        hasHistory = false;
    }

    /**************************
        # 8 -> # CDM -> # 6
    **************************/
    public void ItemSelected(Inventory item)
    {
        // update Inventory
        if(item.count > 1)
        {
            item.count--;
        }
        else
        {
            item.count--;
        }
        InventoryUI invUI = FindObjectOfType<InventoryUI>();
        invUI.updateInv();
        if(item.count==0)
            curInv.Remove(item);

        // update history

    }

    public bool hasHistory = false;

    public class Inventory
    {
        public string imgId;
        public int count = 0;
        public string name;
    }
    
    public List<Inventory> curInv = new List<Inventory>();

    // Update is called once per frame
    void Update()
    {
        
    }
}
