using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swordshield : Prop
{
    public override void UseProp()
    {
        PlayerController.Instance.isFrenchfrie = true;
        PlayerController.Instance.isShiled = true;
    }
}
