using System.Collections.Generic;
using UnityEngine;

public class MergeNode : Node
{
    public override List<NInput> inputs { get; } = new List<NInput>()
    {
        new NInput("Object A", typeof(PartialObjectBuild),null),
        new NInput("Object B", typeof(PartialObjectBuild),null) 
    };
    public override List<NOutput> outputs { get; protected set; }

    public override void Init()
    {
        outputs = new List<NOutput>() { new NOutput("Combined Object", typeof(PartialObject), this) };
    }

    public override object OnExecute()
    {
        MergePartialObject partialObject = new MergePartialObject();
        partialObject.objA = FindConnection(inputs[0])?.GetData<PartialObjectBuild>() ?? new DefaultPartialObject().Build();
        partialObject.objB = FindConnection(inputs[1])?.GetData<PartialObjectBuild>() ?? new DefaultPartialObject().Build();
        return partialObject.Build();
    }
}

public class DefaultPartialObject : PartialObject
{
    
    public override void Create(Transform parent)
    {
        GameObject myHolder = new GameObject("Empty_SLOT");
        myHolder.transform.SetParent(parent);
    }
}

public class MergePartialObject : PartialObject
{
    public PartialObjectBuild objA;
    public PartialObjectBuild objB;
        
    public override void Create(Transform parent)
    {
        GameObject myHolder = new GameObject("Merge");
        myHolder.transform.SetParent(parent);
        objA.Create(myHolder.transform);
        objB.Create(myHolder.transform);
    }
}
