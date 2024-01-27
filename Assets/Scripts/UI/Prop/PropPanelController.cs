using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropPanelController : MonoBehaviour
{
    public static PropPanelController Instance;
    public List<Slot> PropSlots;
    private void Awake()
    {
        Instance = this;
    }

    public void RefreshAllPropSlot()
    {
        foreach(var slot in PropSlots)
        {
            slot.RefreshBagSlotInfo();
        }
    }
}
