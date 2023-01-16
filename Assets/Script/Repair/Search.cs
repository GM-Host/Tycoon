using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Repair
{
    public class Search : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private GameObject MonitorText;

        private GuestDB.WeaponState dtWeaponState;

        public void SetMonitorInfo(GuestDB.WeaponState pdtWeaponState)
        {
            dtWeaponState = pdtWeaponState;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            MonitorText.SetActive(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            MonitorText.SetActive(false);
        }
    }
}