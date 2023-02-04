using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public abstract class Node
{
    public abstract List<Control> controls { get; }
    public abstract List<NInput> inputs { get;}
    public abstract List<NOutput> outputs { get; protected set; }
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
        catch (InvalidCastException) { wasUnsafe = true; Debug.LogError("Oopsie woopsie, the code is stukkie wukkie! Deze nodes hadden nooit geconnect moeten zijn ＼（〇_ｏ）／");}
        return new Tuple<bool, T>(wasUnsafe,result);
    }

    public virtual void Init() { }

    public abstract object OnExecute();

    public Connection FindConnection(NInput i)
    {
        return connections.FirstOrDefault(x => x.nInput == i);
    }
}
