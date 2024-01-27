using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public BagItem containedItem;
    public TMP_Text amountText;
    public Image itemImage;
    public bool isContainedItem = false;

    public void RefreshItemIntroduction()
    {
        InventoryManager.Instance.RefreshItemIntroduction(transform.gameObject);
    }
    public void RefreshBagSlotInfo()
    {
        //Debug.Log("into RefreshSlot");
        if (containedItem != null)
        {
            if (isContainedItem == true && containedItem.itemAmount > 0)
            {
                //Debug.Log("into RefreshSlot_isContainedItem == true");
                amountText.text = "" + containedItem.itemAmount;
                itemImage.sprite = containedItem.item.itemSprite;
            }
            else if (isContainedItem == true && containedItem.itemAmount == 0)
            {
                //Debug.Log("into RefreshSlot_isContainedItem == false");                
                containedItem = InventorySaver.Instance.emptyItem;
                itemImage.sprite = containedItem.item.itemSprite;
                amountText.text = "";
                isContainedItem = false;
            }
            else if (isContainedItem == false)
            {
                containedItem = InventorySaver.Instance.emptyItem;
                amountText.text = "";
                itemImage.sprite = containedItem.item.itemSprite;
            }
        }
        else
        {
            containedItem = InventorySaver.Instance.emptyItem;
            isContainedItem = false;
            amountText.text = "";
            itemImage.sprite = containedItem.item.itemSprite;
        }
    }

    public void RefreshCombineSlotInfo()
    {
        if (containedItem != null)
        {
            if (isContainedItem == true)
            {
                //Debug.Log("into RefreshSlot_isContainedItem == true");
                amountText.text = "";
                itemImage.sprite = containedItem.item.itemSprite;
            }            
            else if (isContainedItem == false)
            {
                containedItem = InventorySaver.Instance.emptyItem;
                amountText.text = "";
                itemImage.sprite = containedItem.item.itemSprite;
            }
        }
        else
        {
            containedItem = InventorySaver.Instance.emptyItem;
            isContainedItem = false;
            amountText.text = "";
            itemImage.sprite = containedItem.item.itemSprite;
        }
    }
}
