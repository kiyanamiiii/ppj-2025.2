using System.Collections;
using UnityEngine;

public class EnemyCollectible : MonoBehaviour
{
    [Header("Arraste aqui o OBJETO do Canvas (GameObject)")]
    [SerializeField] private GameObject gameOverCanvas;

    [Header("Tempo de atraso antes do Game Over")]
    [SerializeField] private float delayAntesGameOver = 0.5f;

    private bool gameOverAtivado = false;
    private SpriteRenderer spriteRenderer;
    private Collider2D colisor;

    private void Start()
    {
        if (gameOverCanvas != null)
            gameOverCanvas.SetActive(false);

        spriteRenderer = GetComponent<SpriteRenderer>();
        colisor = GetComponent<Collider2D>();

        Time.timeScale = 1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !gameOverAtivado)
        {
            gameOverAtivado = true;

            // Desativa visual e colisão, mas não destrói ainda
            if (spriteRenderer != null)
                spriteRenderer.enabled = false;
            if (colisor != null)
                colisor.enabled = false;

            // Continua a coroutine normalmente
            StartCoroutine(AtivarGameOver());
        }
    }

    private IEnumerator AtivarGameOver()
    {
        yield return new WaitForSeconds(delayAntesGameOver);

        if (gameOverCanvas != null)
            gameOverCanvas.SetActive(true);

        Time.timeScale = 0;

        // Destroi o inimigo após ativar o Game Over
        Destroy(gameObject);
    }
}
