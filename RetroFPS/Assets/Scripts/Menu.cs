using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using RetroFPS.Assets.Scripts;
using UnityEngine.Video;

public class Menu : MonoBehaviour
{
    public Canvas menuQuit;
    public Button buttonStart;
    public Button buttonExit;

    private Canvas canvasMenu;//obiekt menu caly
    public Canvas canvas;//hud nazwy takie jaak w unity

    //"skrypty" do zablokowania, gdy wyswietlamy menu
    public Transform ActorEyesCamera;



    // Use this for initialization
    void Start()
    {
        //pobieram obiekty
        canvasMenu = (Canvas)GetComponent<Canvas>();//pobieram menu tam jest caly skrypt podpiety
        menuQuit = menuQuit.GetComponent<Canvas>();//pobieranie menu - pytanie o wyjscied
        canvas = canvas.GetComponent<Canvas>();//pobranie menu hud gracza

        buttonStart = buttonStart.GetComponent<Button>();
        buttonExit = buttonExit.GetComponent<Button>();

        menuQuit.enabled = false; //wylaczenie sceny wyjscia, na starcie to jest niewidoczne
        canvas.enabled = false;//wyl interfejsu uzytkownikaq

        ActorEyesCamera.GetComponent<camMouseLook>().enabled = false;
        Time.timeScale = 0;//zatrzymanie czasu- gra zatrzymana
        Cursor.visible=canvasMenu.enabled; //kursor widoczny
        Cursor.lockState = CursorLockMode.Locked;//odblokowanie kursora myszy
    }

    //Update is called once per frame
    //void Update()
    //{
    //    if (Input.GetKeyUp(KeyCode.Escape))
    //    {
    //        canvasMenu.enabled = !canvasMenu.enabled;//ukrycie/pokazanie menu
    //        canvas.enabled = !canvas.enabled;//ukrycie/pokazanie huda
    //        Cursor.visible = canvasMenu.enabled;// ukrycie/pokazanie kursora

    //        if (canvasMenu.enabled)
    //        {

    //            ActorEyesCamera.GetComponent<camMouseLook>().enabled = false;
    //            Cursor.lockState = CursorLockMode.Confined;
    //            Cursor.visible = true;
    //            Time.timeScale = 0;
    //        }
    //        else
    //        {

    //            ActorEyesCamera.GetComponent<camMouseLook>().enabled = true;
    //            Cursor.lockState = CursorLockMode.Locked;//zabl kursora
    //            Cursor.visible = false;
    //            Time.timeScale = 1;
    //        }
    //    }

    //}
    //metody wywwolywana przy nacisnieciu przycisku exit
    public void Exit()
    {
     
        ActorEyesCamera.GetComponent<camMouseLook>().enabled = false;
        menuQuit.enabled = true;
        buttonStart.enabled = false;
        buttonExit.enabled = false;
    }
    //metoda wywwolywana przy odpowiedzi nie wychodz
    public void NoExit()
    {
     
        ActorEyesCamera.GetComponent<camMouseLook>().enabled = false;

        menuQuit.enabled = false;
        buttonStart.enabled = true;
        buttonExit.enabled = true;
    }
    public void YesExit()
    {
        Application.Quit();
    }
    //metoda wywolywana przez przycisk uruchomienia  gry
    public void StartGame()
    {
        ActorEyesCamera.GetComponent<camMouseLook>().enabled = true;
        canvasMenu.enabled = false;//ukrycie     menu glownego
        canvas.enabled = true;
        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        buttonStart.enabled = true;
    }

}
