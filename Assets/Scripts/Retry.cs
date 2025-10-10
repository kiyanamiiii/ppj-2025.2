using UnityEngine;
using UnityEngine.SceneManagement; // Importante para recarregar a cena

public class RetryButton : MonoBehaviour
{
    /// <summary>
    /// Recarrega a cena atualmente ativa.
    /// Esta fun��o deve ser chamada quando o bot�o for clicado.
    /// </summary>
    public void RestartCurrentScene()
    {
        // Pega o �ndice da cena atualmente carregada
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Recarrega a cena usando o seu �ndice
        SceneManager.LoadScene(currentSceneIndex);
    }
}