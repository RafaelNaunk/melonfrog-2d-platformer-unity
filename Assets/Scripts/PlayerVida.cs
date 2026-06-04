using UnityEngine;

public class PlayerVida : MonoBehaviour
{
    public AudioClip somDano;
    public float limiteQueda = -10f;   // se cair abaixo disso, perde vida
    private Vector3 posicaoInicial;

    void Start()
    {
        posicaoInicial = transform.position;   // guarda onde nasceu
    }

    void Update()
    {
        // caiu no vazio? perde vida
        if (transform.position.y < limiteQueda)
            LevarDano();
    }

    public void LevarDano()
    {
        if (somDano != null)
            AudioSource.PlayClipAtPoint(somDano, transform.position);

        bool vivo = GameManager.instancia.PerderVida();
        if (vivo)
            transform.position = posicaoInicial;   // volta pro início da fase
    }
}