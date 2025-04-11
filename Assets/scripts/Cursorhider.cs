using UnityEngine;

public class HideCursorOnClick : MonoBehaviour
{
    private bool cursorHidden = false;

    void Update()
    {
        // Press Escape to show cursor again
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            cursorHidden = false; // Reset so we can hide it again later
        }

        // Click to hide cursor if it's visible
        if (Input.GetMouseButtonDown(0) && !cursorHidden)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.None; // Or Locked, depending on your need
            cursorHidden = true;
        }
    }
}
