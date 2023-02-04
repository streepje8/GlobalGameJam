using System;

public class NInput
{
    public string name;
    public Type type;
    public object defaultValue;

    public NInput(string name, Type type, object defaultValue = null)
    {
        this.name = name;
        this.type = type;
        this.defaultValue = defaultValue;
    }
}
