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

    private void Start()
    {
        StartRoom();
    }

    public void StartRoom()
    {
        // teste rapido: spawna 1 corredor no primeiro ponto
        if (spawnPoints.Count > 0)
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
            exitTrigger.Unlock();
        }
    }
}