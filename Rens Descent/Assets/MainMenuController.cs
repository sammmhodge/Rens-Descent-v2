using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class MainMenuController : MonoBehaviour
{
    public GameObject Ren, startAnimation, titleCard, scorePanel, pauseMenu, resumeButton;
    public GameObject[] buttons;
    bool mainMenuOver = false;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0f;
        scorePanel.SetActive(false);
        startAnimation.SetActive(false);
        titleCard.SetActive(true);
        Ren.SetActive(false);
        for (int i = 0; i < buttons.Length; i++) buttons[i].SetActive(true);

    }

    private void Update()
    {
        
    }

    public void play()
    {
        titleCard.SetActive(false);
        for (int i = 0; i < buttons.Length; i++) buttons[i].SetActive(false);
        startAnimation.SetActive(true);
        StartCoroutine(startPlayer());
        mainMenuOver = true;
    }

    IEnumerator startPlayer()
    {
        yield return new WaitForSecondsRealtime(2.5f);
        Ren.SetActive(true);
        scorePanel.SetActive(true);
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }

    public void quit()
    {
        Application.Quit();
    }
}
