using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interaction : MonoBehaviour
{
    [SerializeField]
    private Animator stampAnimator;
    [SerializeField]
    private GameObject expansionPaper;
    [SerializeField]
    private GameObject stamps;
    [SerializeField]
    private GameObject toad;
    [SerializeField]
    private Sprite toadImage1, toadImage2;

    private bool stampActive;

    public void ClickBackground()
    {
        expansionPaper.SetActive(false);
    }

    public void ClickToad()
    {
        if (stampActive)
        {
            stampAnimator.SetBool("appear", false);
            toad.GetComponent<Image>().sprite = toadImage1;
        }
        else
        {
            stampAnimator.SetBool("appear", true);
            toad.GetComponent<Image>().sprite = toadImage2;
        }
        stampActive = !stampActive;
    }

    public void ClickBell()
    {
        Debug.Log("º§ Å¬¸¯");
    }
}
