using UnityEngine;
using UnityEngine.UI;

namespace Player.UI
{
    public class HealthUI : MonoBehaviour
    {
        [SerializeField] private PlayerController playerController;
        [SerializeField] private Image[] heartImages = new Image[5]; // 5 corações
        [SerializeField] private Sprite fullHeartSprite;
        [SerializeField] private Sprite emptyHeartSprite;
        [SerializeField] private Color fullHeartColor = Color.white;
        [SerializeField] private Color emptyHeartColor = new Color(0.5f, 0.5f, 0.5f, 0.5f);

        private void OnEnable()
        {
            if (playerController != null)
            {
                playerController.OnHealthChanged += UpdateHealth;
                UpdateHealth(playerController.GetHealth(), playerController.GetMaxHealth());
            }
        }

        private void OnDisable()
        {
            if (playerController != null)
            {
                playerController.OnHealthChanged -= UpdateHealth;
            }
        }

        private void UpdateHealth(int currentHearts, int maxHearts)
        {
            // Mostrar/esconder corações baseado na vida atual
            for (int i = 0; i < heartImages.Length; i++)
            {
                if (i < currentHearts)
                {
                    // Coração cheio
                    heartImages[i].sprite = fullHeartSprite;
                    heartImages[i].color = fullHeartColor;
                }
                else
                {
                    // Coração vazio
                    heartImages[i].sprite = emptyHeartSprite;
                    heartImages[i].color = emptyHeartColor;
                }
            }
        }
    }
}