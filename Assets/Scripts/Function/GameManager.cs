using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool isLoose;
    public bool isWin;

    private void Update()
    {
        if (isLoose)
        {
            Loose();
        }
        if (isWin)
        {
            Win();
        }
    }
    public void Loose()
    {
        
    }
    public void Win()
    {

    }
}
