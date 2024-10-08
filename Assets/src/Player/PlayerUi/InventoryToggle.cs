using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryToggle : MonoBehaviour
{
    public GameObject inventoryUI; // The GameObject for the inventory UI
    public GameObject pauseMenuUI; // The GameObject for the pause menu UI

    public TimeOpSave tos;

    public TextMeshProUGUI t1;
    public TextMeshProUGUI t2;

    private bool isInventoryOpen = false; // Tracks if the inventory is currently open
    private bool isPausedOpen = false; // Tracks if the game is currently paused

    void Start()
    {
        // Ensure the inventory UI is closed (inactive) when the game starts
        inventoryUI.SetActive(false);
        pauseMenuUI.SetActive(false);
    }

    void Update()
    {
        // Check if the "I" key is pressed
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventory();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Escape key pressed");
            
            TogglePause();
            ReloadText();
        }
    }

    void ToggleInventory()
    {
        isInventoryOpen = !isInventoryOpen; // Toggle the inventory state

        // Set the inventory UI active state based on the toggle
        inventoryUI.SetActive(isInventoryOpen || isPausedOpen);

        // Show or hide the cursor based on the inventory state
        if (isInventoryOpen || isPausedOpen)
        {
            Cursor.lockState = CursorLockMode.None; // Unlock the cursor
            Cursor.visible = true; // Make the cursor visible
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked; // Lock the cursor back to the center
            Cursor.visible = false; // Hide the cursor
        }
    }

     void TogglePause()
    {
        isPausedOpen = !isPausedOpen; // Toggle the inventory state

        // Set the inventory UI active state based on the toggle
        pauseMenuUI.SetActive(isPausedOpen || isInventoryOpen);

        // Show or hide the cursor based on the inventory state
        if (isPausedOpen || isInventoryOpen)
        {
            Cursor.lockState = CursorLockMode.None; // Unlock the cursor
            Cursor.visible = true; // Make the cursor visible
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked; // Lock the cursor back to the center
            Cursor.visible = false; // Hide the cursor
        }
    }

   void ReloadText()
{
    int minutesSinceStart = Mathf.FloorToInt(tos._TimeSinceStart / 60);
    int secondsSinceStart = Mathf.FloorToInt(tos._TimeSinceStart % 60);

    int minutesGenTimeActive = Mathf.FloorToInt(tos._GenTimeActive / 60);
    int secondsGenTimeActive = Mathf.FloorToInt(tos._GenTimeActive % 60);

    // Setze den Text mit Minuten und Sekunden
    t1.text = string.Format("{0} min {1} sec", minutesSinceStart, secondsSinceStart);
    t2.text = string.Format("{0} min {1} sec", minutesGenTimeActive, secondsGenTimeActive);
}
}
    