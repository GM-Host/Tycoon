using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FlavorUI : MonoBehaviour
{
    public TMP_Text flavorText;
    public void PrintFlavor(string data)
    {
        flavorText.text = data;
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
