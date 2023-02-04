using UnityEngine;

public class NodeEditor : MonoBehaviour
{
    public bool isOpen = false;
    public RectTransform nodeSpace;
    public GameObject nodePrefab;
    public NodeEditableObject obj;
    public RectTransform boi;
    public float padding = 400;
    public Vector2 NodesStartingPoint = new Vector2(1920/2f - 500, 0);
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            boi.gameObject.SetActive(false); 
            obj = null; 
            RegenerateUI();
            GameController.Instance.controller.enabled = true;
            isOpen = false;
        }
    }

    public void Open(NodeEditableObject obj)
    {
        boi.gameObject.SetActive(true);
        this.obj = obj;
        Cursor.lockState = CursorLockMode.None;
        RegenerateUI();
        GameController.Instance.controller.enabled = false;
        isOpen = true;
    }

    private void RegenerateUI()
    {
        for (int i = 0; i < nodeSpace.childCount; i++)
        {
            Destroy(nodeSpace.GetChild(i).gameObject);
        }

        float totalPadding = padding;
        if (obj != null)
        {
            obj.graph.nodes.ForEach(x =>
            {
                Instantiate(nodePrefab,nodeSpace).GetComponent<VisualNode>().SetNode(x).Move(NodesStartingPoint - new Vector2(totalPadding,0));
                totalPadding += padding;
            });
            
        }
    }
}
