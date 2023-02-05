using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WallNode : Node
{
    public override List<NInput> inputs
    {
        get =>
            new List<NInput>()
            {
                new NInput("Scale", typeof(Vector3), Vector3.one),
                new NInput("Material", typeof(Material))
            };
    }

    public override List<NOutput> outputs { get; protected set; } = new List<NOutput>();

    private bool hasInitialized = false;
    
    public override void Init()
    {
        if (!hasInitialized)
        {
            outputs = new List<NOutput>() { new NOutput("Partial Object", typeof(PartialObjectBuild), this) };
            color = Color.green;
        }
        hasInitialized = true;
    }

    public override object OnExecute()
    {
        MeshPartialObject partialObject = new MeshPartialObject();
        partialObject.scale = FindConnection(inputs[0])?.GetData<Vector3>() ?? (Vector3)inputs[0].defaultValue;
        partialObject.mat = FindConnection(inputs[1])?.GetData<Material>() ?? (Material)inputs[1].defaultValue;
        partialObject.mesh = GameController.Instance.meshDatabase.meshes[6];
        return partialObject.Build();
    }
}