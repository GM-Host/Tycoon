using System;
using UnityEngine;
using UnityEngine.UI;

namespace Repair
{
    class TimingBar : MonoBehaviour
    {
        [SerializeField] [Range(0f, 10f)] private float speed = 1f;

        [SerializeField] private Transform blueTarget;
        [SerializeField] private Image yellowLine;

        private float runningTime = 0f;
        private float xPos = 0f;
        private float length = 1600f - 70f;

        private float yellowLineX = 0f;
        private float yellowLineY = 0f;

        private float delayTime = 2f;
        private int count = 0;

        void Start()
        {
            yellowLineX = yellowLine.transform.position.x;
            yellowLineY = yellowLine.transform.position.y;
        }
        void Update()
        {
            runningTime += Time.deltaTime * speed;
            xPos = Mathf.Sin(runningTime) * (length / 2) + (length / 2) + 70;
            Debug.Log(xPos);
            yellowLine.transform.localPosition = new Vector2(xPos, yellowLineY);

            // 클릭하고 2초 정지 후 타겟 크기 줄어듦
            if(Input.GetMouseButtonDown(0))
            {
                float time = 0f;
                while(time < delayTime)
                {
                    time += Time.deltaTime;
                }

                CheckYellowLinePosition();

                count++;
            }

            if(count >= 3)
            {
                // 2초 대기 후 다음 화면으로 넘어감
            }
        }

        private void CheckYellowLinePosition()
        {
            float targetLeftX = blueTarget.position.x;
            float targetRightX = targetLeftX + blueTarget.GetComponent<RectTransform>().sizeDelta.x;

            float lineLeftX = yellowLine.transform.position.x;
            float lineRightX = lineLeftX + yellowLine.GetComponent<RectTransform>().sizeDelta.x;

            // Perfect
            if(targetLeftX <= lineLeftX && lineRightX <= targetRightX)
            {
                print("Perfect");
            }

            // Bad
            else if(targetRightX < targetLeftX || targetLeftX < lineLeftX)
            {
                print("Bad");
            }

            // Good
            else
            {
                print("Good");
            }
        }
    }
}
