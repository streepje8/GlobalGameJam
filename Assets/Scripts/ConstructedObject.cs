using System.Collections.Generic;
using UnityEngine;

public class ConstructedObject
{
    private List<PartialObject> parts = new List<PartialObject>();
    public void AddObject(PartialObject g)
    {
        parts.Add(g);
    }

    public GameObject Create()
    {
        GameObject myObject = new GameObject();
        parts.ForEach(x => x.Create(myObject.transform));
        return myObject;
    }
}
