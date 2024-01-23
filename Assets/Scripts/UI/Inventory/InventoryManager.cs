using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    private void Awake()
    {
        Instance = this;
    }

    public GameObject bagGrid;
    public GameObject bagSlotPrefab;
    public int slotAmout;
    public List<GameObject> slotLists;

    public TMP_Text itemIntroduction;

    public void AddItemToInventory(Item itemToAdd, int amountToAdd)
    {
        if (itemToAdd.isStackable == true)
        {
            if (InventorySaver.Instance.inventoryItemList.Exists(x => x.item == itemToAdd) == false)
            {
                InventorySaver.Instance.inventoryItemList.Add(new BagItem(itemToAdd, amountToAdd));
            }
            else
            {
                InventorySaver.Instance.inventoryItemList.Find(x => x.item == itemToAdd).itemAmount += amountToAdd;
            }
        }
        else
        {
            for (int i = 0; i < amountToAdd; i++)
            {
                InventorySaver.Instance.inventoryItemList.Add(new BagItem(itemToAdd, 1));
            }
        }
        RefreshBagSlotAmount();
    }
    public void ReduceItemInInventory(Item itemToReduce, int amountToReduce)
    {
        if (InventorySaver.Instance.inventoryItemList.Exists(x => x.item == itemToReduce) == true && InventorySaver.Instance.inventoryItemList.Find(x => x.item == itemToReduce).itemAmount - amountToReduce >= 0)
        {
            InventorySaver.Instance.inventoryItemList.Find(x => x.item == itemToReduce).itemAmount -= amountToReduce;
            if (InventorySaver.Instance.inventoryItemList.Find(x => x.item == itemToReduce).itemAmount == 0)
            {
                InventorySaver.Instance.inventoryItemList.Remove(InventorySaver.Instance.inventoryItemList.Find(x => x.item == itemToReduce));

            }
        }
        else
        {
            Debug.Log(itemToReduce + "Item amount is not enough to reduce");
        }
    }
    public void RefreshBagSlotAmount()
    {
        if (bagGrid.transform.childCount == slotAmout)
        {
            return;
        }
        if (bagGrid.transform.childCount < slotAmout)
        {
            while(slotLists.Count < slotAmout)
            {
                slotLists.Add(Instantiate(bagSlotPrefab, bagGrid.transform));
            }
        }
        if (InventorySaver.Instance.inventoryItemList.Count > slotLists.Count)
        {
            slotLists.Add(Instantiate(bagSlotPrefab, bagGrid.transform));
        }
    }

    public void RefreshItemIntroduction(GameObject slotInChoose)
    {
        if (slotInChoose.GetComponent<Slot>().isContainedItem == true)
        {
            itemIntroduction.text = slotInChoose.GetComponent<Slot>().containedItem.item.itemName + "\n" + slotInChoose.GetComponent<Slot>().containedItem.item.itemInfo;
        }
    }

    public void RefreshItemInBag()
    {
        for (int i = 0; i < slotAmout; i++)
        {
            if (bagGrid.transform.childCount == 0)
            {
                break;
            }
            if (slotLists[i].GetComponent<Slot>().isContainedItem == false)
            {
                slotLists[i].GetComponent<Slot>().containedItem = InventorySaver.Instance.ChooseOneItemNotInBag(slotLists[i].GetComponent<Slot>());
            }
            slotLists[i].GetComponent<Slot>().RefreshBagSlotInfo();
        }
    }
}
