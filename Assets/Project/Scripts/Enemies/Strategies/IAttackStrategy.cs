using UnityEngine;

public interface IAttackStrategy
{
    // Agora exigimos que todo ataque saiba quem é o alvo (target)
    void Attack(GameObject target); 
}