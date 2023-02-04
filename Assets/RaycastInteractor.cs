using UnityEngine;

public class RaycastInteractor : MonoBehaviour
{
    public LayerMask interactionLayer;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, Mathf.Infinity,
                    interactionLayer))
            {
                IInteractable[] interactables = hit.collider.gameObject.GetComponents<IInteractable>();
                if (interactables.Length > 0)
                {
                    foreach (var interactable in interactables)
                    {
                        interactable.OnInteract();
                    }
                }
            }
        }
    }
}
