using UnityEngine;
using UnityEngine.EventSystems;

public class NodeTopBar : MonoBehaviour,IDragHandler
{
    public VisualNode visualNode;
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 mouseUV = GameController.Instance.highResCam.ScreenToViewportPoint(eventData.position);
        mouseUV *= new Vector2(1920, 1080);
        mouseUV -= new Vector2(1920 / 2f, 1080 / 2f);
        if(!visualNode.node.isLocked) visualNode.Move(mouseUV);
    }
}
