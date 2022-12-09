using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        updateInv();
    }

    /********************
        # CDM -> # 8
    ********************/
    public Transform inventory;
    public Object itemWin;
    public void updateInv()
    {
        foreach (CookDataManager.Inventory item in CookDataManager.Instance.curInv)
        {
            // 이미 인벤토리에 존재할 때
            if(inventory.Find(item.imgId))
            {
                GameObject already = inventory.Find(item.imgId).gameObject;
                // 개수만 업데이트
                if(item.count == 1)
                    already.transform.Find("countbox").gameObject.SetActive(false);
                else if(item.count == 0)
                    Destroy(already);
                else
                {
                    print(already.name);
                    already.transform.Find("countbox").gameObject.SetActive(true);
                    already.transform.Find("countbox").GetComponentInChildren<Text>().text = item.count.ToString();
                }
                already.GetComponent<ItemUI>().item = item;
                return;
            }
            // 인벤토리에 없는 새 아이템일 때
            GameObject temp = Instantiate(itemWin, inventory) as GameObject;
            temp.name = item.imgId;
            temp.transform.Find("img").GetComponent<Image>().sprite = Resources.Load("Cook/Items/" + item.imgId, typeof(Sprite)) as Sprite;
            temp.transform.Find("name").GetComponent<Text>().text = item.name;
            if(item.count > 1)
            {
                GameObject countbox = temp.transform.Find("countbox").gameObject;
                countbox.SetActive(true);
                countbox.transform.GetComponentInChildren<Text>().text = item.count.ToString();
            }
            // 위에 있는 코드 ItemUI에 넣을지 말지...
            temp.GetComponent<ItemUI>().item = item;
        }
    }

    
    // Update is called once per frame
    void Update()
    {
        
    }
}
