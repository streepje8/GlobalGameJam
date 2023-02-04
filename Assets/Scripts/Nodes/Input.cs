using System;

public class Input
{
    public string name;
    public Type type;
    public object defaultValue;

    public Input(string name, Type type, object defaultValue = null)
    {
        this.name = name;
        this.type = type;
        this.defaultValue = defaultValue;
    }
}
