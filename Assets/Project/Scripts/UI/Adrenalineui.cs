using UnityEngine;
using UnityEngine.UI;

namespace Player.UI
{
    public class AdrenalineUI : MonoBehaviour
    {
        [SerializeField] private PlayerController playerController;
        [SerializeField] private Image adrenalineFill;
        [SerializeField] private Image adrenalineGlowOverlay; // Efeito de brilho quando cheio
        [SerializeField] private Text adrenalineText;
        [SerializeField] private Color adrenalineColor = new Color(1f, 0.8f, 0f); // Amarelo

        private void OnEnable()
        {
            if (playerController != null)
            {
                playerController.OnAdrenalineChanged += UpdateAdrenaline;
                UpdateAdrenaline(playerController.GetAdrenaline(), playerController.GetMaxAdrenaline());
            }
        }

        private void OnDisable()
        {
            if (playerController != null)
            {
                playerController.OnAdrenalineChanged -= UpdateAdrenaline;
            }
        }

        private void UpdateAdrenaline(float currentAdrenaline, float maxAdrenaline)
        {
            if (adrenalineFill != null)
            {
                adrenalineFill.fillAmount = currentAdrenaline / maxAdrenaline;
            }

            if (adrenalineGlowOverlay != null)
            {
                // Fazer brilhar quando estiver cheio
                float glowAlpha = currentAdrenaline >= maxAdrenaline ? 0.8f : 0f;
                adrenalineGlowOverlay.color = new Color(1f, 1f, 1f, glowAlpha);
            }

            if (adrenalineText != null)
            {
                adrenalineText.text = $"{currentAdrenaline:F0} / {maxAdrenaline:F0}";
            }
        }
    }
}