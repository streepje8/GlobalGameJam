using System;
using System.Collections.Generic;
using System.Linq;

public abstract class Node
{
    public List<Input> inputs = new List<Input>();
    public List<Output> outputs = new List<Output>();
    public List<Connection> connections = new List<Connection>();

    public Tuple<bool,T> Execute<T>()
    {
        bool wasUnsafe = true;
        T result = default;
        try
        {
            result = (T)Convert.ChangeType(OnExecute(), typeof(T));
            wasUnsafe = false;
        }
        catch (InvalidCastException e) { wasUnsafe = true; }
        return new Tuple<bool, T>(wasUnsafe,result);
    }

    public abstract object OnExecute();

    public Connection FindConnection(Input i)
    {
        return connections.FirstOrDefault(x => x.input == i);
    }
}
