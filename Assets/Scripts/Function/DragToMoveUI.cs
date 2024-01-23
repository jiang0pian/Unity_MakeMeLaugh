using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragToMoveUI : MonoBehaviour,IDragHandler
{
    RectTransform currentRectTransform;
    private void Awake()
    {
        currentRectTransform = GetComponent<RectTransform>();
    }
    public void OnDrag(PointerEventData eventData)
    {
        currentRectTransform.anchoredPosition += eventData.delta;
    }
}
