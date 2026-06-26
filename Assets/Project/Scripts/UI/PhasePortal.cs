using UnityEngine;
using UnityEngine.SceneManagement;

namespace Lobby
{
    public class PhasePortal : MonoBehaviour
    {
        [SerializeField] private string phaseSceneName = "Phase_1"; // Nome da cena a carregar
        [SerializeField] private int phaseNumber = 1; // Numero visual da fase
        [SerializeField] private ParticleSystem portalEffect;

        private bool canEnter = false;

        private void Start()
        {
            GetComponent<Collider2D>().isTrigger = true;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                canEnter = true;
                if (portalEffect != null)
                    portalEffect.Play();
            }
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.CompareTag("Player") && canEnter)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    EnterPhase();
                }
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                canEnter = false;
                if (portalEffect != null)
                    portalEffect.Stop();
            }
        }

        private void EnterPhase()
        {
            // Salvar posição do jogador ou outro estado necessário
            SceneManager.LoadScene(phaseSceneName);
        }

        public int GetPhaseNumber() => phaseNumber;
    }
}