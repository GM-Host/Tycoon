using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FlavorUI : MonoBehaviour
{
    public GameObject flavorUI;
    public Camera uiCamera;
    [SerializeField] private RectTransform menuUITr;
    private Vector2 screenPoint;
    public TMP_Text flavorText;
    public void PrintFlavor(string data)
    {
        // GameObject[] children = flavorUI.GetComponentsInChildren<GameObject>();
        // foreach(GameObject objects in children)
        // {
        //     objects.SetActive(true);
        // }
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
