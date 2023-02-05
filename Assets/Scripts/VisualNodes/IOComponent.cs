using System;
using System.Collections.Generic;
using System.Linq;
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

    private Dictionary<Type, Color> colors = new Dictionary<Type, Color>()
    {
        { typeof(Vector3), Color.magenta },
        { typeof(PartialObject), Color.green},
        { typeof(PartialObjectBuild), Color.green},
        { typeof(ConstructedObject), Color.green},
        { typeof(float), Color.blue},
        { typeof(int), Color.cyan}
    };

    public void Init()
    {
        Type t = type == IO.Input ? input.type : output.expectedType;
        if (colors.Keys.Contains(t)) cirkel.color = colors[t];
    }
    
    public void SetConnected(bool b)
    {
        this.Connected = b;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        switch (type)
            {
                case IO.Input:
                    if (!node.isLocked)
                    {
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
