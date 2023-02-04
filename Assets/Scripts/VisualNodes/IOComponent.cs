using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum IO {
    Input,
    Output
}
public class IOComponent : MonoBehaviour
{
    public IO type;
    public TMP_Text label;
    public RawImage cirkel;
    public NInput input = null;
    public NOutput output = null;
}
