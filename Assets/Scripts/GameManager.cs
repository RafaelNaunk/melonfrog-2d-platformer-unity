using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instancia;

    [Header("Pontuação")]
    public int pontos = 0;
    public TMP_Text textoPontos;

    [Header("Vidas")]
    public int vidas = 3;
    public TMP_Text textoVidas;

    void Awake()
    {
        if (instancia == null)
            instancia = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        AtualizarHUD();
    }

    public void AdicionarPontos(int valor)
    {
        pontos += valor;
        AtualizarHUD();
    }

    // tira uma vida; retorna true se ainda está vivo, false se acabou
    public bool PerderVida()
    {
        vidas--;
        AtualizarHUD();

        if (vidas <= 0)
        {
            // game over: recarrega a fase atual
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            return false;
        }
        return true;
    }

    void AtualizarHUD()
    {
        if (textoPontos != null)
            textoPontos.text = "Frutas: " + pontos;
        if (textoVidas != null)
            textoVidas.text = "" + vidas;
    }
}