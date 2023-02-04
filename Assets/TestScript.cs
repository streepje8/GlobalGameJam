using UnityEngine;

public class TestScript : MonoBehaviour
{
    
    void Start()
    {
        RootNode rootNode = new RootNode();
        ShapeNode shapeNode = new ShapeNode();
        rootNode.connections.Add(new Connection(rootNode.inputs[0],shapeNode.outputs[0]));
        ConstructedObject constructedObject = rootNode.Execute<ConstructedObject>().Item2;
        constructedObject.Create();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
