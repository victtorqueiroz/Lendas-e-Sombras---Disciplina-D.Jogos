using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private EnemyFactory enemyFactory;
    [SerializeField] private RoomExit exitTrigger;
    
    [Header("Spawns")]
    [SerializeField] private List<Transform> spawnPoints;
    
    private int aliveEnemiesCount = 0;

    private void Awake()
    {
        // Pega automaticamente o script EnemyFactory se o usuário esqueceu de arrastar
        if (enemyFactory == null)
            enemyFactory = GetComponent<EnemyFactory>();
            
        // Se a lista de spawns for nula, inicializa ela
        if (spawnPoints == null)
            spawnPoints = new List<Transform>();
    }

    private void Start()
    {
        StartRoom();
    }

    public void StartRoom()
    {
        // Spawna os inimigos da Fase 1 (1 Mula e 1 Tanque)
        if (spawnPoints.Count >= 2)
        {
            SpawnEnemy(EnemyType.Corredor, spawnPoints[0]);
            SpawnEnemy(EnemyType.Tanque, spawnPoints[1]);
        }
        else if (spawnPoints.Count == 1)
        {
            SpawnEnemy(EnemyType.Corredor, spawnPoints[0]);
        }
        
        CheckRoomClear();
    }

    private void SpawnEnemy(EnemyType type, Transform spawnPoint)
    {
        Enemy newEnemy = enemyFactory.CreateEnemy(type, spawnPoint.position, spawnPoint.rotation);
        
        if (newEnemy != null)
        {
            newEnemy.OnDied += HandleEnemyDeath; // escuta a morte do bicho
            aliveEnemiesCount++;
        }
    }

    private void HandleEnemyDeath(Enemy deadEnemy)
    {
        deadEnemy.OnDied -= HandleEnemyDeath; // limpa a escuta pra n vazar memoria
        aliveEnemiesCount--;
        
        CheckRoomClear();
    }

    private void CheckRoomClear()
    {
        // matou todos = libera a porta
        if (aliveEnemiesCount <= 0)
        {
            if (exitTrigger != null)
            {
                exitTrigger.Unlock();
            }
            else
            {
                Debug.LogWarning("RoomManager: Exit Trigger não foi assinalado no Inspector!");
            }
        }
    }
}