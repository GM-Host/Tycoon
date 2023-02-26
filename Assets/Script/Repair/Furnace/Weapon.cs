using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Repair
{
    class Weapon : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public static GameObject itemBeingDragged;
        Vector3 startPosition;

        public void OnBeginDrag(PointerEventData eventData)
        {
            itemBeingDragged = gameObject;
            startPosition = transform.position;
        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = Input.mousePosition;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            itemBeingDragged = null;
            transform.position = startPosition;
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            if(collision.gameObject.name == "Begin")
                Debug.Log("ㅇㅇ");
        }
    }
}
