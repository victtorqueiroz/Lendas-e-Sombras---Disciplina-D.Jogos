using UnityEngine;
using Player; // Added namespace for PlayerController

public class CorredorAttackStrategy : IAttackStrategy
{
    private int damage = 1;

    public void Attack(GameObject target)
    {
        Debug.Log("Corredor executou um ataque RÁPIDO corpo a corpo!");
        
        PlayerController bentoHealth = target.GetComponent<PlayerController>();
        if (bentoHealth != null)
        {
            bentoHealth.TakeDamage(damage);
        }
    }
}