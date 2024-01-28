using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureChest : MonoBehaviour
{
    public int radomAwardAmount;
    public List<int> appointAwardAmountList;
    public bool isGetRadom;

    public void GetRadomAward()
    {
        InventoryManager.Instance.GetRadomProp(radomAwardAmount);
    }
    public void GetAppointAward()
    {
        for(int i = 0; i < appointAwardAmountList.Count; i++)
        {
            InventoryManager.Instance.GetProp(appointAwardAmountList[i], i);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (isGetRadom)
            {
                GetRadomAward();
            }
            else
            {
                GetAppointAward();
            }
            Destroy(this.gameObject);
            SFXManger.Instance.PlaySFXPiched(SFX.openAward, 0.9f, 1.1f);
        }
    }

}
