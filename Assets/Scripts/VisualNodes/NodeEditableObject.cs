using System;
using UnityEngine;

public enum StartGraph
{
    Cube,
    Cylinder,
    Capsule,
    Sphere,
    PlantDoor,
    Lights,
    Wall,
    ENDING
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
            case StartGraph.Lights:
                graph.rootNode.isLocked = true;
                LightNode lightNode = new LightNode();
                IntNode nodeA = new IntNode(0);
                IntNode nodeB = new IntNode(1);
                IntNode nodeC = new IntNode(-1);
                MaterialNode matdisNode = new MaterialNode();
                VectorNode vecnode = new VectorNode(new Vector3(3,5,1));
                graph.AddNode(lightNode); graph.AddNode(nodeA); 
                graph.AddNode(matdisNode); graph.AddNode(vecnode); graph.AddNode(nodeB); graph.AddNode(nodeC);  
                graph.Connect(lightNode,0,graph.rootNode,0);
                graph.Connect(nodeA,0,lightNode,0);
                graph.Connect(nodeB,0,matdisNode,0);
                break;
            case StartGraph.Wall:
                graph.rootNode.isLocked = true;
                MergeNode mergeNode = new MergeNode();
                ColliderNode colliderNode = new ColliderNode();
                VectorNode centerVecNode = new VectorNode(new Vector3(-1.34f,0,0)); //Change dit
                VectorNode sizeVecNode = new VectorNode(new Vector3(7.11999989f,0.819999993f,12.5299997f)); //Change dit
                WallNode wallNode = new WallNode();
                VectorNode scaleVecNode = new VectorNode(Vector3.one);
                MaterialNode wallMatNode = new MaterialNode();
                IntNode wallMatIndex = new IntNode(1); //Change dit
                graph.AddNode(mergeNode); graph.AddNode(colliderNode); graph.AddNode(wallNode); graph.AddNode(centerVecNode); graph.AddNode(sizeVecNode);
                graph.AddNode(wallMatNode); graph.AddNode(wallMatIndex); graph.AddNode(scaleVecNode);
                graph.Connect(mergeNode,0,graph.rootNode,0); //Part -> Part
                graph.Connect(colliderNode,0,mergeNode,0); //Part -> Part
                graph.Connect(wallNode,0,mergeNode,1); //Part -> Part
                graph.Connect(centerVecNode,0,colliderNode,0); //Vec3 -> Vec3
                graph.Connect(sizeVecNode,0,colliderNode,1); //Vec3 -> Vec3
                graph.Connect(scaleVecNode,0,wallNode,0); //Vec3 -> Vec3
                graph.Connect(wallMatNode,0,wallNode,1); //Mat -> Mat
                graph.Connect(wallMatIndex,0,wallMatNode,0); //Int -> Int
                break;
            case StartGraph.ENDING:
                graph.rootNode.isLocked = true;
                Congrats cccc = new Congrats();
                You yyyy = new You();
                Win wwww = new Win();
                graph.AddNode(wwww); graph.AddNode(yyyy); graph.AddNode(cccc);
                graph.Connect(wwww,0,graph.rootNode,0);
                graph.Connect(yyyy,0,wwww,0);
                graph.Connect(cccc,0,yyyy,0);
                break;
        }
        currentGameObject = graph.ExecuteGraph().Create();
        currentGameObject.transform.SetParent(transform);
        currentGameObject.transform.position = transform.position;
        currentGameObject.transform.rotation = transform.rotation;
        currentGameObject.transform.localScale = transform.localScale;
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
        currentGameObject.transform.localScale = transform.localScale;
        OnBuild?.Invoke(currentGameObject);
    }

    public event Action<GameObject> OnBuild;
}
