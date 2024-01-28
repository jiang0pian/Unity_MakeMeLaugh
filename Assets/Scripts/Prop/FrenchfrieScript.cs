using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrenchfrieScript : Prop
{
    public override void UseProp()
    {
        PlayerController.Instance.isFrenchfrie = true;
        StartCoroutine(Frenchfrie());
    }

    public IEnumerator Frenchfrie()
    {
        yield return new WaitForSeconds(0.5f);
        PlayerController.Instance.isFrenchfrie = false;
    }
}
