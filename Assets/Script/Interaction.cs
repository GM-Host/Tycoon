using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interaction : MonoBehaviour
{
    [SerializeField]
    private Animator stampAnimator;
    [SerializeField]
    private GameObject closeUpIdentity;
    [SerializeField]
    private GameObject stamps;
    [SerializeField]
    private GameObject toad;
    [SerializeField]
    private Sprite toadImage1, toadImage2;

    private bool stampActive;

    public void ClickBackground()
    {
        closeUpIdentity.SetActive(false);
        GameObject.Find("Canvas").transform.Find("Identity(Clone)").GetComponent<Identity>().ReleaseCloseUp();  // 클로즈업 해제
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
        Debug.Log("벨 클릭");
    }
}
