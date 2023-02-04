using System.Collections.Generic;

[System.Serializable]
public class NodeGraph
{
    public RootNode rootNode = new RootNode();
    public List<Node> nodes = new List<Node>();

    public NodeGraph()
    {
        nodes.Add(rootNode);
    }

    public void AddNode(Node n)
    {
        if(!n.isInitialized) n.Init();
        nodes.Add(n);
    }

    public void Connect(Node outputtingNode, int outputID, Node proccesingNode, int inputID) => proccesingNode.connections.Add(new Connection(proccesingNode.inputs[inputID],
            outputtingNode.outputs[outputID]));

    public ConstructedObject ExecuteGraph() => rootNode.Execute<ConstructedObject>().Item2;
}
