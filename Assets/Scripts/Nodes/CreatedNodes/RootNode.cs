using System.Collections.Generic;

[System.Serializable]
public class RootNode : Node
{
    public override List<Control> controls { get; } = new List<Control>();
    public override List<NInput> inputs { get; } = new List<NInput>() { new NInput("Final Object", typeof(PartialObject)) };
    public override List<NOutput> outputs { get; protected set; } = new List<NOutput>();

    public override object OnExecute()
    {
        ConstructedObject endResult = new ConstructedObject();
        if(connections.Count > 0) {
            endResult.AddObject(connections[0].GetData<PartialObjectBuild>());
        }
        return endResult;
    }
}
