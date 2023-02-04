using System;

public class NOutput
{
    public string name;
    public Guid guid;
    public Node node;
    public Type expectedType;

    public NOutput(string name, Type type, Node outputter)
    {
        guid = Guid.NewGuid();
        this.name = name;
        expectedType = type;
        node = outputter;
    }
}
