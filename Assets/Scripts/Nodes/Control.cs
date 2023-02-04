using UnityEngine;

public class Control
{
    public string label;
    public object value;

    public Control(string label, object defaultValue)
    {
        this.label = label;
        value = defaultValue;
    }

    public virtual void OnDrawControl()
    {
        Debug.Log("Debug drawcall: " + label + " == " + value.ToString());
    }
}
