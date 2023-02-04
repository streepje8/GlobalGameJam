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
    }

    public void SetNode(Node n)
    {
        node = n;
        if(!node.isInitialized) {node.Init(); node.isInitialized = true; }
        UpdateUI();
    }

    private void Update()
    {
        ((RectTransform)transform).localPosition = Vector3.Lerp(((RectTransform)transform).localPosition, goalPos, 10f * Time.deltaTime);
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

        node.inputs.ForEach(x =>
        {
            IOComponent IOC = Instantiate(InputPrefab, IOArea).GetComponent<IOComponent>();
            IOC.type = IO.Input;
            IOC.label.text = x.name;
            IOC.input = x;
        });
        node.outputs.ForEach(x =>
        {
            IOComponent IOC = Instantiate(OuputPrefab, IOArea).GetComponent<IOComponent>();
            IOC.type = IO.Output;
            IOC.label.text = x.name;
            IOC.output = x;
        });

        images.ForEach(x => x.color = nodeColor);
        RectTransform rectTransform = (RectTransform)transform;
        float IOYdelta = (node.inputs.Count * (((RectTransform)InputPrefab.transform).sizeDelta.y + padding) +
                          node.outputs.Count * (((RectTransform)OuputPrefab.transform).sizeDelta.y + padding));
        IOArea.sizeDelta = new Vector2(IOArea.sizeDelta.x, IOYdelta);
        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x,settingsArea.sizeDelta.y + IOYdelta + 50 + padding + padding);
        IOArea.transform.localPosition -= Vector3.up * (settingsArea.sizeDelta.y + padding);
        nodeName.text = node.GetType().Name;
    }

    public void Move(Vector2 eventDataDelta)
    {
        eventDataDelta.y -= ((RectTransform)transform).sizeDelta.y / 2f - 10f;
        goalPos = eventDataDelta;
    }
}
