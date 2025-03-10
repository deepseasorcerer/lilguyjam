using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{
    [Header("UI Elements")]
    public Slider healthSlider;
    public Image fillImage;
    public Gradient healthGradient;
    public TextMeshProUGUI currentHealthText;
    public TextMeshProUGUI maxHealthText;

    [Header ("Effects COMING SOON")]


    [Header("Player Health Settings")]
    public float maxHealth = 100f;
    private float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = maxHealth;
        UpdateHealthBar();
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthBar();
    }

    public void Heal(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        healthSlider.value = currentHealth;
        fillImage.color = healthGradient.Evaluate(healthSlider.normalizedValue);
        maxHealthText.text = $"/{maxHealth}";
        currentHealthText.text = $"{currentHealth}";
    }
}
