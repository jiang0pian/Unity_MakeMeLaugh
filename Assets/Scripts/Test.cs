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
            GameManager.Instance.isWin = true;
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            GameManager.Instance.isLoose = true;
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            InventoryManager.Instance.GetRadomProp(3);
        }
        InventoryManager.Instance.RefreshItemInBag();
        InventoryManager.Instance.RefreshBagSlotAmount();
        CombineController.Instance.TryToCombine();
        CombineController.Instance.RefreshCombineSlots();
        PropPanelController.Instance.RefreshAllPropSlot();
    }
}
