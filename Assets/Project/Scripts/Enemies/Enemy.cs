using System;
using UnityEngine;

public enum EnemyType
{
    Corredor,
    Tanque
}

public abstract class Enemy : MonoBehaviour
{
    [Header("Status")]
    public float maxHealth = 100f;
    protected float currentHealth;
    
    // ref do strategy de ataque
    protected IAttackStrategy attackStrategy;

    // disparar isso pra avisar o RoomManager
    public event Action<Enemy> OnDied;

    protected virtual void Start()
    {
        currentHealth = maxHealth;
    }

    public virtual void TakeDamage(float amount)
    {
        currentHealth -= amount;
        
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    [ContextMenu("Matar Inimigo")]
    protected virtual void Die()
    {
        OnDied?.Invoke(this); // avisa quem tiver escutando q morreu
        
        // Toca a animação de morte
        Animator anim = GetComponentInChildren<Animator>();
        if (anim != null)
        {
            anim.SetTrigger("Morto");
        }

        // Desativa o colisor para o player conseguir passar por ele e não tomar mais dano
        Collider2D coll = GetComponent<Collider2D>();
        if (coll != null) coll.enabled = false;

        // Desliga a inteligência artificial para ele parar de seguir/atacar
        EnemyAI ai = GetComponent<EnemyAI>();
        if (ai != null) ai.enabled = false;

        // Destrói o inimigo depois de 1.5 segundos (tempo para a animação tocar)
        Destroy(gameObject, 1.5f);
    }

    // injetar a logica de ataque dps
    public void SetAttackStrategy(IAttackStrategy strategy)
    {
        this.attackStrategy = strategy;
    }

    // O Cérebro vai chamar este método quando for a hora de bater
    public void PerformAttack(GameObject target)
    {
        if (attackStrategy != null)
        {
            attackStrategy.Attack(target);
        }
        else
        {
            Debug.LogWarning("Inimigo tentou atacar, mas está sem estratégia!");
        }
    }   
}