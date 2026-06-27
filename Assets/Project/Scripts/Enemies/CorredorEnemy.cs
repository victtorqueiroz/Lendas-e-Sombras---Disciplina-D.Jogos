using UnityEngine;

public class CorredorEnemy : Enemy
{
    protected override void Start()
    {
        base.Start(); // chama o start do molde base
        
        // ajusta a vida pro corredor (mais fraco)
        maxHealth = 50f;
        currentHealth = maxHealth;
        SetAttackStrategy(new CorredorAttackStrategy());
    }
}