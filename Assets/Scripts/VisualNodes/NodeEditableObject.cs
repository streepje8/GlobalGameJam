using UnityEngine;

public class NodeEditableObject : MonoBehaviour, IInteractable
{
    public NodeGraph graph;

    //Debug
    private void Awake()
    {
        ShapeNode sn = new ShapeNode();
        graph.AddNode(sn);
        graph.Connect(sn,0,graph.rootNode,0);
    }

    public void OnInteract()
    {
        GameController.Instance.editor.Open(this);
    }
}
