using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Parallax : MonoBehaviour
{
    private float length, startpos;
    public float parallexEffect;
    private CinemachineVirtualCamera cam;

    void Start()
    {
        cam = FindObjectOfType<CinemachineVirtualCamera>();
        startpos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }
    
    void Update()
    {
        float temp = (cam.transform.position.x * (1 - parallexEffect));
        float dist = (cam.transform.position.x * parallexEffect);
        transform.position = new Vector3(startpos + dist, transform.position.y, transform.position.z);
        if (temp > startpos + length) startpos += length;
        else if (temp < startpos - length) startpos -= length;
    }
}

// using UnityEngine;
// using System.Collections.Generic;

// public class Parallax : MonoBehaviour
// {
//     [System.Serializable]
//     public struct ParallaxLayer
//     {
//         public Transform background;
//         public float parallaxScale;
//     }

//     public List<ParallaxLayer> parallaxLayers; // List of all the backgrounds and their parallax scales
//     public float smoothing = 1f;               // How smooth the parallax effect will be

//     private Transform cam;                     // Reference to the main camera's transform
//     private Vector3 previousCamPos;            // The position of the camera in the previous frame

//     void Awake()
//     {
//         // Set up the camera reference
//         cam = Camera.main.transform;
//     }

//     void Start()
//     {
//         // The previous frame had the current frame's camera position
//         previousCamPos = cam.position;
//     }

//     void Update()
//     {
//         // For each background layer
//         foreach (ParallaxLayer layer in parallaxLayers)
//         {
//             // Calculate the parallax
//             float parallax = (previousCamPos.x - cam.position.x) * layer.parallaxScale;

//             // Calculate the target position
//             float backgroundTargetPosX = layer.background.position.x + parallax;

//             // Calculate the difference between the camera's current position and the previous frame's position
//             float camDeltaX = cam.position.x - previousCamPos.x;

//             // Get the width of the background layer
//             float backgroundWidth = GetBackgroundWidth(layer.background);

//             // If the camera has moved past the right boundary of the background layer
//             if (cam.position.x > layer.background.position.x + backgroundWidth / 2)
//             {
//                 // Move the background layer to the left boundary
//                 layer.background.position += Vector3.right * backgroundWidth;
//             }
//             // If the camera has moved past the left boundary of the background layer
//             else if (cam.position.x < layer.background.position.x - backgroundWidth / 2)
//             {
//                 // Move the background layer to the right boundary
//                 layer.background.position -= Vector3.right * backgroundWidth;
//             }
            
//             // Fade between current position and the target position using lerp
//             Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, layer.background.position.y, layer.background.position.z);
//             layer.background.position = Vector3.Lerp(layer.background.position, backgroundTargetPos, smoothing * Time.deltaTime);
//         }

//         // Set the previousCamPos to the camera's position at the end of the frame
//         previousCamPos = cam.position;
//     }

//     // Function to get the width of a background layer
//     float GetBackgroundWidth(Transform background)
//     {
//         // Check if the background has a sprite renderer
//         SpriteRenderer spriteRenderer = background.GetComponent<SpriteRenderer>();
//         if (spriteRenderer != null && spriteRenderer.sprite != null)
//         {
//             // Return the width of the sprite
//             return spriteRenderer.sprite.bounds.size.x * background.localScale.x;
//         }

//         // If the background doesn't have a sprite renderer, return 0
//         return 0;
//     }
// }
