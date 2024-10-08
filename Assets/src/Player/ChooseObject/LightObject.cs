using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightObject : MonoBehaviour
{
    private Material originalMaterial;
    private Renderer objectRenderer;
    private Material[] materialsBackup;
    private Color originalColor;

    public float highlightIntensity = 1.5f; // Intensität der Hervorhebung, wie hell das Objekt wird

    void Start()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Interactable") || other.CompareTag("Collectable") || other.CompareTag("Holdable"))
        {
            if (objectRenderer != null)
            {
                RestoreOriginalMaterials();
            }

            objectRenderer = other.GetComponent<Renderer>();
            if (objectRenderer != null)
            {
                materialsBackup = objectRenderer.materials;
                originalMaterial = materialsBackup[0];
                originalColor = originalMaterial.color;

                // Erstelle ein neues Material für die Highlight-Farbe
                Material[] newMaterials = new Material[materialsBackup.Length];
                for (int i = 0; i < newMaterials.Length; i++)
                {
                    newMaterials[i] = new Material(materialsBackup[i]);
                }
                
                // Setze die neue Farbe für das erste Material
                Color highlightColor = originalColor * highlightIntensity; 
                highlightColor = new Color(Mathf.Clamp01(highlightColor.r), Mathf.Clamp01(highlightColor.g), Mathf.Clamp01(highlightColor.b), originalColor.a);

                newMaterials[0].color = highlightColor;

                objectRenderer.materials = newMaterials;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Interactable") || other.CompareTag("Collectable") || other.CompareTag("Holdable"))
        {
            if (objectRenderer != null)
            {
                RestoreOriginalMaterials();
            }
        }
    }

    private void RestoreOriginalMaterials()
    {
        if (objectRenderer != null)
        {
            objectRenderer.materials = materialsBackup;
            objectRenderer = null;
        }
    }
}
