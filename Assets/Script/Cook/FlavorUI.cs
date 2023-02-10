using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FlavorUI : MonoBehaviour
{
    [SerializeField] private GameObject flavorUI;
    public Camera uiCamera;
    private RectTransform menuUITr;
    private Vector2 screenPoint;
    public TMP_Text flavorText;
    public void PrintFlavor(string data)
    {
        flavorUI.SetActive(true);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(flavorUI.GetComponent<RectTransform>(), Input.mousePosition, uiCamera, out screenPoint);
        menuUITr.localPosition = screenPoint;
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
