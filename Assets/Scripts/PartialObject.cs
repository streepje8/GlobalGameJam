using UnityEngine;
public class PartialObject
{
    public virtual void Create(Transform parent) => new GameObject("Generic Partial Object").transform.SetParent(parent);
}
