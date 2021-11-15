using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class QuestionNode : INode
{
    Func<bool> question;
    INode trueNode;
    INode falseNode;

    public QuestionNode(Func<bool> question, INode trueNode, INode falseNode)
    {
        this.question = question;
        this.trueNode = trueNode;
        this.falseNode = falseNode;
    }

    public void Execute()
    {
        if (question != null && question())
        {
            trueNode.Execute();
        }
        else
        {
            falseNode.Execute();
        }
    }
}
