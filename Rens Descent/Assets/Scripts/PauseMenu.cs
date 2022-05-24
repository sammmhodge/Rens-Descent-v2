using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{
    public GameObject _pausePanel;
    //public GameObject pauseFirstButton;
    public MovementInput mi;

    public void resume()
    {
        mi.StartResume();
    }

    public void Quit()
    {
        Application.Quit();
    }
}
