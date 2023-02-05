using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class ColliderNode : Node
{
    public override List<NInput> inputs
    {
        get =>
            new List<NInput>()
            {
                new NInput("Center", typeof(Vector3), Vector3.zero),
                new NInput("Size", typeof(Vector3),Vector3.one)
            };
    }

    public override List<NOutput> outputs { get; protected set; } = new List<NOutput>();

    private bool hasInitialized = false;
    
    public override void Init()
    {
        if (!hasInitialized)
        {
            outputs = new List<NOutput>() { new NOutput("Partial Object", typeof(PartialObject), this) };
            color = Color.magenta;
        }
        hasInitialized = true;
    }

    public override object OnExecute()
    {
        ColliderPartialObject partialObject = new ColliderPartialObject();
        partialObject.center = FindConnection(inputs[0])?.GetData<Vector3>() ?? (Vector3)inputs[0].defaultValue;
        partialObject.size = FindConnection(inputs[1])?.GetData<Vector3>() ?? (Vector3)inputs[1].defaultValue;
        return partialObject.Build();
    }
}

public class ColliderPartialObject : PartialObject
{
    public Vector3 center = Vector3.zero;
    public Vector3 size = Vector3.one;
        
    public override void Create(Transform parent)
    {
        GameObject myHolder = new GameObject("ColliderObject");
        myHolder.transform.SetParent(parent);
        BoxCollider bcol = myHolder.AddComponent<BoxCollider>();
        bcol.center = center;
        bcol.size = size;
    }
}