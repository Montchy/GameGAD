using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    public float interactionDistance = 3f;
    public Camera playerCamera;

    private Interactable currentInteractable;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            TryInteract();
        }

        HighlightInteractable();
    }

    private void TryInteract()
    {
        if (currentInteractable != null)
        {
            currentInteractable.Interact();
        }
    }

    private void HighlightInteractable()
    {
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, interactionDistance))
        {
            Interactable interactable = hit.collider.GetComponent<Interactable>();
            if (interactable != null)
            {
                if (interactable != currentInteractable)
                {
                    if (currentInteractable != null)
                    {
                        currentInteractable.ResetHighlight();
                    }

                    currentInteractable = interactable;
                    currentInteractable.Highlight();
                }
            }
            else if (currentInteractable != null)
            {
                currentInteractable.ResetHighlight();
                currentInteractable = null;
            }
        }
        else if (currentInteractable != null)
        {
            currentInteractable.ResetHighlight();
            currentInteractable = null;
        }
    }
}
