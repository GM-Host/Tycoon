using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookDataParse : MonoBehaviour
{
    public static CookDataParse Instance;
    private List<Dictionary<string, object>> data;
    // Start is called before the first frame update
    void Start()
    {
        // Singleton
        Instance = this;
        // Read Noelle Dialog Database
        data = CSVReader.Read("NoelleDialog");
        //print("This is data[0][대사_ID]"+data[0]["대사_ID"]+"end");print("This is data[1]"+data[0]+"end");
    }

    public string DialogNoelle()
    {
        int random = Random.Range(0, data.Count);
        
        return data[random]["대사"].ToString();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
