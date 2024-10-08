using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public TimeOpSave timeOpSave;

    void Start()
    {
        timeOpSave.Initialize(); // Initialisiere die Zeitwerte beim Start des Spiels
    }

    void Update()
    {
        timeOpSave.UpdateTime(Time.deltaTime); // Aktualisiere die Zeiten basierend auf der Zeit seit dem letzten Frame
    }

    void OnApplicationQuit()
    {
        timeOpSave.SaveTime(); // Speichere die Gesamtspielzeit beim Beenden des Spiels
    }
}
