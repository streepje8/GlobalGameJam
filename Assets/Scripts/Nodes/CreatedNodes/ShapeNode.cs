using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShapeNode : Node
{
    public override List<Control> controls { get; } = new List<Control>()
    {
        new MeshControl("Shape", null)
    };

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

    public override void Init()
    {
        outputs = new List<NOutput>() { new NOutput("Partial Object", typeof(PartialObject), this) };
    }

    public override object OnExecute()
    {
        MeshPartialObject partialObject = new MeshPartialObject();
        partialObject.scale = FindConnection(inputs[0])?.GetData<Vector3>() ?? (Vector3)inputs[0].defaultValue;
        partialObject.mat = FindConnection(inputs[1])?.GetData<Material>() ?? (Material)inputs[1].defaultValue;
        return partialObject.Build();
    }
}

public class MeshPartialObject : PartialObject
{
    public Vector3 scale = Vector3.one;
    public Material mat = null;
    public Mesh mesh;
        
    public override void Create(Transform parent)
    {
        GameObject myHolder = new GameObject("ShapeNodeObject");
        myHolder.transform.SetParent(parent);
        MeshRenderer mr = myHolder.AddComponent<MeshRenderer>();
        mr.material = mat;
        MeshFilter filter = myHolder.AddComponent<MeshFilter>();
        filter.mesh = mesh; filter.sharedMesh = mesh;
    }
}