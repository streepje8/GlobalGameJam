using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class GameController : Singleton<GameController>
{
    public NodeEditor editor;
    public FirstPersonController controller;
    public Camera highResCam;
    public MeshDatabase meshDatabase;
    public List<GameObject> LightsLevel;
    public GameObject inverseLightsLevel;
    public Material lightsMaterial;

    private void Awake()
    {
        Instance = this;
    }
}
