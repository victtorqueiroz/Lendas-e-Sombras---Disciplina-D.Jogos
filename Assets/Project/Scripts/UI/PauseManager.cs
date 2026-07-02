using UnityEngine;
using UnityEngine.SceneManagement;

namespace Project.UI
{
    public class PauseManager : MonoBehaviour
    {
        [Header("UI Elements")]
        [Tooltip("Arraste o Painel de Pause (Canvas/Panel) para cá")]
        public GameObject pausePanel;

        private bool isPaused = false;

        void Start()
        {
            // Garante que o menu comece escondido e o tempo rodando normal
            if (pausePanel != null)
            {
                pausePanel.SetActive(false);
            }
            Time.timeScale = 1f;
        }

        void Update()
        {
            // Aperte ESC para pausar/despausar
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (isPaused)
                {
                    ResumeGame();
                }
                else
                {
                    PauseGame();
                }
            }
        }

        public void PauseGame()
        {
            if (pausePanel != null)
            {
                pausePanel.SetActive(true);
            }
            Time.timeScale = 0f; // Congela o tempo do Unity
            isPaused = true;
        }

        public void ResumeGame()
        {
            if (pausePanel != null)
            {
                pausePanel.SetActive(false);
            }
            Time.timeScale = 1f; // Descongela o tempo
            isPaused = false;
        }

        public void QuitToMenu()
        {
            Time.timeScale = 1f; // Descongela antes de sair para não bugar o Menu!
            SceneManager.LoadScene("Menu"); // Nome exato da sua cena de Menu
        }
    }
}
