using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public enum EnemyState
    {
        Idle,
        Patrulha,
        Persegue,
        Ataca
    }

    public EnemyState currentState = EnemyState.Patrulha;

    [Header("Configurações de Movimento")]
    public float patrolSpeed = 2f;
    public float chaseSpeed = 5f;

    [Header("Detecção (Visão)")]
    public float chaseRadius = 5f;   // Distância para começar a correr atrás
    public float attackRadius = 1.5f; // Distância para dar o golpe
    
    // Referência do jogador que será preenchida automaticamente
    private Transform playerTransform; 
    private IAttackStrategy attackStrategy;

    void Start()
    {
        // Ao nascer, o inimigo procura na cena quem tem a Tag "Player" (que será o Bento)
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
    }

    void Update()
    {
        // 1. Primeiro ele olha em volta e decide o que sentir
        CheckStateTransitions();

        // 2. Depois ele age baseado no que sentiu
        switch (currentState)
        {
            case EnemyState.Idle:
                break;
            case EnemyState.Patrulha:
                Patrol();
                break;
            case EnemyState.Persegue:
                Chase();
                break;
            case EnemyState.Ataca:
                AttackPlayer();
                break;
        }
    }

    private void CheckStateTransitions()
    {
        // Se o jogador não estiver na cena (ou já tiver morrido), continua patrulhando
        if (playerTransform == null) return;

        // Calcula a distância exata entre o inimigo e o Bento
        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

        // A lógica de decisão:
        if (distanceToPlayer <= attackRadius)
        {
            currentState = EnemyState.Ataca;
        }
        else if (distanceToPlayer <= chaseRadius)
        {
            currentState = EnemyState.Persegue;
        }
        else
        {
            currentState = EnemyState.Patrulha;
        }
    }

    private void Patrol()
    {
        // A lógica de andar de um lado pro outro entrará aqui (faremos nos prefabs)
    }

    private void Chase()
    {
        // A lógica de mover na direção do playerTransform entrará aqui
    }

    private void AttackPlayer()
    {
        if (attackStrategy != null && playerTransform != null)
        {
            // O inimigo ataca e passa o Bento como alvo!
            // attackStrategy.Attack(playerTransform.gameObject); 
        }
    }

    public void SetAttackStrategy(IAttackStrategy strategy)
    {
        attackStrategy = strategy;
    }

    // --- O TRUQUE DE MESTRE ---
    // Isso desenha os raios de visão do inimigo direto na tela do Unity!
    private void OnDrawGizmosSelected()
    {
        // Círculo amarelo para o raio de Perseguição
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseRadius);

        // Círculo vermelho para o raio de Ataque
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}