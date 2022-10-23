using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private Image healthBarImage;

    /** Call whenever player health changes **/
    public void UpdateHealthBar(float playerHealth, float playerMaxHealth)
    {
        float duration = 0.75f * (playerHealth / playerMaxHealth);
        healthBarImage.DOFillAmount(playerHealth / playerMaxHealth, duration);

        Color newColor = Color.green;
        if (playerHealth < playerMaxHealth * 0.25f) {
            newColor = Color.red;
        } else if (playerHealth < playerMaxHealth * 0.66f) {
            newColor = new Color(1f, 0.64f, 0f, 1f);
        }

        healthBarImage.DOColor(newColor, duration);
    }

    private void OnDestroy()
    {
        healthBarImage.DOKill();
    }
}
