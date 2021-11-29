using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Delore.UI
{
    public class ItemSlot : MonoBehaviour, IDropHandler
    {
        private RectTransform objectRectTransform;//pozycja twojego obiektu
        [SerializeField] Types type; // typ przygotowany na przysz³oœæ 
        public void OnDrop(PointerEventData eventData) // metoda u¿ywana przy puszaniu myszy
        {
            switch ((int)type) {//sprawdzamy typ
                case 0:
                    if (eventData.pointerDrag != null)//je¿eli coœ przeci¹gamy wykonaj 
                    {
                        eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = objectRectTransform.anchoredPosition;//zmieñ pozycje przeci¹ganego obiektu na pozycje slotu w UI
                        eventData.pointerDrag.GetComponent<DragAndDrop>().changed = true; // zmieñ wartoœæ w skrypcie obiektu by nie wróci³ na pozycjê startow¹
                    }
                    break;
                case 1:
                    break;
            }
        }
        private void Start()
        {
            objectRectTransform = GetComponent<RectTransform>(); // ustalamy pozycjê dla naszego slotu
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
