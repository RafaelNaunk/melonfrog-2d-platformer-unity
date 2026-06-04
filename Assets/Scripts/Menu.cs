using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void Jogar()
    {
        SceneManager.LoadScene("Nivel1");
    }

    public void VoltarMenu()
    {
        SceneManager.LoadScene("MenuInicial");
    }

    public void Sair()
    {
        Application.Quit();
    }
}