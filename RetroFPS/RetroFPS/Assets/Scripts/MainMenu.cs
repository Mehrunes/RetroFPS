using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    public enum menuStates { menu, menuOpcion, menuQuit}
    public menuStates currentState;

    public GameObject Menu, MenuOpcion, MenuQuit;

    // Use this for initialization
    void Awake()
    {
        //menu pojawiajace sie jako pierwsze
        currentState = menuStates.menu;

    }
  
    void Update()
    {
        //przelaczamy stany
        switch(currentState)
        {
            case menuStates.menu:
                Menu.SetActive(true);
                MenuOpcion.SetActive(false);
                MenuQuit.SetActive(false);
                break;
            case menuStates.menuOpcion:
                Menu.SetActive(false);
                MenuOpcion.SetActive(true);
                MenuQuit.SetActive(false);
                break;
            case menuStates.menuQuit:
                Menu.SetActive(false);
                MenuOpcion.SetActive(false);
                MenuQuit.SetActive(true);
                break;
        }

    }


    //metody wywwolywana przy nacisnieciu przycisku exit
    public void Exit()
    {
        currentState = menuStates.menuQuit;
    }
    //metoda wywwolywana przy odpowiedzi nie wychodz
    public void NoExit()
    {
        currentState = menuStates.menu;
    }
    public void YesExit()
    {
        Application.Quit();
    }
    //metoda wywolywana przez przycisk uruchomienia  gry
    public void StartGame()
    {
        SceneManager.LoadScene("Scena testowa 1");
        SceneManager.UnloadScene("Menu glowne");
    }
    public void OptionGame()
    {
        currentState = menuStates.menuOpcion;
    }
    public void CanelFromOption()
    {
        currentState = menuStates.menu;

    }
}
