using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CubeNode : Node
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
            outputs = new List<NOutput>() { new NOutput("Partial Object", typeof(PartialObject), this) };
            color = Color.green;
        }
        hasInitialized = true;
    }

    public override object OnExecute()
    {
        MeshPartialObject partialObject = new MeshPartialObject();
        partialObject.scale = FindConnection(inputs[0])?.GetData<Vector3>() ?? (Vector3)inputs[0].defaultValue;
        partialObject.mat = FindConnection(inputs[1])?.GetData<Material>() ?? (Material)inputs[1].defaultValue;
        partialObject.mesh = GameController.Instance.meshDatabase.meshes[0];
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