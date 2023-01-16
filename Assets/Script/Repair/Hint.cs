using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Repair
{
    class Hint
    {
        [SerializeField] private GameObject HintText;
        private string strHintText;

        public void SetHint(string pstrHintText)
        {
            strHintText = pstrHintText;
        }
    }
}
