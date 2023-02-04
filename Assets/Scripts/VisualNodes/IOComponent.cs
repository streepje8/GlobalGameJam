using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum IO {
    Input,
    Output
}
public class IOComponent : MonoBehaviour,
    IPointerClickHandler
{
    public IO type;
    public TMP_Text label;
    public RawImage cirkel;
    public NInput input = null;
    public NOutput output = null;

    private VNodeConnection connection;
    public Node node;
    public bool Connected { get; private set; }

    public void SetConnected(bool b)
    {
        this.Connected = b;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        switch (type)
        {
            case IO.Input:
                if (Connected)
                {
                    connection.Disconnect();
                    GameController.Instance.editor.RegenerateUI();
                }
                else
                {
                    if (GameController.Instance.editor.isConnecting)
                    {
                        GameController.Instance.editor.FinishConnection(this);
                    }
                }
                break;
            case IO.Output:
                if (!Connected)
                {
                    GameController.Instance.editor.StartConnection(this);
                }
                break;
        }
    }

    public void SetConnection(VNodeConnection vNodeConnection)
    {
        connection = vNodeConnection;
    }
}
