using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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
            // ����Ļ����ת��Ϊ��������
            RectTransformUtility.ScreenPointToWorldPointInRectangle(GetComponent<RectTransform>(), eventData.position, eventData.pressEventCamera, out Vector3 worldPosition);
            // ���������λ��
            GetComponent<RectTransform>().position = worldPosition;
            //Debug.Log(eventData.pointerCurrentRaycast.gameObject);
        }

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (oldParentSlot.isContainedItem == true)
        {
            //��Ŀ��㲻Ϊ�գ���Slot��Ŀ��������ڱ�������ʱ
            if (eventData.pointerCurrentRaycast.isValid && eventData.pointerCurrentRaycast.gameObject.transform.parent.GetComponent<Slot>() != null && eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.name == "Grid")
            {
                newParentSlot = eventData.pointerCurrentRaycast.gameObject.transform.parent.GetComponent<Slot>();
                //��ԭ�����Ǳ�������ʱ��ִ�н����߼�
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
                //��ԭ������combine����ʱ��ִ�н����߼�,�����Ķ�Ӧ����isInCombinePanle
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

                gameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;
            }
            //��Ŀ��㲻Ϊ�գ���Slot��������combine����
            else if (eventData.pointerCurrentRaycast.isValid && eventData.pointerCurrentRaycast.gameObject.transform.parent.GetComponent<Slot>() != null && eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.name == "CombinePanel")
            {
                newParentSlot = eventData.pointerCurrentRaycast.gameObject.transform.parent.GetComponent<Slot>();
                //��ԭ������combine����ʱ��ִ�н����߼�,�����¾ɸ��������������isInCombinePanle����Ϊtrue���ƶ�
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
                //��ԭ���Ӳ���combine����ʱ��ִ�н����߼�,�����¸��������������isInCombinePanle��Ϊtrue���ɸ���Ϊfalse
                else
                {
                    if (newParentSlot.isContainedItem == false)
                    {
                        ExchangeBagItem();
                        oldParentSlot.isContainedItem = false;
                        newParentSlot.isContainedItem = true;
                        oldParentSlot.containedItem.isInCombinePanle = false;
                        newParentSlot.containedItem.isInCombinePanle = true;
                        Debug.Log("3");
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
            //Ŀ���Ϊ�ջ�û��Slot
            else
            {
                transform.SetParent(oldParentSlot.transform);
                transform.position = oldParentSlot.transform.position;
                gameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;
            }
            //CombineController.Instance.RefreshConbineSlots();
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
