using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public string itemName;
    public Sprite itemSprite;
    public string itemType;
    public GameObject itemPrefab;
    public bool isStackable;
    [TextArea()]
    public string itemInfo;
}
