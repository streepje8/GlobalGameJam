using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Node
{
    public abstract List<Input> inputs { get;}
    public abstract List<Output> outputs { get; protected set; }
    public List<Connection> connections = new List<Connection>();
    public bool isInitialized = false;

    public Tuple<bool,T> Execute<T>()
    {
        bool wasUnsafe = true;
        T result = default;
        try
        {
            result = (T)OnExecute();
            wasUnsafe = false;
        }
        catch (InvalidCastException e) { wasUnsafe = true; Debug.LogError("Oopsie woopsie, the code is stukkie wukkie! Deze nodes hadden nooit geconnect moeten zijn ＼（〇_ｏ）／");}
        return new Tuple<bool, T>(wasUnsafe,result);
    }

    public virtual void Init() { }

    public abstract object OnExecute();

    public Connection FindConnection(Input i)
    {
        return connections.FirstOrDefault(x => x.input == i);
    }
}
