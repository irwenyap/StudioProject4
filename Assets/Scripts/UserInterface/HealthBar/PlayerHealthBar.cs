using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    private const float DAMAGED_HEALTH_SHRINK_TIMER_MAX = 0.25f;
    private Image fillImage;
    private Image damagedFillImage;
    private float damagedHealthShrinkTimer;
    private HealthSystem healthSystem;

    private void Awake()
    {
        fillImage = transform.Find("Fill").GetComponent<Image>();
        damagedFillImage = transform.Find("DamageFill").GetComponent<Image>();
    }

    private void Start()
    {
        healthSystem = new HealthSystem(100); //start with 100 health
        SetHealth(healthSystem.GetHealthNormalized());
        damagedFillImage.fillAmount = fillImage.fillAmount;
        healthSystem.OnDamaged += HealthSystem_OnDamaged;
        healthSystem.OnHealed += HealthSystem_OnHealed;
    }

    private void Update()
    {
        damagedHealthShrinkTimer -= Time.deltaTime;
        if (damagedHealthShrinkTimer < 0.0f)
        {
            if (fillImage.fillAmount < damagedFillImage.fillAmount)
            {
                float shrinkSpeed = 4.0f;
                damagedFillImage.fillAmount -= shrinkSpeed * Time.deltaTime;
            }
        }

        if (Input.GetKeyDown(KeyCode.A))
            healthSystem.Damage(10);
        if (Input.GetKeyDown(KeyCode.S))
            healthSystem.Heal(10);
    }

    private void HealthSystem_OnDamaged(object sender, System.EventArgs e)
    {
        damagedHealthShrinkTimer = DAMAGED_HEALTH_SHRINK_TIMER_MAX;
        SetHealth(healthSystem.GetHealthNormalized());
    }

    private void HealthSystem_OnHealed(object sender, System.EventArgs e)
    {
        SetHealth(healthSystem.GetHealthNormalized());
        damagedFillImage.fillAmount = fillImage.fillAmount;
    }

    private void SetHealth(float healthNormalized)
    {
        fillImage.fillAmount = healthNormalized;
    }
}
