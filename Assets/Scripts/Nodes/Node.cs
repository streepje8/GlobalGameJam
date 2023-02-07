using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public abstract class Node
{
    public bool isLocked = false;
    public Color color = Color.blue;
    public abstract List<NInput> inputs { get;}
    public abstract List<NOutput> outputs { get; protected set; }
    public List<Connection> connections = new List<Connection>();
    public bool isInitialized = false;
    public Vector3 lastPosition = Vector3.zero;
    public bool hadVisualBefore = false;

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
        return connections.FirstOrDefault(x => x.nInput.name.Equals(i.name,StringComparison.OrdinalIgnoreCase));
    }

    public int FindOutputID(NOutput output)
    {
        for(int i = 0; i < outputs.Count; i++)
        {
            NOutput nOutput = outputs[i];
            if (nOutput.name.Equals(output.name, StringComparison.OrdinalIgnoreCase))
            {
                return i;
            }
        }
        Debug.LogWarning("Failed to bind an output ID!");
        return 0;
    }

    public int FindInputID(NInput input)
    {
        for(int i = 0; i < inputs.Count; i++)
        {
            NInput nInput = inputs[i];
            if (nInput.name.Equals(input.name, StringComparison.OrdinalIgnoreCase))
            {
                return i;
            }
        }
        Debug.LogWarning("Failed to bind an input ID!");
        return 0;
    }
}
