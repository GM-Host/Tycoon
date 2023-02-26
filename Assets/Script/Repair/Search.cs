using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Repair
{
    public class Search : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private GameObject MonitorTextPanel;
        [SerializeField] private TMP_Text MonitorText;

        public void OnPointerEnter(PointerEventData eventData)
        {
            string tstring = "";
            tstring += "내구도   " + MakeInfoToString(Repair.WeaponInfo.state.iDurabilityState);
            tstring += "\n공격력   " + MakeInfoToString(Repair.WeaponInfo.state.iDamageState);
            tstring += "\n방어력   " + MakeInfoToString(Repair.WeaponInfo.state.iDefenseState);
            tstring += "\n룬 레벨  LV." + Repair.WeaponInfo.state.iRuneLevel;

            MonitorText.text = tstring;
            MonitorTextPanel.SetActive(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            MonitorTextPanel.SetActive(false);
        }

        string MakeInfoToString(int value)
        {
            string result = "";
            switch(value)
            {
                case 0: result = "매우 나쁨"; break;
                case 1: result = "나쁨"; break;
                case 2: result = "보통"; break;
                case 3: result = "좋음"; break;
                case 4: result = "매우 좋음"; break;
            }
            return result;
        }
    }
}