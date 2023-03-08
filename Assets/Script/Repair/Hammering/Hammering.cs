using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Repair
{
    class Hammering : MonoBehaviour
    {
        [SerializeField] private GameObject[] TargetObjects;
        private int clickedTarget = 0;

        public void ClickTarget()
        {
            clickedTarget++;

            if(clickedTarget >= TargetObjects.Length)
            {
                // 다음 씬으로 넘어가기
            }
        }
    }
}