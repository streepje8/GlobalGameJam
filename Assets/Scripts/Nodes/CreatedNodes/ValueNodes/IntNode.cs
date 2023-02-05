using System.Collections.Generic;
using UnityEngine;

public class IntNode : Node
{
    public override List<NInput> inputs { get; } = new List<NInput>();
    public override List<NOutput> outputs { get; protected set; }
    private int value;
    public IntNode(int value = 0)
    {
        color = Color.cyan;
        this.value = value;
    }
    
    public override void Init()
    {
        outputs = new List<NOutput>() { new NOutput("Value (" + value + ")", typeof(int), this) };
    }

    public override object OnExecute()
    {
        return value;
    }
}
