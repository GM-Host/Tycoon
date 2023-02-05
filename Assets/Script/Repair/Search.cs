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
            tstring += "내구도     " + Repair.WeaponInfo.state.iDurabilityState;
            tstring += "\n공격력     " + Repair.WeaponInfo.state.iDamageState;
            tstring += "\n방어력     " + Repair.WeaponInfo.state.iDefenseState;
            tstring += "\n룬 레벨    " + Repair.WeaponInfo.state.iRuneLevel;

            MonitorText.text = tstring;
            MonitorTextPanel.SetActive(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            MonitorTextPanel.SetActive(false);
        }
    }
}