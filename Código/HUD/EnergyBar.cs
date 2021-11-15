using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{
    [SerializeField] private List<Image> barSegments;
    [SerializeField] private float segmentLength;

    public float SegmentLength => segmentLength;

    private bool DisplayEnergy(float energy, int segmentNumber)
    {
        return (segmentNumber * segmentLength) >= energy;
    }

    public void Increase(float energy)
    {
        for (int i = 0; i < barSegments.Count; i++)
        {
            if (!DisplayEnergy(energy, i))
            {
                barSegments[i].CrossFadeAlpha(1f, 0.8f, false);
            }
        }
    }

    public void Decrease(float energy)
    {
        for (int i = 0; i < barSegments.Count; i++)
        {
            if (DisplayEnergy(energy, i))
            {
                barSegments[i].CrossFadeAlpha(0f, 0.8f, false);
            }
        }
    }
}
