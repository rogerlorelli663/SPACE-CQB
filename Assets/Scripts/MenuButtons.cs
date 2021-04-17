using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtons : MonoBehaviour
{
    // Start is called before the first frame update
    // Update is called once per frame
    private Buttons MainMenuButton;
    private Buttons TutorialButton;
    private Buttons QuitButton;
    private bool pressed;

    private void Start()
    {
        MainMenuButton = GameObject.Find("MainMenu Button").GetComponent<Buttons>();
        TutorialButton = GameObject.Find("Tutorial Button").GetComponent<Buttons>();
        QuitButton = GameObject.Find("Quit Button").GetComponent<Buttons>();
        pressed = false;
        TurnButtonsOff();
    }

    private void TurnButtonsOff()
    {
        MainMenuButton.TurnOff();
        TutorialButton.TurnOff();
        QuitButton.TurnOff();
    }

    private void TurnButtonsOn()
    {
        MainMenuButton.TurnOn();
        TutorialButton.TurnOn();
        QuitButton.TurnOn();
    } 
    public void OnPress()
    {
        if(pressed)
        {
            TurnButtonsOff();
        }
        else
        {
            TurnButtonsOn();
        }
        pressed = !pressed;
    }

    public void Reset()
    {
        TurnButtonsOff();
    }
}
