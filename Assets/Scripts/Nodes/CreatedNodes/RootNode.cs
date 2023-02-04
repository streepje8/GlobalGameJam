using System.Collections.Generic;

public class RootNode : Node
{
    public override List<Input> inputs { get; } = new List<Input>() { new Input("Final Object", typeof(PartialObject)) };
    public override List<Output> outputs { get; protected set; } = new List<Output>();

    public override object OnExecute()
    {
        ConstructedObject endResult = new ConstructedObject();
        if(connections.Count > 0) {
            endResult.AddObject(connections[0].GetData<PartialObjectBuild>());
        }
        return endResult;
    }
}
