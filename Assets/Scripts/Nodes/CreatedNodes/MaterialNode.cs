using System.Collections.Generic;
using UnityEngine;

public class MaterialNode : Node
{
    public override List<NInput> inputs { get; } = new List<NInput>() { new NInput("Index", typeof(int),0) };
    public override List<NOutput> outputs { get; protected set; }

    public override void Init()
    {
        outputs = new List<NOutput>() { new NOutput("Material", typeof(Material), this) };
    }

    public override object OnExecute()
    {
        if (connections.Count < 1) return null;
        int matnum = connections[0].GetData<int>();
        return GameController.Instance.meshDatabase.materials[matnum];
    }
}
