using UnityEngine;

public class Coletavel : MonoBehaviour
{
    public int valor = 1;              // quanto vale essa fruta
    public AudioClip somColeta;        // efeito sonoro ao pegar

    void OnTriggerEnter2D(Collider2D other)
    {
        // só reage se quem encostou for o Player
        if (other.CompareTag("Player"))
        {
            // soma os pontos no GameManager
            GameManager.instancia.AdicionarPontos(valor);

            // feedback sonoro: toca o som na posição da fruta
            if (somColeta != null)
                AudioSource.PlayClipAtPoint(somColeta, transform.position);

            // feedback visual: a fruta some
            Destroy(gameObject);
        }
    }
}