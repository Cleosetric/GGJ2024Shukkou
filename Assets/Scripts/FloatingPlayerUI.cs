using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FloatingPlayerUI : MonoBehaviour
{
    public float minScale = 0.5f; // Minimum scale
    public float maxScale = 1f; // Maximum scale
    public float maxDistance = 20f; // Maximum distance at which the UI reaches its minimum scale
    public TextMeshProUGUI textDistance;
    public RectTransform floatingUI;

    private Transform playerTransform;
    private Camera cam;

    private void Start()
    {
        playerTransform = GameManager.Instance.GetPlayer().transform;
        cam = Camera.main;
        textDistance.SetText("");
        floatingUI.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (playerTransform == null || cam == null || floatingUI == null)
        {
            Debug.LogError("Missing references in FloatingUIController!");
            return;
        }

        // Get the player's position in world space
        Vector3 playerPosition = playerTransform.position;

        // Convert player position to viewport space
        Vector3 viewportPosition = cam.WorldToViewportPoint(playerPosition);

        // Check if the player is outside the camera's view (viewport)
        if (
            viewportPosition.x < 0
            || viewportPosition.x > 1
            || viewportPosition.y < 0
            || viewportPosition.y > 1
        )
        {
            // Debug.Log("Player is outside camera view.");

            // Calculate the vertical distance between player and camera
            float distance = Mathf.Abs(playerTransform.position.y - cam.transform.position.y);
            textDistance.SetText((distance / 100).ToString("F1") + " m");
            // Debug.Log("Distance : " + distance + " m");

            // Calculate the scale based on distance
            float scaleFactor = Mathf.Clamp(1f - (distance / maxDistance), minScale, maxScale);

            // Set the scale of the UI element
            floatingUI.localScale = new Vector3(scaleFactor, scaleFactor, 1f);
            // Update UI position and scale
            floatingUI.gameObject.SetActive(true);
        }
        else
        {
            // Debug.Log("Player is inside camera view.");
            floatingUI.gameObject.SetActive(false);
            textDistance.SetText("");
        }
    }
}
