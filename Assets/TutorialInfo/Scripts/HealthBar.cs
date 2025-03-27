using Assets.Scripts.Player;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [Header("UI Elements")]
    public Slider healthSlider;
    public Image fillImage;
    public Gradient healthGradient;
    public TextMeshProUGUI currentHealthText;
    public TextMeshProUGUI maxHealthText;

    [Header("Effects")]
    public Animator damageAnimator;
    public RectTransform healthBarTransform;
    private Vector3 originalScale; // Store the original scale of the health bar

    private bool useFirstAnimation = true; //Tracks which animation to play

    private float currentHealth;

    void Start()
    {
        float startHealth = Player.Instance.MaxHealth;

        currentHealth = startHealth;
        healthSlider.value = startHealth;
        originalScale = healthBarTransform.localScale; //For health bar animation

        UpdateMaxHealth();
        UpdateHealthBar();
    }

    /*------THEALTH BAR TESTER--------
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1)) // Press "1" to take damage
            {
               TakeDamage(10f); // Adjust damage amount as needed
            }
        }
    */

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, Player.Instance.MaxHealth);
        UpdateHealthBar();
    }

    public void Heal(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, Player.Instance.MaxHealth);
        UpdateHealthBar();
    }

    public void UpdateMaxHealth()
    {
        healthSlider.maxValue = Player.Instance.MaxHealth;
        maxHealthText.text = $"/{Player.Instance.MaxHealth}";
        fillImage.color = healthGradient.Evaluate(healthSlider.normalizedValue);
    }

    private void UpdateHealthBar()
    {
        healthSlider.value = currentHealth;
        fillImage.color = healthGradient.Evaluate(healthSlider.normalizedValue);
        DamageEffect();
        maxHealthText.text = $"/{Player.Instance.MaxHealth}";
        currentHealthText.text = $"{currentHealth}";
    }

    private void DamageEffect()
    {
        //----DAMAGE SLASH ANIMATION-----
        if (damageAnimator != null)
        {
            // Reset both triggers before setting the new one
            damageAnimator.ResetTrigger("TakeDamage1");
            damageAnimator.ResetTrigger("TakeDamage2");

            if (useFirstAnimation)
            {
                damageAnimator.SetTrigger("TakeDamage1");
            }
            else
            {
                damageAnimator.SetTrigger("TakeDamage2");
            }

            useFirstAnimation = !useFirstAnimation; // Toggle to switch next time
        }

        //-----HEALTH BAR ANIMATION-----
        float scaleFactor = 1.2f; // How much bigger it gets
        float duration = 0.1f; // Speed of the effect

        // Scale up
        LeanTween.scale(healthBarTransform, originalScale * scaleFactor, duration)
            .setEaseOutBounce()
            .setOnComplete(() =>
            {
                // Scale back to original size
                LeanTween.scale(healthBarTransform, originalScale, duration).setEaseInOutQuad();
            });
    }
}