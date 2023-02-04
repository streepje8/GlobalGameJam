using UnityEngine;

public class DebugInteractable : MonoBehaviour,IInteractable
{
    public void OnInteract()
    {
        Debug.Log("POG");
    }
}
