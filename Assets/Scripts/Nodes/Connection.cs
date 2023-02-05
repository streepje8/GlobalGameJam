public class Connection
{
    public Node inputOwner;
    public NInput nInput;
    public NOutput nOutput;

    public Connection(NInput nInput, NOutput nOutput, Node inputOwner)
    {
        this.nInput = nInput;
        this.nOutput = nOutput;
        this.inputOwner = inputOwner;
    }
    
    public bool CanGetData<T>() => nOutput.node.Execute<T>().Item1;
    public T GetData<T>() => nOutput.node.Execute<T>().Item2;

    public void Disconnect(Node input)
    {
        if (input.connections.Contains(this)) input.connections.Remove(this);
    }
}
