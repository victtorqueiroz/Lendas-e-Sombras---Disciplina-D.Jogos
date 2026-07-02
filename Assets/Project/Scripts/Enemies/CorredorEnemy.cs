using UnityEngine;

public class CorredorEnemy : Enemy
{
    protected override void Start()
    {
        base.Start(); // chama o start do molde base
        
        // Mula morre com 3 golpes (30 de vida)
        maxHealth = 30f;
        currentHealth = maxHealth;
        SetAttackStrategy(new CorredorAttackStrategy());
    }
}