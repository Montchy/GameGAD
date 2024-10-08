using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TimeOpSave")]
public class TimeOpSave : ScriptableObject
{
    public float _DayTime;         // Aktuelle Tageszeit (im Spiel)
    public float _SavedDayTime;    // Gespeicherte Tageszeit (im Spiel)
    public float _SavedTime;       // Gespeicherte Zeit (z.B. für bestimmte Events)

    public int _DayTimeType;
    
    public float _TimeSinceStart;  // Zeit seit dem aktuellen Start der Spielsitzung
    public float _GenTimeActive;   // Gesamte Zeit, die das Spiel aktiv war (über alle Sitzungen hinweg)

    private const string GenTimeKey = "GenTimeActive"; // Key für PlayerPrefs zum Speichern der generellen Spielzeit

    // Initialisierung beim Start des Spiels
    public void Initialize()
    {
        _TimeSinceStart = 0f;

        // Lade die gespeicherte Zeit aus PlayerPrefs (falls vorhanden)
        _GenTimeActive = PlayerPrefs.GetFloat(GenTimeKey, 0f); // Lade gespeicherte Gesamtzeit oder 0, wenn nicht vorhanden
    }

    // Aktualisiere die Zeiten in jeder Spielsession
    public void UpdateTime(float deltaTime)
    {
        _TimeSinceStart += deltaTime;
        _GenTimeActive += deltaTime;
    }

    // Speichern der gesamten Spielzeit beim Beenden des Spiels
    public void SaveTime()
    {
        PlayerPrefs.SetFloat(GenTimeKey, _GenTimeActive);
        PlayerPrefs.Save(); // Speichert die Werte in den PlayerPrefs
    }
}
