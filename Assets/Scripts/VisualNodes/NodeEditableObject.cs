using UnityEngine;

public enum StartGraph
{
    Cube,
    Cylinder,
    Capsule,
    Sphere
}

public class NodeEditableObject : MonoBehaviour, IInteractable
{
    public NodeGraph graph;
    public StartGraph startGraphID = StartGraph.Cube;
    public GameObject currentGameObject;
    
    private void Start()
    {
        switch (startGraphID)
        {
            case StartGraph.Cube:
                CubeNode sn = new CubeNode();
                graph.AddNode(sn);
                graph.Connect(sn,0,graph.rootNode,0);
                break;
            case StartGraph.Cylinder:
                CylinderNode ccn = new CylinderNode();
                graph.AddNode(ccn);
                graph.Connect(ccn,0,graph.rootNode,0);
                break;
            case StartGraph.Capsule:
                CapsuleNode cn = new CapsuleNode();
                graph.AddNode(cn);
                graph.Connect(cn,0,graph.rootNode,0);
                break;
            case StartGraph.Sphere:
                SphereNode spn = new SphereNode();
                graph.AddNode(spn);
                graph.Connect(spn,0,graph.rootNode,0);
                break;
        }
        currentGameObject = graph.ExecuteGraph().Create();
        currentGameObject.transform.SetParent(transform);
        currentGameObject.transform.position = transform.position;
    }

    public void OnInteract()
    {
        GameController.Instance.editor.Open(this);
    }

    public void ReEvaluateGraph()
    {
        Destroy(currentGameObject);
        currentGameObject = graph.ExecuteGraph().Create();
        currentGameObject.transform.SetParent(transform);
        currentGameObject.transform.position = transform.position;
    }
}
