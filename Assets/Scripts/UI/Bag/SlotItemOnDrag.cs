using UnityEngine;
using UnityEngine.EventSystems;

public class SlotItemOnDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Slot oldParentSlot;
    public Slot newParentSlot;

    public void OnBeginDrag(PointerEventData eventData)
    {
        oldParentSlot = transform.parent.GetComponent<Slot>();
        if (transform.parent.GetComponent<Slot>() != null && oldParentSlot.isContainedItem == true)
        {
            transform.SetParent(transform.parent.parent.parent);
            GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (oldParentSlot.isContainedItem == true)
        {
            // 将屏幕坐标转换为世界坐标
            RectTransformUtility.ScreenPointToWorldPointInRectangle(GetComponent<RectTransform>(), eventData.position, eventData.pressEventCamera, out Vector3 worldPosition);
            // 设置物体的位置
            GetComponent<RectTransform>().position = worldPosition;
            //Debug.Log(eventData.pointerCurrentRaycast.gameObject);
        }

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (oldParentSlot.isContainedItem == true)
        {
            //当目标点不为空，有Slot，目标格子属于背包格子时
            if (eventData.pointerCurrentRaycast.isValid && eventData.pointerCurrentRaycast.gameObject.transform.parent.GetComponent<Slot>() != null && eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.name == "Grid")
            {
                newParentSlot = eventData.pointerCurrentRaycast.gameObject.transform.parent.GetComponent<Slot>();
                //当原格子是背包格子时，执行交换逻辑
                if (oldParentSlot.transform.parent.name == "Grid")
                {
                    if (newParentSlot.isContainedItem == false)
                    {
                        ExchangeBagItem();
                        oldParentSlot.isContainedItem = false;
                        newParentSlot.isContainedItem = true;
                    }
                    else
                    {
                        ExchangeBagItem();
                    }
                }
                //当原格子是combine格子时，执行交换逻辑,并更改对应属性isInCombinePanle
                else if (oldParentSlot.transform.parent.name == "CombinePanel")
                {
                    if (newParentSlot.isContainedItem == false)
                    {
                        ExchangeBagItem();
                        oldParentSlot.isContainedItem = false;
                        newParentSlot.isContainedItem = true;
                        newParentSlot.containedItem.isInCombinePanle = false;
                    }
                    else
                    {
                        ExchangeBagItem();
                        oldParentSlot.containedItem.isInCombinePanle = true;
                        newParentSlot.containedItem.isInCombinePanle = false;
                    }
                }
                //当原格子是道具格子时，执行交换逻辑
                else if (oldParentSlot.transform.parent.name == "PropGrid")
                {
                    if (newParentSlot.isContainedItem == false)
                    {
                        ExchangeBagItem();
                        oldParentSlot.isContainedItem = false;
                        newParentSlot.isContainedItem = true;
                    }
                    else
                    {
                        ExchangeBagItem();
                    }
                }

                    gameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;
            }
            //当目标点不为空，有Slot，且属于combine格子
            else if (eventData.pointerCurrentRaycast.isValid && eventData.pointerCurrentRaycast.gameObject.transform.parent.GetComponent<Slot>() != null && eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.name == "CombinePanel")
            {
                newParentSlot = eventData.pointerCurrentRaycast.gameObject.transform.parent.GetComponent<Slot>();
                //当原格子是combine格子时，执行交换逻辑,并将新旧格子里的物体属性isInCombinePanle都改为true并移动
                if (oldParentSlot.transform.parent.name == "CombinePanel")
                {
                    if (newParentSlot.isContainedItem == false)
                    {
                        ExchangeBagItem();
                        oldParentSlot.isContainedItem = false;
                        newParentSlot.isContainedItem = true;
                    }
                    else
                    {
                        ExchangeBagItem();
                        oldParentSlot.containedItem.isInCombinePanle = true;
                        newParentSlot.containedItem.isInCombinePanle = true;
                    }
                }
                //当原格子不是combine格子时，执行交换逻辑,并将新格子里的物体属性isInCombinePanle改为true，旧格子为false
                else
                {
                    if (newParentSlot.isContainedItem == false)
                    {
                        ExchangeBagItem();
                        oldParentSlot.isContainedItem = false;
                        newParentSlot.isContainedItem = true;
                        oldParentSlot.containedItem.isInCombinePanle = false;
                        newParentSlot.containedItem.isInCombinePanle = true;
                    }
                    else
                    {
                        ExchangeBagItem();
                        oldParentSlot.containedItem.isInCombinePanle = false;
                        newParentSlot.containedItem.isInCombinePanle = true;
                    }
                }
                gameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;
            }
            //当目标点不为空，有Slot，且属于prop格子
            else if (eventData.pointerCurrentRaycast.isValid && eventData.pointerCurrentRaycast.gameObject.transform.parent.GetComponent<Slot>() != null && eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.name == "PropGrid")
            {
                newParentSlot = eventData.pointerCurrentRaycast.gameObject.transform.parent.GetComponent<Slot>();
                if (oldParentSlot.transform.parent.name == "Grid")
                {
                    if (newParentSlot.isContainedItem == false)
                    {
                        ExchangeBagItem();
                        oldParentSlot.isContainedItem = false;
                        newParentSlot.isContainedItem = true;
                    }
                    else
                    {
                        ExchangeBagItem();
                    }
                }
                else if (oldParentSlot.transform.parent.name == "CombinePanel")
                {
                    if (newParentSlot.isContainedItem == false)
                    {
                        ExchangeBagItem();
                        oldParentSlot.isContainedItem = false;
                        newParentSlot.isContainedItem = true;
                        newParentSlot.containedItem.isInCombinePanle = false;
                    }
                    else
                    {
                        ExchangeBagItem();
                        oldParentSlot.containedItem.isInCombinePanle = true;
                        newParentSlot.containedItem.isInCombinePanle = false;
                    }
                }
                else if (oldParentSlot.transform.parent.name == "PropGrid")
                {
                    if (newParentSlot.isContainedItem == false)
                    {
                        ExchangeBagItem();
                        oldParentSlot.isContainedItem = false;
                        newParentSlot.isContainedItem = true;
                    }
                    else
                    {
                        ExchangeBagItem();
                    }
                }
                gameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;
            }
            //目标点为空或没有Slot
            else
            {
                transform.SetParent(oldParentSlot.transform);
                transform.position = oldParentSlot.transform.position;
                gameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;
            }
        }
    }

    public void ExchangeBagItem()
    {
        InventorySaver.Instance.TempSlot.containedItem = oldParentSlot.containedItem;
        oldParentSlot.containedItem = newParentSlot.containedItem;
        newParentSlot.containedItem = InventorySaver.Instance.TempSlot.containedItem;
        transform.SetParent(oldParentSlot.transform);
        transform.position = oldParentSlot.transform.position;
    }


}
