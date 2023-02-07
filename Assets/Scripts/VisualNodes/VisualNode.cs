using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class VisualNode : MonoBehaviour
{
    public TMP_Text nodeName;
    public RectTransform settingsArea;
    public RectTransform IOArea;
    public List<Image> images = new List<Image>();
    public Color nodeColor = Color.blue;

    public float padding = 5;

    public GameObject InputPrefab;
    public GameObject OuputPrefab;

    public Node node;


    public Vector2 goalPos;
    private void Awake()
    {
        goalPos = ((RectTransform)transform).localPosition;
        if (node != null && node.hadVisualBefore) goalPos = node.lastPosition;
    }

    public VisualNode SetNode(Node n)
    {
        node = n;
        nodeColor = node.color;
        if (node.isLocked) nodeColor = Color.grey;
        GameController.Instance.editor.visuals.Add(n,this);
        if(!node.isInitialized) {node.Init(); node.isInitialized = true; }
        UpdateUI();
        return this;
    }

    private void Update()
    {
        ((RectTransform)transform).localPosition = Vector3.Lerp(((RectTransform)transform).localPosition, goalPos, 30f * Time.deltaTime);
        node.lastPosition = ((RectTransform)transform).localPosition;
        node.hadVisualBefore = true;
    }

    private Dictionary<int, IOComponent> niocIn = new Dictionary<int, IOComponent>();
    private Dictionary<int, IOComponent> niocOut = new Dictionary<int, IOComponent>();
    
    public IOComponent GetIOC(NInput NIn)
    {
        int index = -1;
        for (var i = 0; i < node.inputs.Count; i++)
        {
            if (node.inputs[i].name.Equals(NIn.name,StringComparison.OrdinalIgnoreCase)) index = i;
        }
        if (niocIn.TryGetValue(index, out IOComponent val)) return val;
        return null;
    }

    public IOComponent GetIOC(NOutput NOut)
    {
        if (niocOut.TryGetValue(node.outputs.IndexOf(NOut), out IOComponent val)) return val;
        return null;
    }

    void UpdateUI()
    {
        for (int i = 0; i < IOArea.childCount; i++)
        {
            Destroy(IOArea.GetChild(0).gameObject);
        }
        for (int i = 0; i < settingsArea.childCount; i++)
        {
            Destroy(settingsArea.GetChild(0).gameObject);
        }
        
        //Do the controlls
        settingsArea.sizeDelta = Vector2.zero;

        niocIn = new Dictionary<int, IOComponent>();
        niocOut = new Dictionary<int, IOComponent>();

        for (var i = 0; i < node.inputs.Count; i++)
        {
            NInput x = node.inputs[i];
            IOComponent IOC = Instantiate(InputPrefab, IOArea).GetComponent<IOComponent>();
            niocIn.Add(i,IOC);
            IOC.type = IO.Input;
            IOC.label.text = x.name;
            IOC.input = x;
            IOC.node = node;
            IOC.Init();
        }
        for (var i = 0; i < node.outputs.Count; i++)
        {
            NOutput x = node.outputs[i];
            IOComponent IOC = Instantiate(OuputPrefab, IOArea).GetComponent<IOComponent>();
            niocOut.Add(i,IOC);
            IOC.type = IO.Output;
            IOC.label.text = x.name;
            IOC.output = x;
            IOC.node = node;
            IOC.Init();
        }

        images.ForEach(x => x.color = nodeColor);
        RectTransform rectTransform = (RectTransform)transform;
        float IOYdelta = (node.inputs.Count * (((RectTransform)InputPrefab.transform).sizeDelta.y + padding) +
                          node.outputs.Count * (((RectTransform)OuputPrefab.transform).sizeDelta.y + padding));
        IOArea.sizeDelta = new Vector2(IOArea.sizeDelta.x, IOYdelta);
        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x,settingsArea.sizeDelta.y + IOYdelta + 50 + padding + padding);
        IOArea.transform.localPosition -= Vector3.up * (settingsArea.sizeDelta.y + padding);
        nodeName.text = node.GetType().Name;
        if (node.hadVisualBefore) goalPos = node.lastPosition;
    }

    public VisualNode Move(Vector2 eventDataDelta)
    {
        eventDataDelta.y -= ((RectTransform)transform).sizeDelta.y / 2f - 10f;
        goalPos = eventDataDelta;
        return this;
    }
}
