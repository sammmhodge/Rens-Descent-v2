using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{
    public GameObject _pausePanel, resumeButton;
    //public GameObject pauseFirstButton;
    public MovementInput mi;
    public bool mainmenuover = false;
    public GameObject ES;

    private void Update()
    {
        ES = GameObject.Find("EventSystem");
        resumeButton = GameObject.Find("resumeButton");
        if (ES.GetComponent<EventSystem>().firstSelectedGameObject != resumeButton && mainmenuover == true) ES.GetComponent<EventSystem>().firstSelectedGameObject = resumeButton;
    }
    public void resume()
    {
        mi.StartResume();
    }

    public void Quit()
    {
        Application.Quit();
    }
}
