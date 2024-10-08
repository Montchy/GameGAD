using System.Collections;
using UnityEngine;

public class FreezeAndFall : MonoBehaviour
{
    public float freezeTime = 3f; // Zeit, für die das Objekt gefreezed ist
    public float fallTime = 3f; // Zeit, während der das Objekt fällt, bevor es zerstört wird

    private Rigidbody rb;
    private Renderer rend;
    private Color startColor = Color.green;
    private Color midColor = new Color(0.5f, 0f, 0.5f); // Lila
    private Color endColor = new Color(0.6f, 0.3f, 0f); // Braun

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rend = GetComponent<Renderer>();

        if (rb == null || rend == null)
        {
            Debug.LogError("Rigidbody or Renderer is missing on the object.");
            return;
        }

        // Starte das Verhalten des Objekts
        StartCoroutine(HandleObjectBehavior());
    }

    IEnumerator HandleObjectBehavior()
    {
        // Freeze: Position und Rotation einfrieren, Gravity deaktivieren
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezeAll;

        // Farbe von Grün zu Lila ändern
        yield return StartCoroutine(ChangeColor(startColor, midColor, freezeTime));

        // Nach der Freeze-Zeit: Gravity aktivieren, Constraints aufheben
        rb.useGravity = true;
        rb.constraints = RigidbodyConstraints.None;

        // Farbe von Lila zu Braun ändern während des Falls
        yield return StartCoroutine(ChangeColor(midColor, endColor, fallTime));

        // Objekt nach der Fallzeit zerstören
        Destroy(gameObject);
    }

    IEnumerator ChangeColor(Color startColor, Color endColor, float duration)
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            rend.material.color = Color.Lerp(startColor, endColor, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        rend.material.color = endColor; // Am Ende der Zeit auf die Ziel-Farbe setzen
    }
}
