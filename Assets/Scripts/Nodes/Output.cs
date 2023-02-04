using System;

public class Output
{
    public string name;
    public Node node;
    public Type expectedType;

    public Output(string name, Type type, Node outputter)
    {
        this.name = name;
        expectedType = type;
        node = outputter;
    }
}
