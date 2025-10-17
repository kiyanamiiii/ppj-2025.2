using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject[] platformPrefabs;
    public GameObject[] collectiblePrefabs;

    [Header("Configuração de spawn")]
    [Range(0, 100)] public int collectibleSpawnChance = 50;
    public float spawnMin = 1f;
    public float spawnMax = 2f;
    public float spawnHeightOffset = 1f;
    public int maxSpawnAttempts = 5; // Máximo de tentativas antes de desistir

    private float nextYPosition;

    void Start()
    {
        nextYPosition = transform.position.y;
        Spawn();
    }

    void Spawn()
    {
        // --- 1. Escolhe plataforma ---
        GameObject platformPrefab = platformPrefabs[Random.Range(0, platformPrefabs.Length)];

        // Calcula posição base
        Vector3 spawnPos = new Vector3(
            transform.position.x,
            nextYPosition,
            transform.position.z
        );

        // --- 2. Tenta achar posição livre ---
        bool encontrouPosicao = false;
        int tentativas = 0;

        Renderer platformRenderer = platformPrefab.GetComponent<Renderer>();
        float alturaPlataforma = platformRenderer != null ? platformRenderer.bounds.size.y : 1f;

        while (!encontrouPosicao && tentativas < maxSpawnAttempts)
        {
            Collider2D colisao = Physics2D.OverlapBox(
                spawnPos,
                new Vector2(3f, alturaPlataforma * 1.2f), // área de checagem
                0f
            );

            if (colisao == null)
            {
                encontrouPosicao = true;
                break;
            }

            // Caso esteja colidindo, move um pouco pra cima e tenta de novo
            spawnPos.y += alturaPlataforma + spawnHeightOffset;
            tentativas++;
        }

        if (!encontrouPosicao)
        {
            Debug.LogWarning("Spawner não encontrou posição livre. Pulando spawn desta plataforma.");
            Invoke("Spawn", Random.Range(spawnMin, spawnMax));
            return;
        }

        // --- 3. Instancia plataforma ---
        GameObject novaPlataforma = Instantiate(platformPrefab, spawnPos, Quaternion.identity);

        // Atualiza próxima posição base
        nextYPosition = spawnPos.y + alturaPlataforma + spawnHeightOffset;

        // --- 4. Possível coletável ---
        if (collectiblePrefabs.Length > 0 && Random.Range(0, 100) < collectibleSpawnChance)
        {
            GameObject coletavelPrefab = collectiblePrefabs[Random.Range(0, collectiblePrefabs.Length)];

            Vector3 posColetavel = novaPlataforma.transform.position;
            posColetavel.y += alturaPlataforma / 2f + 1f;

            // Checa se o espaço acima da plataforma está livre
            if (!Physics2D.OverlapCircle(posColetavel, 0.4f))
                Instantiate(coletavelPrefab, posColetavel, Quaternion.identity);
        }

        // --- 5. Agenda próxima ---
        Invoke("Spawn", Random.Range(spawnMin, spawnMax));
    }

    // Gizmos para visualizar área de checagem
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(transform.position, new Vector3(3f, 1.2f, 1f));
    }
}
