using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Win : MonoBehaviour
{
    public List<GameObject> beginStorys;
    public int storyPlayedNumber;
    public Transform begin;
    public Transform end;
    public bool isButtonResealed;
    private void Start()
    {
        BeginPlayStory();
    }
    public void BeginPlayStory()
    {
        storyPlayedNumber = 0;
        beginStorys[0].SetActive(true);
        beginStorys[0].GetComponent<LerpMove>().BeginMove(begin, end);
        storyPlayedNumber++;
        StartCoroutine(OnLeftButtonClick());

    }
    public void PlayNextStory()
    {
        beginStorys[storyPlayedNumber].SetActive(true);
        beginStorys[storyPlayedNumber].GetComponent<LerpMove>().BeginMove(begin, end);
        storyPlayedNumber++;
        if (storyPlayedNumber < beginStorys.Count)
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
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0) && isButtonResealed == true);
        SceneManager.LoadScene("MainMenu");
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
