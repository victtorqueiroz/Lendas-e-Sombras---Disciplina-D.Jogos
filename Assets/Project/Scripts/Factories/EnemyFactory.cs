using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    [Header("Prefabs dos Inimigos")]
    [SerializeField] private Enemy corredorPrefab;
    [SerializeField] private Enemy tanquePrefab;

    // a sala chama isso aqui passando o tipo q quer spawnar
    public Enemy CreateEnemy(EnemyType type, Vector3 position, Quaternion rotation)
    {
        switch (type)
        {
            case EnemyType.Corredor:
                return Instantiate(corredorPrefab, position, rotation);
            case EnemyType.Tanque:
                return Instantiate(tanquePrefab, position, rotation);
            default:
                Debug.LogWarning("tipo de inimigo nao configurado na factory!");
                return null;
        }
    }
}