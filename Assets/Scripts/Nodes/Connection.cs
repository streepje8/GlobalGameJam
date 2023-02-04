public class Connection
{
    public NInput nInput;
    public NOutput nOutput;

    public Connection(NInput nInput, NOutput nOutput)
    {
        this.nInput = nInput;
        this.nOutput = nOutput;
    }
    
    public bool CanGetData<T>() => nOutput.node.Execute<T>().Item1;
    public T GetData<T>() => nOutput.node.Execute<T>().Item2;
}
