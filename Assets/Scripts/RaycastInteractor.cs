using UnityEngine;

public class RaycastInteractor : MonoBehaviour
{
    public LayerMask interactionLayer;
    public float reach = 5f;
    public GameObject hint;
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !GameController.Instance.editor.isOpen)
        {
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, reach,
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

        if (!GameController.Instance.editor.isOpen && Physics.Raycast(transform.position, transform.forward, out RaycastHit woot, reach,
                interactionLayer))
        {
            IInteractable[] interactables = woot.collider.gameObject.GetComponents<IInteractable>();
            if (interactables.Length > 0)
            {
                if(hint != null) hint.SetActive(true);
            }
        }
        else
        {
            if(hint != null) hint.SetActive(false);
        }
    }
}
