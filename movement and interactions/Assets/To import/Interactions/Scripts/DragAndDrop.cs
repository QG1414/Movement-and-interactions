using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using InteractionHandler.Frame;

namespace InteractionHandler
{
    public class DragAndDrop : MonoBehaviour, IDragHandler,IBeginDragHandler,IEndDragHandler
    {
        public bool changed = false;
        private Vector2 startingposition;

        private CanvasGroup canvasGroup;
        private RectTransform rectTransform;
        private Canvas canvas;


        public void OnBeginDrag(PointerEventData eventData)
        {
            startingposition = rectTransform.anchoredPosition;
            canvasGroup.alpha = .6f;
            canvasGroup.blocksRaycasts = false;
            changed = false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            rectTransform.anchoredPosition += eventData.delta/canvas.scaleFactor;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            canvasGroup.alpha = 1f;
            canvasGroup.blocksRaycasts = true;
            if (!changed)
                rectTransform.anchoredPosition = startingposition;
        }

        public void Exchange(Vector2 changedPosition)
        {
            rectTransform.anchoredPosition = changedPosition;
        }

        void Start()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            rectTransform = GetComponent<RectTransform>();
            canvas = GetComponentInParent<Canvas>();
        }


    }



}
