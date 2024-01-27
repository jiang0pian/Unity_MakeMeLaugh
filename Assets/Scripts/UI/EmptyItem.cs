using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyItem : MonoBehaviour
{
    public static EmptyItem Instance;
    public BagItem emptyBagItem;
    public Item emptyItem;

    private void Awake()
    {
        Instance = this;
        emptyItem = GetComponent<Item>();
    }
    private void Update()
    {
        emptyBagItem.item = emptyItem;
    }
}
