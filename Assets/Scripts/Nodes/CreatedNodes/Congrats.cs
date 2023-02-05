using System.Collections.Generic;
using UnityEngine;

public class Congrats : Node
{
    public override List<NInput> inputs { get; } = new List<NInput>() { };
    public override List<NOutput> outputs { get; protected set; }

    public override void Init()
    {
        outputs = new List<NOutput>() { new NOutput("WHOOO", typeof(int), this) };
    }

    public override object OnExecute()
    {
        return 0;
    }
}