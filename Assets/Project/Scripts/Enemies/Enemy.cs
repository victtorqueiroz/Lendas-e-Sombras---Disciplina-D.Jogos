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

    protected virtual void Die()
    {
        OnDied?.Invoke(this); // avisa quem tiver escutando q morreu
        Destroy(gameObject);
    }

    // injetar a logica de ataque dps
    public void SetAttackStrategy(IAttackStrategy strategy)
    {
        this.attackStrategy = strategy;
    }
}