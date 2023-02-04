using UnityEngine;

public class TestScript : MonoBehaviour
{
    
    void Start()
    {
        NodeGraph graph = new NodeGraph();
        ShapeNode shapeNode = new ShapeNode();
        graph.AddNode(shapeNode);
        graph.Connect(shapeNode,0,graph.rootNode,0);

        ConstructedObject co = graph.ExecuteGraph();
        co.Create();
    }
}
