using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class GameController : Singleton<GameController>
{
    public NodeEditor editor;
    public FirstPersonController controller;
    public Camera highResCam;
    public MeshDatabase meshDatabase;

    private void Awake()
    {
        Instance = this;
    }
}
