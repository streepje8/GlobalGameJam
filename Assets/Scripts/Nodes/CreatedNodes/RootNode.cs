using System;
using System.Collections.Generic;

public class RootNode : Node
{

    public new List<Input> inputs = new List<Input>() { new Input("Final Object", typeof(PartialObject)) };
    public override object OnExecute()
    {
        ConstructedObject endResult = new ConstructedObject();
        if(connections.Count > 0) {
            endResult.AddObject(connections[0].GetData<PartialObjectBuild>());
        }
        return endResult;
    }
}
