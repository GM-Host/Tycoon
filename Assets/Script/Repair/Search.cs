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
            tstring += "������     " + Repair.WeaponInfo.state.iDurabilityState;
            tstring += "\n���ݷ�     " + Repair.WeaponInfo.state.iDamageState;
            tstring += "\n����     " + Repair.WeaponInfo.state.iDefenseState;
            tstring += "\n�� ����    " + Repair.WeaponInfo.state.iRuneLevel;

            MonitorText.text = tstring;
            MonitorTextPanel.SetActive(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            MonitorTextPanel.SetActive(false);
        }
    }
}