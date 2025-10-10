using UnityEngine;

public class Collectable : MonoBehaviour
{
    public int valor = 1; // Quantos pontos esse item vale
    private Placar placar; // Referência ao script de pontuação

    void Start()
    {
        // Procura automaticamente o script Placar na cena
        placar = FindObjectOfType<Placar>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica se quem encostou foi o jogador
        if (collision.CompareTag("Player"))
        {
            if (placar != null)
            {
                placar.AdicionarPontos(valor);
            }

            // Destroi o item
            Destroy(gameObject);
        }
    }
}
