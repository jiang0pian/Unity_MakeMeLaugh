using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropPanelController : MonoBehaviour
{
    public static PropPanelController Instance;
    public List<Slot> PropSlots;
    public GameObject clutterManager;
    private void Awake()
    {
        Instance = this;
    }
    private void Update()
    {
        getKeyDown();
    }

    public void RefreshAllPropSlot()
    {
        foreach(var slot in PropSlots)
        {
            slot.RefreshBagSlotInfo();
        }
    }

    public void getKeyDown()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && PropSlots[0].isContainedItem==true)
        {
            UseProp(0);            
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && PropSlots[1].isContainedItem == true)
        {
            PropSlots[1].GetComponent<Prop>().UseProp();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && PropSlots[2].isContainedItem == true)
        {
            PropSlots[2].GetComponent<Prop>().UseProp();
        }
        if (Input.GetKeyDown(KeyCode.Alpha4) && PropSlots[3].isContainedItem == true)
        {
            PropSlots[3].GetComponent<Prop>().UseProp();
        }
        if (Input.GetKeyDown(KeyCode.Q) && PropSlots[4].isContainedItem == true)
        {
            PropSlots[4].GetComponent<Prop>().UseProp();
        }
        if (Input.GetKeyDown(KeyCode.E) && PropSlots[5].isContainedItem == true)
        {
            PropSlots[5].GetComponent<Prop>().UseProp();
        }
        if (Input.GetKeyDown(KeyCode.R) && PropSlots[6].isContainedItem == true)
        {
            PropSlots[6].GetComponent<Prop>().UseProp();
        }
        if (Input.GetKeyDown(KeyCode.F) && PropSlots[7].isContainedItem == true)
        {
            PropSlots[7].GetComponent<Prop>().UseProp();
        }
    }
    public void UseProp(int index)
    {
        GameObject tempGameObject = Instantiate(PropSlots[index].containedItem.item.itemPrefab, clutterManager.transform);
        tempGameObject.GetComponent<Prop>().UseProp();
        InventoryManager.Instance.ReduceItemInInventory(tempGameObject.GetComponent<Item>(), 1);
        StartCoroutine(DestoryThisObject(tempGameObject));
    }
    public IEnumerator DestoryThisObject(GameObject gameObject)
    {
        yield return new WaitForSeconds(30f);
        Destroy(gameObject);
    } 
}
