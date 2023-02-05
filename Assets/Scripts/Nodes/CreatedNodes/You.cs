using System.Collections.Generic;
using UnityEngine;

public class You : Node
{
    public override List<NInput> inputs { get; } = new List<NInput>() { new NInput("YAAAY", typeof(int),0) };
    public override List<NOutput> outputs { get; protected set; }

    public override void Init()
    {
        outputs = new List<NOutput>() { new NOutput("AWESOME", typeof(int), this), new NOutput("Yes", typeof(int), this) };
    }

    public override object OnExecute()
    {
        return 1;
    }
}