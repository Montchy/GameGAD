using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Talks : MonoBehaviour
{
    public List<string> talk; // Liste der Strings
    public TextMeshProUGUI t; // Das Textfeld zum Anzeigen
    public GameObject c; // Referenz auf das UI-Element
    public float f = 1f;

    void Start()
    {
        c.SetActive(false); // Setzt das UI-Element zu Beginn auf inaktiv
    }

    // Diese Methode wird aufgerufen, um die Coroutine zu starten
    public void Execute()
    {
        c.SetActive(true); // Aktiviert das UI-Element
        StartCoroutine(ShowTalks()); // Starte die Coroutine
    }

    IEnumerator ShowTalks()
    {
        foreach (string text in talk)
        {
            yield return StartCoroutine(TypeText(text)); // Ruft die TypeText Coroutine auf
            yield return new WaitForSeconds(f); // Wartet 5 Sekunden, nachdem der Text angezeigt wurde
        }

        c.SetActive(false); // Deaktiviert das UI-Element
        t.text = ""; // Leert das Textfeld
    }

    // Coroutine, um den Text Buchstabe für Buchstabe anzuzeigen
    IEnumerator TypeText(string text)
    {
        t.text = ""; // Leert das Textfeld bevor der neue Text angezeigt wird
        foreach (char letter in text)
        {
            t.text += letter; // Fügt jeden Buchstaben zum Textfeld hinzu
            yield return new WaitForSeconds(0.1f); // Warte zwischen den Buchstaben (0.1 Sekunden, kann angepasst werden)
        }
    }
}
