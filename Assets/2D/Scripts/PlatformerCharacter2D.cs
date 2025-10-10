using UnityEngine;
using UnityEngine.SceneManagement; // Adicionado para carregar a cena

public class RunnerCharacter : MonoBehaviour
{
    // --- Public/Inspector Variables ---

    [SerializeField] private float m_RunSpeed = 10f;
    [SerializeField] private float m_JumpForce = 400f;
    [SerializeField] private bool m_AirControl = false;
    [SerializeField] private LayerMask m_WhatIsGround;
    [SerializeField] private int m_MaxJumpCount = 2;

    // Opcional: tag que seus obstáculos usarão (configure no Inspector)
    [SerializeField] private string m_ObstacleTag = "Obstacle";

    // --- Private Variables ---

    private int m_JumpCount = 0;
    private Transform m_GroundCheck;
    const float k_GroundedRadius = .2f;
    private bool m_Grounded;
    private Animator m_Anim;
    private Rigidbody2D m_Rigidbody2D;

    private void Awake()
    {
        m_GroundCheck = transform.Find("GroundCheck");
        m_Anim = GetComponent<Animator>();
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        // [Lógica de verificação do chão e movimento contínuo (mantida)]
        m_Grounded = false;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                m_Grounded = true;
                break;
            }
        }

        m_Anim.SetBool("Ground", m_Grounded);

        if (m_Grounded)
        {
            m_JumpCount = 0;
        }

        m_Anim.SetFloat("vSpeed", m_Rigidbody2D.velocity.y);

        if (m_Grounded || m_AirControl)
        {
            m_Rigidbody2D.velocity = new Vector2(m_RunSpeed, m_Rigidbody2D.velocity.y);
        }

        m_Anim.SetFloat("Speed", m_RunSpeed);
    }

    public void Move(bool jump)
    {
        // [Lógica de Pulo (mantida)]
        if (jump && (m_Grounded || m_JumpCount < m_MaxJumpCount))
        {
            m_JumpCount++;
            m_Grounded = false;
            m_Anim.SetBool("Ground", false);

            m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, 0f);
            m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
        }
    }

    // --- NOVA LÓGICA DE GAME OVER (COLISÃO) ---

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Verifica se o objeto com o qual colidiu tem a Tag de obstáculo
        if (collision.gameObject.CompareTag(m_ObstacleTag))
        {
            // Se for um obstáculo, ativamos o Game Over
            GameOver();
        }
    }

    private void GameOver()
    {
        Debug.Log("Game Over! Tocou em um obstáculo.");

        // Parar o tempo no jogo é uma opção comum para o Game Over
        // Time.timeScale = 0f; 

        // Recarrega a cena atual (como se fosse um "Retry" automático)
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}