using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour
{
    [Header("Vida")]
    public int vidaMaxima = 5;        // quantos pisões aguenta
    private int vidaAtual;
    public Slider barraVida;          // a barra de vida na tela

    [Header("Patrulha")]
    public float velocidade = 2f;
    public float distancia = 4f;

    [Header("Feedback")]
    public AudioClip somDano;
    public AudioClip somMorte;

    [Header("Invencibilidade")]
    public float tempoInvencivel = 1f;     // duração da imunidade após levar dano
    private bool invencivel = false;       // boss imune logo após o pisão

    [Header("Música")]
    public AudioSource musicaVitoria;      // o Audio Source com a música de vitória
    public AudioSource musicaFundo;        // a música que estava tocando (do GameManager)

    private Vector3 pontoInicial;
    private bool indoDireita = false;
    private SpriteRenderer sr;

    void Start()
    {
        vidaAtual = vidaMaxima;
        pontoInicial = transform.position;
        sr = GetComponent<SpriteRenderer>();

        if (barraVida != null)
        {
            barraVida.maxValue = vidaMaxima;
            barraVida.value = vidaAtual;
        }
    }

    void Update()
    {
        if (morto) return;   

        // patrulha igual ao inimigo comum
        float dir = indoDireita ? 1f : -1f;
        transform.position += Vector3.right * dir * velocidade * Time.deltaTime;

        if (indoDireita && transform.position.x >= pontoInicial.x + distancia) Virar();
        else if (!indoDireita && transform.position.x <= pontoInicial.x - distancia) Virar();
    }

    void Virar()
    {
        indoDireita = !indoDireita;
        transform.localScale = new Vector3(indoDireita ? -Mathf.Abs(transform.localScale.x) : Mathf.Abs(transform.localScale.x), transform.localScale.y, 1);
    }


    void OnCollisionEnter2D(Collision2D col)
    {
        if (!col.gameObject.CompareTag("Player")) return;

        Rigidbody2D rbPlayer = col.gameObject.GetComponent<Rigidbody2D>();

        bool acima = col.transform.position.y > transform.position.y + 0.5f;
        bool descendo = rbPlayer != null && rbPlayer.linearVelocity.y < 0.5f;

        if (acima && descendo)
        {
            // só leva dano se NÃO estiver invencível
            if (!invencivel)
            {
                LevarDano();
                StartCoroutine(Invencibilidade());
            }
            // o quique acontece sempre (pra ele não grudar na cabeça)
            rbPlayer.linearVelocity = new Vector2(rbPlayer.linearVelocity.x, 14f);
        }
        else
        {
            PlayerVida vida = col.gameObject.GetComponent<PlayerVida>();
            if (vida != null) vida.LevarDano();
        }
    }

    System.Collections.IEnumerator Invencibilidade()
    {
        invencivel = true;
        yield return new WaitForSeconds(tempoInvencivel);
        invencivel = false;
    }

    void LevarDano()
    {
        vidaAtual--;

        if (barraVida != null)
            barraVida.value = vidaAtual;

        if (somDano != null)
            AudioSource.PlayClipAtPoint(somDano, transform.position);

        // pisca em vermelho como feedback
        if (sr != null)
            StartCoroutine(Piscar());

        if (vidaAtual <= 0)
            Morrer();
    }

    System.Collections.IEnumerator Piscar()
    {
        sr.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sr.color = Color.white;
    }

    private bool morto = false;

    void Morrer()
    {
        // troca a música
        if (musicaFundo != null)
            musicaFundo.Stop();            // para a música de fundo
        if (musicaVitoria != null)
            musicaVitoria.Play();          // toca a de vitória

        if (somMorte != null)
            AudioSource.PlayClipAtPoint(somMorte, transform.position);

        if (barraVida != null)
            barraVida.gameObject.SetActive(false);

        morto = true;
        if (sr != null) sr.enabled = false;
        GetComponent<Collider2D>().enabled = false;

        Invoke("CarregarProxima", 4.0f);
    }

    void CarregarProxima()
    {
        int proxima = SceneManager.GetActiveScene().buildIndex + 1;

        if (proxima < SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(proxima);
        else
            SceneManager.LoadScene("Vitoria");
    }
}