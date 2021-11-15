using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyComponent
{
    private float currentEnergy;
    private float minEnergy;
    private float maxEnergy;
    private float rechargeTime;
    private float counter = 0;
    private EnergyBar bar;

    public EnergyComponent(float minEnergy, float maxEnergy, float rechargeTime, EnergyBar bar)
    {
        this.minEnergy = minEnergy;
        this.maxEnergy = maxEnergy;
        this.rechargeTime = rechargeTime;
        this.bar = bar;
        currentEnergy = maxEnergy;
    }

    public void Update()
    {
        Recharge();
    }

    public bool HasEnoughEnergy()
    {
        return currentEnergy >= (maxEnergy / bar.SegmentLength);
    }

    public void UseEnergy(float energyUsed)
    {
        currentEnergy -= energyUsed;
        bar.Decrease(currentEnergy);
    }

    private void Recharge()
    {
        if (currentEnergy >= minEnergy && currentEnergy < maxEnergy)
        {
            counter += Time.deltaTime;
            if(counter >= rechargeTime)
            {
                currentEnergy += 20;
                bar.Increase(currentEnergy);
                counter = 0;
            }
        }

        if(currentEnergy > maxEnergy)
        {
            currentEnergy = maxEnergy;
        }
        if(currentEnergy < minEnergy)
        {
            currentEnergy = minEnergy;
        }
    }

}
