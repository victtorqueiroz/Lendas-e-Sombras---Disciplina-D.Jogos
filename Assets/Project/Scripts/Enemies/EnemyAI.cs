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
    
    [Header("Ataque")]
    public float attackCooldown = 1.5f; // Tempo de espera entre cada golpe
    private float lastAttackTime = -100f;

    // Referências
    private Transform playerTransform; 
    private Enemy meuCorpo; 
    private Animator meuAnimator; // Referência para o componente de animação

    // Variáveis para patrulha
    private Vector2 startPosition;
    private Vector2 patrolTarget;

    // O nome exato dos parâmetros que vamos criar no Unity
    private const string CONDICAO_ATAQUE = "Atacando";
    private const string CONDICAO_MOVIMENTO = "Velocidade";

    void Start()
    {
        // Pega o script Enemy que está "grudado" neste mesmo GameObject
        meuCorpo = GetComponent<Enemy>();

        // IMPORTANTÍSSIMO: Como o Animator está no Filho visual, precisamos buscar lá
        meuAnimator = GetComponentInChildren<Animator>();

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }

        startPosition = transform.position;
        patrolTarget = GetRandomPatrolPoint();
    }

    private Vector2 GetRandomPatrolPoint()
    {
        return startPosition + new Vector2(Random.Range(-3f, 3f), Random.Range(-3f, 3f));
    }

    void Update()
    {
        // Se o jogador não estiver na cena, não faz nada
        if (playerTransform == null) return;

        // 1. Primeiro ele olha em volta e decide o que sentir
        CheckStateTransitions();

        // 2. Atualiza as animações baseadas no estado atual
        UpdateAnimations();

        // 3. Depois ele age baseado no que sentiu
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

    private void UpdateAnimations()
    {
        if (meuAnimator == null) return;

        // Resetamos as condições padrão a cada frame
        meuAnimator.SetBool(CONDICAO_ATAQUE, false);
        meuAnimator.SetFloat(CONDICAO_MOVIMENTO, 0f);

        // Definimos a animação baseada no estado atual
        switch (currentState)
        {
            case EnemyState.Patrulha:
                // Se está patrulhando, definimos uma velocidade baixa
                meuAnimator.SetFloat(CONDICAO_MOVIMENTO, 1f); 
                break;
            case EnemyState.Persegue:
                // Se está perseguindo, definimos velocidade alta
                meuAnimator.SetFloat(CONDICAO_MOVIMENTO, 2f); 
                break;
            case EnemyState.Ataca:
                // Se está atacando, ativamos o gatilho de ataque
                meuAnimator.SetBool(CONDICAO_ATAQUE, true);
                break;
        }
    }

    private void Patrol()
    {
        // Lógica simples de patrulha indo para pontos aleatórios ao redor do ponto inicial
        if (Vector2.Distance(transform.position, patrolTarget) < 0.2f)
        {
            patrolTarget = GetRandomPatrolPoint();
        }

        Vector2 direction = (patrolTarget - (Vector2)transform.position).normalized;
        transform.Translate(direction * patrolSpeed * Time.deltaTime);
    }

    private void Chase()
    {
        // Move na direção do player
        if (playerTransform != null)
        {
            Vector2 direction = (playerTransform.position - transform.position).normalized;
            transform.Translate(direction * chaseSpeed * Time.deltaTime);
        }
    }

    private void AttackPlayer()
    {
        // Só ataca se já passou o tempo do cooldown
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            // Se temos um corpo e o Bento está na mira, mandamos o corpo atacar!
            if (meuCorpo != null && playerTransform != null)
            {
                meuCorpo.PerformAttack(playerTransform.gameObject);
                lastAttackTime = Time.time; // Reseta o tempo
            }
        }
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