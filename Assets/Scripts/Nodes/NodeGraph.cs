using UnityEngine;

public class NodeGraph
{
    public RootNode rootNode = new RootNode();

    public void AddNode(Node n)
    {
        if(!n.isInitialized) n.Init();
    }

    public void Connect(Node outputtingNode, int outputID, Node proccesingNode, int inputID) => proccesingNode.connections.Add(new Connection(proccesingNode.inputs[inputID],
            outputtingNode.outputs[outputID]));

    public ConstructedObject ExecuteGraph() => rootNode.Execute<ConstructedObject>().Item2;
}
