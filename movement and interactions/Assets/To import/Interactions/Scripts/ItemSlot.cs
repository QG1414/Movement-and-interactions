using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace InteractionHandler.Frame
{
    public class ItemSlot : MonoBehaviour, IDropHandler
    {
        private RectTransform objectRectTransform;
        [SerializeField] Types type;
        public void OnDrop(PointerEventData eventData)
        {
            switch ((int)type) {
                case 0:
                    if (eventData.pointerDrag != null)
                    {
                        eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = objectRectTransform.anchoredPosition;
                        eventData.pointerDrag.GetComponent<DragAndDrop>().changed = true;
                    }
                    break;
                case 1:
                    break;
            }
        }
        private void Start()
        {
            objectRectTransform = GetComponent<RectTransform>();
        }
    }

    enum Types
    {
        ChestItems,
        Helmet,
        ChestPlate,
        Boots,
        Weapon,
        Ring
    }
}
