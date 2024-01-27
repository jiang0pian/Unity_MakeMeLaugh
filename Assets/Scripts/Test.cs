using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public Item testItem1;
    public Item testItem2;
    public Item testItem3;

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.O))
        {
            InventoryManager.Instance.AddItemToInventory(testItem1, 1);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            InventoryManager.Instance.AddItemToInventory(testItem2, 1);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            InventoryManager.Instance.AddItemToInventory(testItem3, 1);
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            InventoryManager.Instance.ReduceItemInInventory(testItem1, 1);
        }
        InventoryManager.Instance.RefreshItemInBag();
        InventoryManager.Instance.RefreshBagSlotAmount();
        CombineController.Instance.TryToCombine();
        CombineController.Instance.RefreshCombineSlots();
        PropPanelController.Instance.RefreshAllPropSlot();
    }
}
