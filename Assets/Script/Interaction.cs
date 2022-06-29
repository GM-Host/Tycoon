using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interaction : MonoBehaviour
{
    [SerializeField]
    private GameObject stamps;
    [SerializeField]
    private GameObject toad;
    [SerializeField]
    private Sprite toadImage1, toadImage2;
    public void ClickToad()
    {
        if (stamps.activeSelf)
        {
            stamps.SetActive(false);
            toad.GetComponent<Image>().sprite = toadImage1;
        }
        else
        {
            stamps.SetActive(true);
            toad.GetComponent<Image>().sprite = toadImage2;
        }
    }

    public void ClickBell()
    {
        Debug.Log("º§ Å¬¸¯");
    }
}
