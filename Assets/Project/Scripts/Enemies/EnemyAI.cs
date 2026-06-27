using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    // 1. Definindo os estados possíveis da nossa máquina
    public enum EnemyState
    {
        Idle,
        Patrulha,
        Persegue,
        Ataca
    }

    // O estado atual em que o inimigo se encontra
    public EnemyState currentState = EnemyState.Patrulha;

    [Header("Configurações de Movimento")]
    public float patrolSpeed = 2f; // Velocidade andando de boa
    public float chaseSpeed = 5f;  // Velocidade correndo atrás do Bento

    // Referência para o nosso contrato de ataque
    private IAttackStrategy attackStrategy;

    void Update()
    {
        // 2. A Máquina de Estados em ação! O Update roda todo frame e checa o estado atual.
        switch (currentState)
        {
            case EnemyState.Idle:
                // Fica paradinho
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

    // 3. Os métodos que vão rodar dentro de cada estado
    private void Patrol()
    {
        // Por enquanto, apenas avisa no console
        // No futuro, colocaremos o código para andar de um lado pro outro aqui
    }

    private void Chase()
    {
        // Vai correr na direção do jogador usando a chaseSpeed
    }

    private void AttackPlayer()
    {
        // Se tiver uma estratégia de ataque equipada, ele usa!
        if (attackStrategy != null)
        {
            // O alvo (target) será preenchido na Fase 3 quando o inimigo detectar o Bento
            // attackStrategy.Attack(alvo); 
        }
    }

    // Função que usaremos mais pra frente para "equipar" a estratégia certa (Rápida ou Pesada)
    public void SetAttackStrategy(IAttackStrategy strategy)
    {
        attackStrategy = strategy;
    }
}