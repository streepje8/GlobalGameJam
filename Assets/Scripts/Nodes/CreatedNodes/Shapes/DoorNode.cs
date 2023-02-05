using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DoorNode : Node
{
    public override List<NInput> inputs
    {
        get =>
            new List<NInput>()
            {
                new NInput("HasDoorHandle", typeof(int), 0),
                new NInput("Material", typeof(Material))
            };
    }

    public override List<NOutput> outputs { get; protected set; } = new List<NOutput>();

    private bool hasInitialized = false;
    
    public override void Init()
    {
        if (!hasInitialized)
        {
            outputs = new List<NOutput>() { new NOutput("Partial Object", typeof(PartialObject), this) };
            color = Color.green;
        }
        hasInitialized = true;
    }

    public override object OnExecute()
    {
        MeshPartialObject partialObject = new MeshPartialObject();
        int hasDoorHandle = FindConnection(inputs[0])?.GetData<int>() ?? (int)inputs[0].defaultValue;
        partialObject.mat = FindConnection(inputs[1])?.GetData<Material>() ?? (Material)inputs[1].defaultValue;
        partialObject.mesh = hasDoorHandle > 0 ? GameController.Instance.meshDatabase.meshes[5] : GameController.Instance.meshDatabase.meshes[4];
        return partialObject.Build();
    }
}