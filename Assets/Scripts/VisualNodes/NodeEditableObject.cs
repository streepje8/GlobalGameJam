using UnityEngine;

public enum StartGraph
{
    Cube,
    Cylinder,
    Capsule,
    Sphere,
    PlantDoor
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
                MaterialNode matNode = new MaterialNode();
                IntNode intNode = new IntNode();
                VectorNode vecNode = new VectorNode(new Vector3(1, 1, 1));
                graph.AddNode(sn);
                graph.AddNode(matNode);
                graph.AddNode(intNode);
                graph.AddNode(vecNode);
                graph.Connect(sn,0,graph.rootNode,0);
                graph.Connect(matNode,0,sn,1);
                graph.Connect(vecNode,0,sn,0);
                graph.Connect(intNode,0,matNode,0);
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
            case StartGraph.PlantDoor:
                graph.rootNode.isLocked = true;
                DoorNode doorNode = new DoorNode();
                IntNode hasDoorHandleNode = new IntNode(0);
                MaterialNode doorMatNode = new MaterialNode();
                IntNode materialInt = new IntNode(1);
                graph.AddNode(doorNode); graph.AddNode(hasDoorHandleNode); graph.AddNode(doorMatNode); graph.AddNode(materialInt);
                graph.Connect(doorNode,0,graph.rootNode,0);
                graph.Connect(doorMatNode,0,doorNode,1);
                graph.Connect(hasDoorHandleNode,0,doorNode,0);
                graph.Connect(materialInt,0,doorMatNode,0);
                break;
        }
        currentGameObject = graph.ExecuteGraph().Create();
        currentGameObject.transform.SetParent(transform);
        currentGameObject.transform.position = transform.position;
        currentGameObject.transform.rotation = transform.rotation;
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
        currentGameObject.transform.rotation = transform.rotation;
    }
}
