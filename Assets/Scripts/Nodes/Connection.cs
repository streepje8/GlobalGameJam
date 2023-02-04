public class Connection
{
    public Input input;
    public Output output;

    public Connection(Input input, Output output)
    {
        this.input = input;
        this.output = output;
    }
    
    public bool CanGetData<T>() => output.node.Execute<T>().Item1;
    public T GetData<T>() => output.node.Execute<T>().Item2;
}
