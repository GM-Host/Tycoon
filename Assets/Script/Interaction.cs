using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interaction : MonoBehaviour
{
    [SerializeField]
    private Animator stampAnimator;
    [SerializeField]
    private GameObject closeUp;
    [SerializeField]
    private GameObject closeUpIdentity, closeUpTierSeal;
    [SerializeField]
    private GameObject stamps;
    [SerializeField]
    private GameObject toad;
    [SerializeField]
    private Sprite toadImage1, toadImage2;

    private bool stampActive;

    // ���(CloseUp ������Ʈ) Ŭ�� -> Ŭ����� ����
    public void ClickCloseUp()
    {
        closeUp.SetActive(false);

        if (closeUpIdentity.activeSelf == true) // �ſ��� Ŭ����� ����
        {
            closeUpIdentity.SetActive(false);
            GameObject.Find("Canvas").transform.Find("Identity(Clone)").GetComponent<SpawnObject>().ReleaseCloseUp();
        }
        else    // ��ǥ Ŭ����� ����
        {
            closeUpTierSeal.SetActive(false);
            GameObject.Find("Canvas").transform.Find("TierSeal(Clone)").GetComponent<SpawnObject>().ReleaseCloseUp();
        }

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
        Debug.Log("�� Ŭ��");
    }
}
