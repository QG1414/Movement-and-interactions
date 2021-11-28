using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using InteractionHandler.Frame;

namespace InteractionHandler
{
    public class DragAndDrop : MonoBehaviour, IDragHandler,IBeginDragHandler,IEndDragHandler
    {
        public bool changed = false; // bool do sprawdzania czy obiekt dosta³ now¹ pozycjê
        private Vector2 startingposition; // pozycja statrowa 

        private CanvasGroup canvasGroup; // u¿ywany do zmiany przezroczystoœci podnoszonego obiektu i by nie blokowa³ promieni 
        private RectTransform rectTransform; // aktualna pozycja obiektu
        private Canvas canvas; // canvas na którym jest umieszczony obiekt


        public void OnBeginDrag(PointerEventData eventData) // podczas gdy zlapiemy obiekt
        {
            startingposition = rectTransform.anchoredPosition;// ustawiamy pozycje startow¹
            canvasGroup.alpha = .6f; // zwiêkszamy przezroczystoœæ obiektu 
            canvasGroup.blocksRaycasts = false; // ustawienie by ray z myszki przechodzi³ przez obiekt
            changed = false; // ustawiamy ¿e aktualnie obiekt nie ma ¿adnego nowego slotu
        }

        public void OnDrag(PointerEventData eventData) // podczas przeci¹gania
        {
            rectTransform.anchoredPosition += eventData.delta/canvas.scaleFactor; // zmieniamy pozycjê naszego obiektu relatywnie do rozmiaru canvasu
        }

        public void OnEndDrag(PointerEventData eventData) // na koniec przeci¹gania gdy puszamy klawisz myszy
        {
            canvasGroup.alpha = 1f; // przezroczystoœæ wraca do podstawowej pozycji
            canvasGroup.blocksRaycasts = true; // obiekt teraz blokuje raye z myszy
            if (!changed) // je¿eli obiekt nie dosta³ nowego slotu
                rectTransform.anchoredPosition = startingposition; // to pozycja obiektu wraca do startowej
        }

        void Start()
        {
            canvasGroup = GetComponent<CanvasGroup>(); // ustalamy canvasGroup
            rectTransform = GetComponent<RectTransform>(); // ustalamy RectTransform
            canvas = GetComponentInParent<Canvas>(); // ustalamy Canvas
        }


    }



}
