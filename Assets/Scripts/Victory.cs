using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryManager : MonoBehaviour
{
    [Header("Referências")]
    [SerializeField] private GameObject victoryCanvas; // Canvas de vitória
    [SerializeField] private Placar placar;            // Script de pontuação

    [Header("Condição de Vitória")]
    [SerializeField] private int pontosParaVitoria = 50;

    private bool vitoriaAtivada = false;

    private void Start()
    {
        if (victoryCanvas != null)
            victoryCanvas.SetActive(false);

        if (placar == null)
            placar = FindObjectOfType<Placar>();

        Time.timeScale = 1;
    }

    private void Update()
    {
        if (vitoriaAtivada || placar == null)
            return;

        if (placar.Pontos >= pontosParaVitoria)
            AtivarVitoria();
    }

    private void AtivarVitoria()
    {
        vitoriaAtivada = true;

        if (victoryCanvas != null)
            victoryCanvas.SetActive(true);

        Time.timeScale = 0;
    }

    // Botão opcional para reiniciar a cena
    public void ReiniciarJogo()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
