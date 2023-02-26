using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Repair
{
    class Weapon : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        // 드래그
        public static GameObject itemBeingDragged;
        private Vector3 startPosition;

        // 불꽃 탐지용 레이
        private float distance;
        private RaycastHit2D rayHit;
        private Ray2D ray;

        // 호버링
        private bool hovering = false;
        private Image gaugeBar;
        private float fLimitTime;
        private string strType = "";

        void FixedUpdate()
        {
            if(hovering)
            {

            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            itemBeingDragged = gameObject;
            startPosition = transform.position;

            ray = new Ray2D();
            ray.origin = startPosition;
            ray.direction = transform.forward;

            rayHit = Physics2D.Raycast(transform.position, -Vector2.up);
        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = Input.mousePosition;

            if((rayHit = Physics2D.Raycast(transform.position, -Vector2.up)))
            {
                string tstrFireName = rayHit.transform.name;
                switch(tstrFireName)
                {
                    case "Begin": break;
                    case "Brave": break;
                    case "Bless": break;
                    case "Clear": break;
                }
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            itemBeingDragged = null;
            transform.position = startPosition;
        }
    }
}
