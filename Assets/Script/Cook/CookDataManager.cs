using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookDataManager : MonoBehaviour
{
    // History Object Class
    public class CookObject {
        public string id;
        public Inventory itemInfo = null;
    }
    public List<CookObject> curCook = new List<CookObject>();

    // Inventory Information Class    
    public class Inventory
    {
        public string imgId;
        public int count = 0;
        public string name;
    }
    public List<Inventory> curInv = new List<Inventory>();

    // Noelle Dialog Data
    private List<Dictionary<string, object>> dialogData;
    // Flavor Data
    private List<Dictionary<string, object>> flavorData;
    // Recipe Data
    private List<Dictionary<string, object>> recipeData;

    public static CookDataManager Instance;
    // Start is called before the first frame update
    void Awake()
    {
        // Singleton
        Instance = this;

        // Read Noelle Dialog Database
        dialogData = CSVReader.Read("NoelleDialog");
        flavorData = CSVReader.Read("Flavor");
        recipeData = CSVReader.Read("Recipe");

        // test
        Inventory food0001 = new Inventory();
        food0001.imgId = "food0001";
        food0001.count = 4;
        food0001.name = "건식 전투식량 블럭";
        curInv.Add(food0001);
        Inventory food0003 = new Inventory();
        food0003.imgId = "food0003";
        food0003.count = 6;
        food0003.name = "굽은 뿔 산양 위장주머니";
        curInv.Add(food0003);
    }

    /**************************
        # 2 -> # CDM -> # 2
    **************************/
    public string DialogNoelle()
    {
        int random = Random.Range(0, dialogData.Count);
        
        return dialogData[random]["대사"].ToString();
    }

    /**************************
        # 4 -> # CDM -> # 6
    **************************/
    public void CleanHistory(int cleanAll)
    {
        CookUI cookUI = FindObjectOfType<CookUI>();

        print("Clean History left = "+curCook.Count);

        if(cleanAll == 1)
        {
            cookUI.cleanHistory();
            numOfObj=0;
            hasHistory = false;
            order = Order.Food;
            // Item Inventory에 Unselect 하는 기능 아직 없음!!!
        }
        else
        {
            if(curCook.Count != 0)
            {
                if (order==Order.Operation && curCook[curCook.Count - 1].itemInfo != null)
                    ItemUnselect(curCook[curCook.Count - 1].itemInfo);
                cookUI.deleteHistory(curCook[curCook.Count - 1].id, curCook.Count-1);
                order = order==Order.Food ? Order.Operation : Order.Food;
                numOfObj--;
                foreach(CookObject obj in curCook)
                {
                    print("In Cur Cook : "+ obj.id);
                }
            }
            else
            {
                cookUI.cleanHistory();
                numOfObj=0;
                hasHistory = false;
            }
        }
    }

    private void ItemUnselect(Inventory item)
    {
        print("ItemUnselect Initial");
        Inventory temp;
        if(curInv.Find(temp => temp.imgId == item.imgId) == null)    // 현재 inven에 없는 아이템인 경우
        {
            curInv.Add(item);
        }
        else
            curInv.Find(temp => temp.imgId == item.imgId).count++;
        // update Inventory
        InventoryUI invUI = FindObjectOfType<InventoryUI>();
        invUI.updateInv();
    }


    /**************************
        # 8 -> # CDM -> # 6
    **************************/
    public int numOfObj = 0;
    public bool hasHistory = false;
    public enum Order {Food, Operation};
    public Order order = Order.Food;
    public void ItemSelected(Inventory item)
    {
        if(order != Order.Food || numOfObj == 6)
            return;

        // update history
        hasHistory = true;
        order = Order.Operation;
        CookObject itemObj = new CookObject();
        itemObj.id = item.imgId;
        itemObj.itemInfo = item;
        CookUI cookUI = FindObjectOfType<CookUI>();
        NoelleUI noelleUI = FindObjectOfType<NoelleUI>();
        noelleUI.DeleteDialog();
        curCook.Add(itemObj);
        cookUI.ShowObject(itemObj, curCook.Count-1);
        numOfObj++;


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

    }
    
    /**************************
        # 7 -> # CDM -> # 6
    **************************/
    public void OperSelected(CookObject oper)
    {
        if(order != Order.Operation || numOfObj == 6)
            return;
        // update history
        hasHistory = true;
        order = Order.Food;
        CookUI cookUI = FindObjectOfType<CookUI>();
        NoelleUI noelleUI = FindObjectOfType<NoelleUI>();
        noelleUI.DeleteDialog();
        curCook.Add(oper);
        cookUI.ShowObject(oper, curCook.Count-1);
        numOfObj++;
    }

    /**************************
        # 5 -> # CDM -> # 10
    **************************/
    public void MakeResult()
    {
        //List<CookObject> recipe0001 = new List<CookObject>();
        ResultUI resultUI = FindObjectOfType<ResultUI>();
        // Recipe0001
        if(curCook[0].id == "food0001" && curCook[1].id == "Steaming" && curCook.Count == 2)
        {
            resultUI.ShowResult("Success");
        }
        // Recipe0003
        else if(curCook[0].id == "food0003" && curCook[1].id == "Boiling" && curCook.Count == 2)
        {
            resultUI.ShowResult("Success");
        }
        else
            resultUI.ShowResult("Fail");
        // Clean History
        CleanHistory(1);
    }

    
    /**************************
        # 8 -> # CDM -> # 9
    **************************/
    public void SendFlavorData(int itemId)
    {
        FlavorUI flavorUI = FindObjectOfType<FlavorUI>();
        flavorUI.PrintFlavor(flavorData[itemId - 1]["플레이버_텍스트"].ToString());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
