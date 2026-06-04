using UnityEngine;

public class CameraSegue : MonoBehaviour
{
    public Transform alvo;          // quem a câmera segue (o Player)
    public float suavidade = 5f;    // quão "mole" é o movimento
    public Vector3 deslocamento = new Vector3(0, 1, -10);  // offset; Z=-10 é essencial em 2D

    void LateUpdate()
    {
        if (alvo == null) return;

        // posição desejada = posição do player + deslocamento
        Vector3 destino = alvo.position + deslocamento;

        // move suavemente da posição atual até o destino
        transform.position = Vector3.Lerp(transform.position, destino, suavidade * Time.deltaTime);
    }
}