using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Counter : MonoBehaviour
{
    [SerializeField] private Text text;
    private int minAmount;
    private int maxAmount;
    private int currentAmount;
    private bool limitReached;

    public bool LimitReached => limitReached;

    public void StartAmount(int minAmount, int maxAmount)
    {
        this.minAmount = minAmount;
        this.maxAmount = maxAmount;
        currentAmount = minAmount;
        SetText();
    }

    public void UpdateAmount(int value)
    {
        if(currentAmount < maxAmount)
        {
            currentAmount += value;
            SetText();
        }        
        if(currentAmount >= maxAmount)
        {
            limitReached = true;
        }
    }

    private void SetText()
    {
        text.text = currentAmount + "/" + maxAmount;
    }
}
