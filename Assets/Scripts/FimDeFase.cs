using UnityEngine;
using UnityEngine.SceneManagement;

public class FimDeFase : MonoBehaviour
{
    public AudioClip somVitoria;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        if (somVitoria != null)
            AudioSource.PlayClipAtPoint(somVitoria, transform.position);

        // descobre o índice da próxima cena
        int proxima = SceneManager.GetActiveScene().buildIndex + 1;

        // se existe próxima cena na lista, carrega; senão, vai pra tela de vitória
        if (proxima < SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(proxima);
        else
            SceneManager.LoadScene("Vitoria");   // tela final (criamos já já)
    }
}