using UnityEngine;

public class TanqueEnemy : Enemy
{
    protected override void Start()
    {
        base.Start();
        
        // Tanque morre com 5 golpes (50 de vida)
        maxHealth = 50f;
        currentHealth = maxHealth;
        SetAttackStrategy(new TanqueAttackStrategy());
    }
}