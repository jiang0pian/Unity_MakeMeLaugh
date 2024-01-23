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
                amountText.text = "";
                itemImage.sprite = containedItem.item.itemSprite;
                containedItem = InventorySaver.Instance.emptyItem;
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
            if (isContainedItem == true && containedItem.itemAmount > 0)
            {
                //Debug.Log("into RefreshSlot_isContainedItem == true");
                amountText.text = "";
                itemImage.sprite = containedItem.item.itemSprite;
            }
            else if (isContainedItem == true && containedItem.itemAmount == 0)
            {
                //Debug.Log("into RefreshSlot_isContainedItem == false");
                amountText.text = "";
                containedItem = InventorySaver.Instance.emptyItem;
                itemImage.sprite = containedItem.item.itemSprite;                
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
}
