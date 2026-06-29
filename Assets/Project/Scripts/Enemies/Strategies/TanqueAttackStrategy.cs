using UnityEngine;
using Player; // Added namespace for PlayerController

public class TanqueAttackStrategy : IAttackStrategy
{
    private int damage = 2;

    public void Attack(GameObject target)
    {
        Debug.Log("Tanque executou um ataque PESADO e LENTO!");
        
        PlayerController bentoHealth = target.GetComponent<PlayerController>();
        if (bentoHealth != null)
        {
            bentoHealth.TakeDamage(damage);
        }
    }
}