using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomNode : INode
{
    Roulette roulette;
    Dictionary<INode, float> items;

    public RandomNode(Dictionary<INode, float> items)
    {
        roulette = new Roulette();
        SetItems(items);
    }

    public RandomNode(Roulette roulette, Dictionary<INode, float> items)
    {
        this.roulette = roulette;
        SetItems(items);
    }

    public void SetItems(Dictionary<INode, float> items)
    {
        this.items = items;
    }

    public void Execute()
    {
        INode node = roulette.Run(items);
        node.Execute();
    }
}
