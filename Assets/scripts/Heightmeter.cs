using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HeightMeter : MonoBehaviour
{
    public Transform target; // The object whose height we're tracking
    public TextMeshProUGUI heightText; // The TextMeshPro UI element
    public float groundLevel = 0f; // Adjust if the ground is not at 0

    void Update()
    {
        if (target != null && heightText != null)
        {
            float height = target.position.y - groundLevel;
            heightText.text = $"Height: {height:F2}m";
        }
    }
}
