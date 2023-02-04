using System;

public class NOutput
{
    public string name;
    public Node node;
    public Type expectedType;

    public NOutput(string name, Type type, Node outputter)
    {
        this.name = name;
        expectedType = type;
        node = outputter;
    }
}
