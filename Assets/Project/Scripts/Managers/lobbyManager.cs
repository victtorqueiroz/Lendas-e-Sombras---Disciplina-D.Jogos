using UnityEngine;
using Player;

namespace Lobby
{
    public class LobbyManager : MonoBehaviour
    {
        [SerializeField] private Transform playerSpawnPoint;
        [SerializeField] private GameObject playerPrefab;

        private PlayerController playerInstance;

        private void Start()
        {
            if (playerSpawnPoint == null)
            {
                Debug.LogError("Player spawn point não foi definido no LobbyManager!");
                return;
            }

            SpawnPlayer();
        }

        private void SpawnPlayer()
        {
            if (playerPrefab == null)
            {
                Debug.LogError("Player prefab não foi definido no LobbyManager!");
                return;
            }

            // Instanciar player no spawn point
            playerInstance = Instantiate(playerPrefab, playerSpawnPoint.position, Quaternion.identity)
                .GetComponent<PlayerController>();

            if (playerInstance != null)
            {
                // Conectar evento de morte ao respawn
                playerInstance.OnDeath += HandlePlayerDeath;
            }
        }

        private void HandlePlayerDeath()
        {
            if (playerInstance != null)
            {
                playerInstance.OnDeath -= HandlePlayerDeath;
                Destroy(playerInstance.gameObject);
            }

            // Aguardar um pouco e respawnar
            Invoke(nameof(SpawnPlayer), 2f);
        }

        private void OnDestroy()
        {
            if (playerInstance != null)
            {
                playerInstance.OnDeath -= HandlePlayerDeath;
            }
        }
    }
}