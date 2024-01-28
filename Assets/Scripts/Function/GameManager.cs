using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool isLoose;
    public bool isWin;
    private void Awake()
    {
        Instance = this;
        isLoose=false;
        isWin=false;
}

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
        SceneManager.LoadScene("Loose");
    }
    public void Win()
    {
        SFXManger.Instance.PlaySFX(SFX.Victory);
        SceneManager.LoadScene("Win");
    }
}
