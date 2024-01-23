using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagController : MonoBehaviour
{
    public static BagController Instance;

    public GameObject Bag;
    public bool isBagOpen;

    private void Awake()
    {
        Instance = this;
    }
    public void OpenAndCloseBag()
    {
        if (Bag.activeSelf == false)
        {
            PlayerController.Instance.SetGameState(GameState.Pause);
            isBagOpen = true;
            Bag.SetActive(true);
            InventoryManager.Instance.RefreshItemInBag();
        }
        else
        {
            isBagOpen = false;
            Bag.SetActive(false);            
        }
    }
}
