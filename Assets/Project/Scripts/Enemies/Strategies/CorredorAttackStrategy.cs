using UnityEngine;

public class CorredorAttackStrategy : IAttackStrategy
{
    private int damage = 1;

    // O método precisa ter a mesma assinatura que você definiu na sua IAttackStrategy.
    // Estou assumindo que ele recebe o alvo (GameObject) como parâmetro.
    public void Attack(GameObject target)
    {
        Debug.Log("Corredor executou um ataque RÁPIDO corpo a corpo!");
        
        // Aqui vamos buscar o script de vida do Bento. 
        // Exemplo:
        // PlayerHealth bentoHealth = target.GetComponent<PlayerHealth>();
        // if (bentoHealth != null) bentoHealth.TakeDamage(damage);
    }
}