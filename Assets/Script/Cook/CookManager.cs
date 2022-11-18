using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        List<Dictionary<string, object>> data = CSVReader.Read("NoelleDialog");
        print("This is data[0][대사_ID]"+data[0]["대사_ID"]+"end");print("This is data[1]"+data[1]+"end");
    }

    public string DialogNoelle()
    {
        
        return "";
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
