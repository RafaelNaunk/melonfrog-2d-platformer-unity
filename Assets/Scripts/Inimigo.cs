using UnityEngine;

public class Inimigo : MonoBehaviour
{
    [Header("Patrulha")]
    public float velocidade = 2f;
    public float distancia = 3f;

    [Header("Detecção de borda (chão à frente)")]
    public Transform sensorFrente;     // ponto à frente/abaixo dos pés
    public float raioSensor = 0.2f;
    public LayerMask camadaChao;

    [Header("Detecção de parede")]
    public Transform sensorParede;     // ponto à frente, na altura do corpo
    public float distanciaParede = 0.3f;

    private Vector3 pontoInicial;
    private bool indoDireita = false;  // começa indo pra esquerda

    void Start()
    {
        pontoInicial = transform.position;
        transform.localScale = new Vector3(indoDireita ? -1 : 1, 1, 1);
    }

    void Update()
    {
        float dir = indoDireita ? 1f : -1f;
        transform.position += Vector3.right * dir * velocidade * Time.deltaTime;

        // limite da patrulha
        if (indoDireita && transform.position.x >= pontoInicial.x + distancia) Virar();
        else if (!indoDireita && transform.position.x <= pontoInicial.x - distancia) Virar();

        // sem chão à frente (beirada)? vira
        if (sensorFrente != null)
        {
            bool temChaoFrente = Physics2D.OverlapCircle(sensorFrente.position, raioSensor, camadaChao);
            if (!temChaoFrente) Virar();
        }

        // tem parede à frente (montanha/bloco)? vira
        if (sensorParede != null)
        {
            bool temParede = Physics2D.OverlapCircle(sensorParede.position, raioSensor, camadaChao);
            if (temParede) Virar();
        }
    }

    void Virar()
    {
        indoDireita = !indoDireita;
        transform.localScale = new Vector3(indoDireita ? -1 : 1, 1, 1);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (!col.gameObject.CompareTag("Player")) return;

        Rigidbody2D rbPlayer = col.gameObject.GetComponent<Rigidbody2D>();

        if (rbPlayer != null && rbPlayer.linearVelocity.y < -0.1f)
        {
            Destroy(gameObject);
            rbPlayer.linearVelocity = new Vector2(rbPlayer.linearVelocity.x, 10f);
        }
        else
        {
            PlayerVida vida = col.gameObject.GetComponent<PlayerVida>();
            if (vida != null) vida.LevarDano();
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        if (sensorFrente != null) Gizmos.DrawWireSphere(sensorFrente.position, raioSensor);
        Gizmos.color = Color.cyan;
        if (sensorParede != null) Gizmos.DrawWireSphere(sensorParede.position, raioSensor);
    }
}