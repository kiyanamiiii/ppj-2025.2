using UnityEngine;
using System.Collections;

public class MagnetCollectible : MonoBehaviour
{
    [Header("Configurações do Magnetismo")]
    [SerializeField] private float magnetDuration = 5f;       // Tempo que o magnetismo dura
    [SerializeField] private float magnetSpeed = 10f;          // Velocidade de atração
    [SerializeField] private float attractionRadius = 15f;     // Raio de alcance
    [SerializeField] private bool useDistanceFalloff = true;   // Diminui força pela distância

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(ActivateMagnet(collision.gameObject));
            Destroy(gameObject);
        }
    }

    private IEnumerator ActivateMagnet(GameObject player)
    {
        float endTime = Time.time + magnetDuration;

        while (Time.time < endTime)
        {
            PointCollectible[] collectibles = FindObjectsOfType<PointCollectible>();

            foreach (var c in collectibles)
            {
                Rigidbody2D rb = c.GetComponent<Rigidbody2D>();
                if (rb == null) continue;

                Vector2 toPlayer = (player.transform.position - c.transform.position);
                float distance = toPlayer.magnitude;

                if (distance > attractionRadius) continue;

                toPlayer.Normalize();
                float speed = useDistanceFalloff ? magnetSpeed * (1f - distance / attractionRadius) : magnetSpeed;

                rb.gravityScale = 0f;
                rb.velocity = toPlayer * speed;
            }

            yield return new WaitForFixedUpdate();
        }

        // Restaura a física normal
        PointCollectible[] all = FindObjectsOfType<PointCollectible>();
        foreach (var c in all)
        {
            Rigidbody2D rb = c.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.gravityScale = 1f;
                rb.velocity = Vector2.zero;
            }
        }

        Debug.Log("Magnet effect ended.");
    }
}
