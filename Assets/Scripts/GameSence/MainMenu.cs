using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string mainGameScene;
    public static MainMenu Instance;
    public List<GameObject> storys;
    public int storyPlayedNumber;
    public Transform begin;
    public Transform end;
    public bool isButtonResealed;

    private void Awake()
    {
        Instance = this;
    }
    public void StartGame()
    {
        BeginPlayStory();
        
    }
    public void QuietGame()
    {
        if (Application.isEditor)
        {
            //UnityEditor.EditorApplication.isPlaying = false;
        }
        else
        {
            Application.Quit();
        }
    }
    public void BeginPlayStory()
    {
        storyPlayedNumber = 0;
        storys[0].SetActive(true);
        storys[0].GetComponent<LerpMove>().BeginMove(begin, end);
        storyPlayedNumber++;
        StartCoroutine(OnLeftButtonClick());

    }
    public void PlayNextStory()
    {        
        storys[storyPlayedNumber].SetActive(true);
        storys[storyPlayedNumber].GetComponent<LerpMove>().BeginMove(begin, end);
        storyPlayedNumber++;
        if (storyPlayedNumber < storys.Count)
        {
            StartCoroutine(OnLeftButtonClick());
        }
        else
        {
            StartCoroutine(StoryEnd());
        }
    }   
    public IEnumerator StoryEnd()
    {
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0) && isButtonResealed==true);
        SceneManager.LoadScene(mainGameScene);
    }
    public IEnumerator OnLeftButtonClick()
    {
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0) && isButtonResealed == true);
        isButtonResealed = false;
        PlayNextStory();
    }
    public void GetButtonUpDown()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    isButtonResealed = false;
        //}
        if (Input.GetMouseButtonUp(0))
        {
            isButtonResealed = true;
        }
    }
    private void Update()
    {
        GetButtonUpDown();
    }
}
