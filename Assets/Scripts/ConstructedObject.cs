using System.Collections.Generic;
using UnityEngine;

public class ConstructedObject
{
    private List<PartialObjectBuild> parts = new List<PartialObjectBuild>();
    public void AddObject(PartialObjectBuild g)
    {
        parts.Add(g);
    }

    public GameObject Create()
    {
        GameObject myObject = new GameObject("Constructed Object");
        parts.ForEach(x => x.Create(myObject.transform));
        return myObject;
    }
}
