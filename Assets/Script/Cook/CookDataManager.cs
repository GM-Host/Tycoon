using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class CookDataManager : MonoBehaviour
{
    // History Object Class
    public class CookObject {
        public string id;
        public CookInventory itemInfo = null;
    }
    public List<CookObject> curCook = new List<CookObject>();

    // Inventory Information Class
    public class CookInventory
    {
        public string imgId;
        public int count = 0;
        public string name;
    }

    // 드래그 중인 아이템 정보 갱신
    public CookInventory draggingItem;
    public void DraggingItem(CookInventory item)
    {
        draggingItem = item;
    }

    public Dictionary<string, CookInventory> curInv = new Dictionary<string, CookInventory>();


    // Noelle Dialog Data
    private List<Dictionary<string, object>> dialogData;
    // Flavor Data
    private List<Dictionary<string, object>> flavorData;
    // Recipe Data
    private List<Dictionary<string, object>> recipeData;
    private List<Dictionary<string, object>> findRecipe;

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
        findRecipe = CSVReader.Read("FindRecipe");

        // test
        CookInventory food0001 = new CookInventory();
        food0001.imgId = "food0001";
        food0001.count = 4;
        food0001.name = "건식 전투식량 블럭";
        curInv.Add(food0001.imgId, food0001);
        CookInventory food0003 = new CookInventory();
        food0003.imgId = "food0003";
        food0003.count = 6;
        food0003.name = "굽은 뿔 산양 위장주머니";
        curInv.Add(food0003.imgId, food0003);
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


    private void ItemUnselect(CookInventory item)
    {
        print("ItemUnselect Initial Stage");
        CookInventory temp;
        if(!curInv.ContainsKey(item.imgId))    // 현재 inven에 없는 아이템인 경우
        {
            curInv.Add(item.imgId, item);
        }
        else
        {
            curInv.TryGetValue(item.imgId, out temp);
            temp.count++;
        }
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
    public void ItemSelected(CookInventory item)
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
        item.count--;
        InventoryUI invUI = FindObjectOfType<InventoryUI>();
        invUI.updateInv();
        if(item.count==0)
            curInv.Remove(item.imgId);

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
        string recipe = "";

        for (int i = 1 ; i <= 3 ; i++)
        {
            // "레시피N"
            string column = "레시피";
            bool isRecipeCor = true;
            column += i.ToString();
            recipe = findRecipe[int.Parse(Regex.Replace(curCook[0].id, @"\D", "")) - 1][column].ToString();
            
            // 레시피 찾았을 때
            if(recipe != "")
            {
                // 해당 레시피 행
                int row = int.Parse(Regex.Replace(recipe, @"\D", "")) - 1 ;
                // 해당 레시피 과정 수
                int count = int.Parse(recipeData[row]["과정_Count"].ToString());
                if(count * 2 != curCook.Count || recipeData[row]["과정1_ID"].ToString() != curCook[1].id)
                    continue;  // 과정 수 틀리거나, 첫번째 과정 틀렸을 때 다음 레시피 탐색
                

                // 과정 수 맞으면 나머지 모든 재료와 과정 확인하기
                for(int j = 2 ; j <= count && isRecipeCor==true ; j++)
                {
                    if(curCook[j].id != recipeData[row]["재료"+j.ToString()+"_ID"].ToString()
                    || curCook[j+1].id != recipeData[row]["과정"+j.ToString()+"_ID"].ToString())
                    isRecipeCor = false;
                }
                print("isRecipeCor: "+isRecipeCor);
                // 레시피 내용 중 틀린 게 있을 때
                if(!isRecipeCor)
                    continue;   // 다음 레시피 탐색
                // 레시피 모든 내용 맞았을 때
                else
                {
                    resultUI.ShowResult("Success", count);
                    break;
                }
                
            }

            // 레시피 없을 때
            else
            {
                print("Fail to find recipe...");
            }
        }
        
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
