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
            UseProp(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && PropSlots[2].isContainedItem == true)
        {
            UseProp(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4) && PropSlots[3].isContainedItem == true)
        {
            UseProp(3);
        }
        if (Input.GetKeyDown(KeyCode.Q) && PropSlots[4].isContainedItem == true)
        {
            UseProp(4);
        }
        if (Input.GetKeyDown(KeyCode.E) && PropSlots[5].isContainedItem == true)
        {
            UseProp(5);
        }
        if (Input.GetKeyDown(KeyCode.R) && PropSlots[6].isContainedItem == true)
        {
            UseProp(6);
        }
        if (Input.GetKeyDown(KeyCode.F) && PropSlots[7].isContainedItem == true)
        {
            UseProp(7);
        }
    }
    public void UseProp(int index)
    {
        GameObject tempGameObject = Instantiate(PropSlots[index].containedItem.item.itemPrefab, clutterManager.transform);
        tempGameObject.GetComponent<Prop>().UseProp();
        InventoryManager.Instance.ReduceItemInInventory(PropSlots[index].containedItem.item, 1);
        StartCoroutine(DestoryThisObject(tempGameObject));
        if (clutterManager.transform.childCount >= 10)
        {
            Destroy(clutterManager.transform.GetChild(0).gameObject);           
        }
    }
    public IEnumerator DestoryThisObject(GameObject gameObject)
    {
        yield return new WaitForSeconds(30f);
        Destroy(gameObject);
    } 
}
