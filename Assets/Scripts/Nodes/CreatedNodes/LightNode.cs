using System.Collections.Generic;
using UnityEngine;

public class LightNode : Node
{
    public override List<NInput> inputs { get; } = new List<NInput>()
    {
        new NInput("IsActive", typeof(int),0),
    };
    public override List<NOutput> outputs { get; protected set; }

    public override void Init()
    {
        GameController.Instance.LightsLevel.ForEach(x => x.SetActive(true));
        outputs = new List<NOutput>() { new NOutput("Light Object", typeof(PartialObject), this) };
    }

    public override object OnExecute()
    {
        LightPartialObject partialObject = new LightPartialObject();
        partialObject.isActive = FindConnection(inputs[0])?.GetData<int>() > 0;
        return partialObject.Build();
    }
}
public class LightPartialObject : PartialObject
{
    public bool isActive;
        
    public override void Create(Transform parent)
    {
        GameObject myHolder = new GameObject("Light");
        myHolder.transform.SetParent(parent);
        if (isActive)
        {
            GameController.Instance.LightsLevel.ForEach(x => x.SetActive(true));
            GameController.Instance.inverseLightsLevel.SetActive(false);
        }
        else
        {
            GameController.Instance.LightsLevel.ForEach(x => x.SetActive(false));
            GameController.Instance.inverseLightsLevel.SetActive(true);
        }
    }
}
