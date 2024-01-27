using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CombineController : MonoBehaviour
{
    public static CombineController Instance;

    public List<Formula> combineList;
    public List<Slot> combineSlots;
    public Slot resultSlot;
    public bool readyToCombine;
    public Formula combineFormula;

    private void Awake()
    {
        Instance = this;
    }
    public void TryToCombine()
    {
        
        string tempCombineString = "";
        foreach (var slot in combineSlots)
        {
            tempCombineString += slot.containedItem.item.itemName;
        }
        resultSlot.containedItem=InventorySaver.Instance.emptyItem;
        resultSlot.isContainedItem = false;
        combineFormula = null;
        foreach (var formula in combineList)
        {
            if (formula.combineString == tempCombineString)
            {
                combineFormula = formula;
                resultSlot.containedItem.item = formula.resultItem;
                resultSlot.isContainedItem = true;
                resultSlot.RefreshCombineSlotInfo();
                readyToCombine = true;
                return;
            }                
        }
        readyToCombine = false;
    }
    public void Combine()
    {
        if (readyToCombine)
        {
            foreach (var slot in combineSlots)
            {
                InventorySaver.Instance.inventoryItemList.Remove(InventorySaver.Instance.inventoryItemList.Find(x => (x.item == slot.containedItem.item && x.isInCombinePanle == true)));
                slot.isContainedItem = false;
                slot.RefreshCombineSlotInfo();
            }

            InventoryManager.Instance.AddItemToInventory(combineFormula.resultItem, 1);            
            resultSlot.containedItem = InventorySaver.Instance.emptyItem;            
            resultSlot.isContainedItem = false;
        }        
    }
    public void RefreshCombineSlots()
    {
        foreach (var slot in combineSlots)
        {
            slot.RefreshCombineSlotInfo();
        }
        resultSlot.RefreshCombineSlotInfo();
    }
}

    
[Serializable]
public class Formula
{
    public string combineString;
    public Item resultItem;
}
