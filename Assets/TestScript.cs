using UnityEngine;

public class TestScript : MonoBehaviour
{
    
    void Start()
    {
        NodeGraph graph = new NodeGraph();
        CubeNode cubeNode = new CubeNode();
        graph.AddNode(cubeNode);
        graph.Connect(cubeNode,0,graph.rootNode,0);

        ConstructedObject co = graph.ExecuteGraph();
        co.Create();
    }
}
