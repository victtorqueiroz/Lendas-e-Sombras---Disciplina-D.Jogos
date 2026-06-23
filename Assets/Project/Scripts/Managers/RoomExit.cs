using UnityEngine;

public class RoomExit : MonoBehaviour
{
    private bool isUnlocked = false;

    public void Unlock()
    {
        isUnlocked = true;
        Debug.Log("porta liberada pra proxima fase!");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // checa se ta destrancado e se foi o player q encostou
        if (isUnlocked && other.CompareTag("Player"))
        {
            Debug.Log("disparar AudioManager e FadeManager aqui dps");
            // ex: AudioManager.Instance.PlaySound("porta_abrindo");
        }
    }
}