using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class InventorySaver : MonoBehaviour
{
    public static InventorySaver Instance;
    
    public List<BagItem> inventoryItemList;
    public BagItem emptyItem;    
    public Slot TempSlot;//用作存储slot信息

    public List<Item> PropList;

    private void Awake()
    {
        Instance = this;
    }    
    public BagItem ChooseOneItemNotInBag(Slot slot)
    {
        for (int i = 0; i < inventoryItemList.Count; i++)
        {
            if (inventoryItemList[i].isInBag == false)
            {
                inventoryItemList[i].isInBag = true;
                slot.isContainedItem = true;
                return inventoryItemList[i];
            }
        }
        return InventorySaver.Instance.emptyItem;
    }
}
[Serializable]
public class BagItem
{
    public Item item;
    public int itemAmount;
    public bool isInBag = false;
    public bool isInCombinePanle = false;
    public int propIndex;//0为不在prop，1-8为所在prop栏的序号

    public BagItem(Item itemToAdd, int amount)
    {
        this.item = itemToAdd;
        this.itemAmount = amount;
    }
}
