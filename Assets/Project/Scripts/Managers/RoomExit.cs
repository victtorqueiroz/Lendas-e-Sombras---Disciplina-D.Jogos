using UnityEngine;

public class RoomExit : MonoBehaviour
{
    private bool isUnlocked = false;

    public string nextSceneName = "Fase2";

    public void Unlock()
    {
        isUnlocked = true;
        Debug.Log("Porta liberada para a próxima fase!");
        
        // Brilha ou muda de cor para o jogador saber que abriu (opcional)
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null) sr.color = Color.green;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // checa se ta destrancado e se foi o player q encostou
        if (isUnlocked && other.CompareTag("Player"))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(nextSceneName);
        }
    }
}