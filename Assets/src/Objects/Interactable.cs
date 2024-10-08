using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    private Material originalMaterial;
    private Color originalColor;
    public float highlightIntensity = 1.5f;
    private bool isFollowing;
    public InventoryItem item;
    
    // Sound for pickup action
    public AudioClip pickupSound;
    private AudioSource audioSource;

    void Start()
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            originalMaterial = renderer.material;
            originalColor = originalMaterial.color;
        }

        // Initialize AudioSource component
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public virtual void Interact()
    {
        if (CompareTag("Interactable"))
        {
            ExecuteAttachedScripts();
        }
        else if (CompareTag("Collectable"))
        {
            Pickup();
        }
        else if (CompareTag("Holdable"))
        {
            ToggleFollow();
        }
    }

    public void Highlight()
    {
        if (originalMaterial != null)
        {
            Color highlightColor = originalColor * highlightIntensity;
            highlightColor.a = originalColor.a;
            originalMaterial.color = highlightColor;
        }
    }

    public void ResetHighlight()
    {
        if (originalMaterial != null)
        {
            originalMaterial.color = originalColor;
        }
    }

    private void ToggleFollow()
    {
        if (FollowObject.CurrentlyHeldObject != null && FollowObject.CurrentlyHeldObject != gameObject)
        {
            Debug.Log("Cannot hold more than one object at a time.");
            return;
        }

        isFollowing = !isFollowing;
        if (isFollowing)
        {
            if (GetComponent<FollowObject>() == null)
            {
                gameObject.AddComponent<FollowObject>();
            }
            FollowObject.CurrentlyHeldObject = gameObject;
        }
        else
        {
            if (GetComponent<FollowObject>() != null)
            {
                Destroy(GetComponent<FollowObject>());
            }
            FollowObject.CurrentlyHeldObject = null;
        }
    }

    private void ExecuteAttachedScripts()
    {
        MonoBehaviour[] attachedScripts = GetComponents<MonoBehaviour>();
        foreach (MonoBehaviour script in attachedScripts)
        {
            if (script != this)
            {
                var method = script.GetType().GetMethod("Execute");
                if (method != null)
                {
                    method.Invoke(script, null);
                }
            }
        }
    }

    void Pickup()
    {
        // Play the pickup sound
        if (pickupSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(pickupSound);
        }

        InventoryManager.Instance.Add(item);
        InventoryManager.Instance.ListItems();
        Destroy(gameObject);
    }
}
