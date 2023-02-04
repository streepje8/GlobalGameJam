using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class GameController : Singleton<GameController>
{
    public NodeEditor editor;
    public FirstPersonController controller;
    public Camera highResCam;

    private void Awake()
    {
        Instance = this;
    }
}
