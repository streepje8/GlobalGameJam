using System;

public class NInput
{
    public string name;
    public Guid guid;
    public Type type;
    public object defaultValue;

    public NInput(string name, Type type, object defaultValue = null)
    {
        guid = Guid.NewGuid();
        this.name = name;
        this.type = type;
        this.defaultValue = defaultValue;
    }
}
