using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class ShapeNode : Node
{
    public List<Control> controls = new List<Control>()
    {
        new MeshControl("Shape",null)
    };

    public new List<Input> inputs = new List<Input>()
    {
        new Input("Scale", typeof(Vector3), Vector3.one),
        new Input("Material", typeof(Material))
    };

    public ShapeNode()
    {
        outputs = new List<Output>() { new Output("Partial Object", typeof(PartialObject), this) };
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