using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Win : Node
{
    public override List<NInput> inputs { get; } = new List<NInput>() { new NInput("AAAAAH", typeof(int),0),new NInput("Restart?", typeof(int),0) };
    public override List<NOutput> outputs { get; protected set; }

    public override void Init()
    {
        outputs = new List<NOutput>() { new NOutput("Cup", typeof(PartialObjectBuild), this) };
    }

    public override object OnExecute()
    {
        if (connections.Count > 1) SceneManager.LoadScene(0);
        return new PartialObject().Build();
    }
}