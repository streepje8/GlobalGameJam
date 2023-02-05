using System.Collections.Generic;
using UnityEngine;

public class VectorNode : Node
{
    public override List<NInput> inputs { get; } = new List<NInput>();
    public override List<NOutput> outputs { get; protected set; }
    private Vector3 value;
    public VectorNode(Vector3 value = new Vector3())
    {
        color = Color.cyan;
        this.value = value;
    }
    
    public override void Init()
    {
        outputs = new List<NOutput>() { new NOutput("Value (" + value + ")", typeof(Vector3), this) };
    }

    public override object OnExecute()
    {
        return value;
    }
}
