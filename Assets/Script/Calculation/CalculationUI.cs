using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculationUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(OpenCurtain(LeftCurtain, -1));
        StartCoroutine(OpenCurtain(RightCurtain, 1));
        isFirstCal = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Trigger if the parchment is clicked
    private int isSized = 1;
    private bool isOpened = true;
    private bool isFirstCal = true;
    [Header("First Settlement UI")]
    public GameObject firstSettleWindow;
    public void ClickedParchment()
    {
        StartCoroutine(ScaleUpAndMove(parchment, isSized));
        openedImg.SetActive(isOpened);
        openedWindow.SetActive(isOpened);
        isSized = -isSized;
        if(!isOpened && isFirstCal)
        {
            firstSettleWindow.SetActive(true);
            // Call First Settle Coroutine
            isFirstCal = false;
        }
        isOpened = !isOpened;
    }


    [Header("Curtain UI")]
    public RectTransform LeftCurtain;
    public RectTransform RightCurtain;
    private float speedOfCurtain = 400f;
    private IEnumerator OpenCurtain(RectTransform curtain, int dir)
    {
        bool isStart = true;
        while(true)
        {
            if(isStart == true)
            {
                curtain.anchoredPosition += 
                        new Vector2(dir * speedOfCurtain * Time.deltaTime, 0);
                if(Mathf.Abs(curtain.anchoredPosition.x) > 380)
                {
                    isStart = false;
                    break;
                }
            }
            yield return null;
        }
    }

    [Header("Parchment UI")]
    public RectTransform parchment;
    public GameObject openedImg;
    public GameObject openedWindow;
    private IEnumerator ScaleUpAndMove(RectTransform parchment, int dir)
    {
        bool isStart = true;
        bool isPosY = true;
        while(true)
        {
            if(isStart == true)
            {
                print(parchment.sizeDelta.x + " : " + parchment.sizeDelta.y);
                // Scale Up
                parchment.sizeDelta += 
                        new Vector2(dir * speedOfCurtain * Time.deltaTime, dir * speedOfCurtain * Time.deltaTime);
                // Roatate
                parchment.transform.Rotate(new Vector3(0,0, dir * 50f * Time.deltaTime));
                // Translate
                if(isPosY == true)
                {
                    parchment.anchoredPosition += 
                        new Vector2(0, dir * speedOfCurtain * Time.deltaTime);

                }
                if(parchment.anchoredPosition.y > 0 || parchment.anchoredPosition.y < -365)    // Translate until
                {
                    isPosY = false;
                }

                // Scale up and rotate until
                if((parchment.sizeDelta.x > 500 && parchment.rotation.z > 0) || 
                (parchment.sizeDelta.x < 91))
                {
                    isStart = false;
                    break;
                }
            }
            yield return null;
        }
    }
}
