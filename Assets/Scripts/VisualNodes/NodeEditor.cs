using System.Collections.Generic;
using UnityEngine;

public class NodeEditor : MonoBehaviour
{
    public bool isOpen = false;
    public RectTransform nodeSpace;
    public GameObject nodePrefab;
    public GameObject connectionPrefab;
    public NodeEditableObject obj;
    public RectTransform boi;
    public float padding = 400;
    public Vector2 NodesStartingPoint = new Vector2(1920/2f - 500, 0);
    public LineRenderer connectionInProgressLine;

    public Dictionary<Node, VisualNode> visuals = new Dictionary<Node, VisualNode>();
    public bool isConnecting = false;

    private IOComponent currentConnector;
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && obj != null) {
            boi.gameObject.SetActive(false);
            obj.ReEvaluateGraph();
            obj = null; RegenerateUI();
            GameController.Instance.controller.enabled = true;
            isOpen = false;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            connectionInProgressLine.enabled = false;
        }

        if (currentConnector != null)
        {
            Vector3 pointOne = currentConnector.cirkel.transform.position -
                currentConnector.cirkel.transform.rotation * Vector3.forward * 0.01f;
            Vector2 mouseUV = GameController.Instance.highResCam.ScreenToViewportPoint(Input.mousePosition);
            mouseUV -= new Vector2(0.5f,0.5f);
            mouseUV.y *= 0.581f;
            mouseUV *= 2;
            connectionInProgressLine.SetPositions(new Vector3[]
            {
                pointOne,
                boi.transform.position + (boi.forward * 0.0001f)+ boi.transform.rotation * new Vector3(mouseUV.x,mouseUV.y,0)
            });
        }
    }
    
    public void StartConnection(IOComponent ioComponent)
    {
        isConnecting = true;
        currentConnector = ioComponent;
        connectionInProgressLine.enabled = true;
    }

    public void Open(NodeEditableObject obj)
    {
        boi.gameObject.SetActive(true);
        this.obj = obj;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        RegenerateUI();
        GameController.Instance.controller.enabled = false;
        isOpen = true;
    }

    public void RegenerateUI()
    {
        for (int i = 0; i < nodeSpace.childCount; i++)
        {
            Destroy(nodeSpace.GetChild(i).gameObject);
        }
        visuals = new Dictionary<Node, VisualNode>();

        float totalPadding = padding;
        float yPadding = -1080 / 2f / 2f;
        if (obj != null)
        {
            obj.graph.nodes.ForEach(x =>
            {
                Vector2 pos = NodesStartingPoint - new Vector2(totalPadding, 0);
                if (pos.x < (-(1920 / 2f) + 300))
                {
                    pos.x = (-(1920 / 2f) + 300);
                    pos.y = yPadding;
                    yPadding += (1080/2f/2f) * 2f;
                }
                Instantiate(nodePrefab,nodeSpace).GetComponent<VisualNode>().SetNode(x).Move(pos);
                totalPadding += padding;
            });
            obj.graph.nodes.ForEach(x =>
            {
                x.connections.ForEach(y =>
                {
                    Node startNode = x;
                    Node endNode = y.nOutput.node;
                    VisualNode startVNode = visuals[startNode];
                    VisualNode endVNode = visuals[endNode];
                    IOComponent startIOC = startVNode.GetIOC(y.nInput);
                    IOComponent endIOC = endVNode.GetIOC(y.nOutput);
                    startIOC.SetConnected(true);
                    endIOC.SetConnected(true);
                    VNodeConnection con = Instantiate(connectionPrefab, startVNode.transform).GetComponent<VNodeConnection>();
                    con.start = startIOC;
                    con.end = endIOC;
                    con.Init(y);
                });
            });
        }
    }

    public void FinishConnection(IOComponent ioComponent)
    {
        IOComponent end = currentConnector;
        IOComponent start = ioComponent;
        NInput input = start.input;
        NOutput output = end.output;
        connectionInProgressLine.enabled = false;
        if (input.type == output.expectedType && !end.Connected)
        {
            isConnecting = false;
            connectionInProgressLine.enabled = false;
            currentConnector = null;
            Node endNode = end.node;
            Node startNode = start.node;
            obj.graph.Connect(endNode, endNode.FindOutputID(output), startNode, startNode.FindInputID(input));
            RegenerateUI();
        }
    }
}
