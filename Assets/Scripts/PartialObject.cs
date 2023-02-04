using System;
using UnityEngine;
public class PartialObjectBuild
{
    public Action<Transform> Create;

    public PartialObjectBuild(Action<Transform> createAction)
    {
        Create = createAction;
    }
}

public class PartialObject
{
    public PartialObjectBuild Build() => new PartialObjectBuild((parent) => Create(parent));
    public virtual void Create(Transform parent) => new GameObject("Generic Partial Object").transform.SetParent(parent);
}
