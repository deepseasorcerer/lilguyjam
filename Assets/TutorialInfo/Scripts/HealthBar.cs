using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class HealthBar : MonoBehaviour
{
    [Header("UI Elements")]
    public Slider healthSlider;
    public Image fillImage;
    public Gradient healthGradient;
    public TextMeshProUGUI currentHealthText;
    public TextMeshProUGUI maxHealthText;

    [Header ("Effects")]
    public Animator damageAnimator;
    public RectTransform healthBarTransform;
    private Vector3 originalScale; // Store the original scale of the health bar

    private bool useFirstAnimation = true; //Tracks which animation to play

    [Header("Player Health Settings")]
    public float maxHealth = 100f;
    private float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = maxHealth;
        originalScale = healthBarTransform.localScale; //For health bar animation
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
        DamageEffect();
        maxHealthText.text = $"/{maxHealth}";
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
