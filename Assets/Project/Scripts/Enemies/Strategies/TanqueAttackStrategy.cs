using UnityEngine;

public class TanqueAttackStrategy : IAttackStrategy
{
    private int damage = 2;

    public void Attack(GameObject target)
    {
        Debug.Log("Tanque executou um ataque PESADO e LENTO!");
        
        // Aqui vamos buscar o script de vida do Bento.
        // Exemplo:
        // PlayerHealth bentoHealth = target.GetComponent<PlayerHealth>();
        // if (bentoHealth != null) bentoHealth.TakeDamage(damage);
    }
}