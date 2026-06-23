using UnityEngine;

public class TanqueEnemy : Enemy
{
    protected override void Start()
    {
        base.Start();
        
        // tanque aguenta mais porrada
        maxHealth = 200f;
        currentHealth = maxHealth;
    }
}