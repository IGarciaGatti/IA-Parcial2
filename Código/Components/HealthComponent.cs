using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent
{
    private float minHealth;
    private float maxHealth;
    private float currentHealth;
    private bool canRegenerate;
    private float regenSpeed;
    private HealthBar bar;

    public HealthComponent(float minHealth, float maxHealth, bool canRegenerate, float regenSpeed)
    {
        this.minHealth = minHealth;
        this.maxHealth = maxHealth;
        this.canRegenerate = canRegenerate;
        this.regenSpeed = regenSpeed;
        currentHealth = maxHealth;
    }

    public HealthComponent(float minHealth, float maxHealth, bool canRegenerate, float regenSpeed, HealthBar bar)
    {
        this.minHealth = minHealth;
        this.maxHealth = maxHealth;
        this.canRegenerate = canRegenerate;
        this.regenSpeed = regenSpeed;
        currentHealth = maxHealth;
        this.bar = bar;
    }
  
    public void Update()
    {
        Regeneration();
    }

    public bool IsHealthDepleted()
    {
        return currentHealth <= minHealth;
    }

    private void Regeneration()
    {
        if (canRegenerate && currentHealth < maxHealth)
        {
            currentHealth += (Time.deltaTime * regenSpeed);
            if(bar != null)
            {
                bar.SetHealth(currentHealth);
            }            
        }       
    }

    public void DamageTaken(float damage)
    {
        currentHealth -= damage;
        if(bar != null)
        {
            bar.SetHealth(currentHealth);
        }
    }
}
