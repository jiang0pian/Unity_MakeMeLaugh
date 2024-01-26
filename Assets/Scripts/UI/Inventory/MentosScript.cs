using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MentosScript : Prop
{
    public static MentosScript Instance;
    public float addSpeed = 800f;
    public float durationTime = 2f;

    private void Start()
    {
        Instance = this;
    }

    public override void UseProp()
    {
        StartCoroutine(SpeedUp());
    }

    public IEnumerator SpeedUp()
    {
        PlayerAttribute.Instance.moveSpeed += addSpeed;
        yield return new WaitForSeconds(durationTime);
        PlayerAttribute.Instance.moveSpeed -= addSpeed;
    }

}
