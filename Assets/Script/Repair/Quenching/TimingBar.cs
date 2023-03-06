using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Repair
{
    class TimingBar : MonoBehaviour
    {
        [SerializeField] [Range(0f, 10f)]   private float speed = 1f;
        [SerializeField] [Range(0f, 5f)]    private float delayTime = 0.5f;
        [SerializeField] [Range(0f, 5f)]    private float reduceLen = 10f;


        [SerializeField] private Transform blueTarget;
        [SerializeField] private Image yellowLine;

        private float length = 1600f - 70f;

        private float runningTime = 0f;
        private float xPos = 0f;
        private RectTransform rectTransform;
        private float yellowLineY = 0f;
        private int count = 0;

        void Start()
        {
            yellowLineY = yellowLine.transform.localPosition.y;

            StartCoroutine(MoveTimingBar());
        }

        private IEnumerator MoveTimingBar()
        {
            while(count < 3)
            {
                runningTime += Time.deltaTime * speed;
                xPos = Mathf.Sin(runningTime) * (length / 2) - 10;
                //Debug.Log(xPos);
                yellowLine.transform.localPosition = new Vector2(xPos, yellowLineY);

                // 클릭하고 2초 정지 후 타겟 크기 줄어듦
                if (Input.GetMouseButtonDown(0))
                {
                    yield return new WaitForSeconds(delayTime);

                    if(CheckYellowLinePosition())
                    {
                        print("perfect");
                        count++;
                        ReduceBlueTargetLen();
                    }
                }

                yield return null;
            }

            print("끝!");
        }

        private bool CheckYellowLinePosition()
        {
            float targetLeftX = blueTarget.position.x;
            float targetRightX = targetLeftX + blueTarget.GetComponent<RectTransform>().sizeDelta.x;

            float lineLeftX = yellowLine.transform.position.x;
            float lineRightX = lineLeftX + yellowLine.GetComponent<RectTransform>().sizeDelta.x;

            //print("target : " + targetLeftX + ", " + targetRightX);
            //print("line : " + lineLeftX + ", " + lineRightX);
            
            if(targetLeftX <= lineLeftX && lineRightX <= targetRightX)
            {
                return true;
            }

            return false;
        }

        private void ReduceBlueTargetLen()
        {
            float sizeDeltaX = blueTarget.GetComponent<RectTransform>().sizeDelta.x;
            float sizeDeltaY = blueTarget.GetComponent<RectTransform>().sizeDelta.y;

            print(sizeDeltaX + " " + sizeDeltaY);

            blueTarget.GetComponent<RectTransform>().sizeDelta = new Vector2(sizeDeltaX - reduceLen, sizeDeltaY);
        }
    }
}
