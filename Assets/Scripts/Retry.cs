using UnityEngine;
using UnityEngine.SceneManagement; // Importante para recarregar a cena

public class RetryButton : MonoBehaviour
{
    /// <summary>
    /// Recarrega a cena atualmente ativa.
    /// Esta função deve ser chamada quando o botão for clicado.
    /// </summary>
    public void RestartCurrentScene()
    {
        // Pega o índice da cena atualmente carregada
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Recarrega a cena usando o seu índice
        SceneManager.LoadScene(currentSceneIndex);
    }
}