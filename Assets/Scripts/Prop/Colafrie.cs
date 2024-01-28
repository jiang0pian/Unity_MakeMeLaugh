using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colafrie : Prop
{
    public override void UseProp()
    {
        PlayerController.Instance.isColafrie = true;
        StartCoroutine(Frenchfrie());
    }

    public IEnumerator Frenchfrie()
    {
        yield return new WaitForSeconds(0.5f);
        PlayerController.Instance.isColafrie = false;
    }
}
