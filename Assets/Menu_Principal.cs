using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MenuPrincipal : MonoBehaviour
{
    public GameObject botonJugar;

    void Start()
    {
        EventSystem.current.SetSelectedGameObject(botonJugar);
    }
    public void Jugar()
    {
        SceneManager.LoadScene("Juego");
    }

    public void Salir()
    {
        Application.Quit();
    }

    //ya me cansé de intentar que el botón funcione, quedará de adorno !!!!!!!!!!!!
     void Update()
    {
        if (Input.anyKeyDown)
        {
            Jugar();
        }
    }
}

