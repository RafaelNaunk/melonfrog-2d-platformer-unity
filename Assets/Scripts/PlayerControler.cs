using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movimento")]
    public float velocidadeAndar = 5f;
    public float velocidadeCorrer = 8f;
    public float forcaPulo = 4f;          

    [Header("Pulo duplo")]
    public int maxPulos = 1;           
    private int pulosRestantes;

    [Header("Escalada")]
    public float velocidadeEscalada = 4f;

    [Header("Checagem de chão")]
    public Transform checagemChao;
    public float raioChecagem = 0.2f;
    public LayerMask camadaChao;

    private Rigidbody2D rb;
    private Animator anim;
    private bool noChao;
    private float inputHorizontal;
    private float inputVertical;
    private bool estaCorrendo;

    private bool podeEscalar;
    private bool escalando;
    private float gravidadeOriginal;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        gravidadeOriginal = rb.gravityScale;
    }

    void Update()
    {
        inputHorizontal = Input.GetAxisRaw("Horizontal");
        inputVertical = Input.GetAxisRaw("Vertical");
        estaCorrendo = Input.GetKey(KeyCode.LeftShift);

        noChao = Physics2D.OverlapCircle(checagemChao.position, raioChecagem, camadaChao);

        // recarrega os pulos quando toca o chão
        if (noChao && !escalando)
            pulosRestantes = maxPulos;

        if (podeEscalar && Mathf.Abs(inputVertical) > 0.1f)
            escalando = true;


        if (Input.GetKeyDown(KeyCode.Space) && (pulosRestantes > 0 || escalando))
        {
            if (!noChao && !escalando)
                anim.SetTrigger("doubleJump");

            escalando = false;
            rb.gravityScale = gravidadeOriginal;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, forcaPulo);
            pulosRestantes--;
        }

        if (inputHorizontal > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (inputHorizontal < 0)
            transform.localScale = new Vector3(-1, 1, 1);

        anim.SetFloat("velocidade", Mathf.Abs(rb.linearVelocity.x));
        anim.SetBool("noChao", noChao);
        anim.SetFloat("velocidadeY", rb.linearVelocity.y);   // novo: pra separar subir/cair
    }

    void FixedUpdate()
    {
        if (escalando)
        {
            rb.gravityScale = 0f;
            rb.linearVelocity = new Vector2(inputHorizontal * velocidadeAndar, inputVertical * velocidadeEscalada);
        }
        else
        {
            rb.gravityScale = gravidadeOriginal;
            float velocidade = estaCorrendo ? velocidadeCorrer : velocidadeAndar;
            rb.linearVelocity = new Vector2(inputHorizontal * velocidade, rb.linearVelocity.y);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Escada"))
            podeEscalar = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Escada"))
        {
            podeEscalar = false;
            escalando = false;
        }
    }

    void OnDrawGizmos()
    {
        if (checagemChao == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(checagemChao.position, raioChecagem);
    }
}