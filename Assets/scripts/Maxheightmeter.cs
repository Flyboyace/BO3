using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MaxheightMeter : MonoBehaviour
{
    public Transform target; // The object whose height we're tracking
    public TextMeshProUGUI heightText; // The TextMeshPro UI element
    public float groundLevel = 0f; // Adjust if the ground is not at 0
    private float maxHeight = 0f; // Variable to store max height reached

    void Update()
    {
        if (target != null && heightText != null)
        {
            float currentHeight = target.position.y - groundLevel;

            // Update max height only if the new height is greater
            if (currentHeight > maxHeight)
            {
                maxHeight = currentHeight;
            }

            heightText.text = $"Max Height : {maxHeight:F2}m";
        }
    }
}