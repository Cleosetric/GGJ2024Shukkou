using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;

public class PlatformSpawner : MonoBehaviour
{
    public GameObject[] platformPrefabs; // Array of platform prefabs
    public Transform spawnPoint; // Point where platforms will spawn
    public float platformLength = 20f; // Length of each platform piece
    private List<GameObject> platforms = new List<GameObject>();
    private CinemachineVirtualCamera virtualCamera;
    private float cameraBoundaryX;
    float spawnPointBoundaryX;

    void Start()
    {
        virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
        SpawnPlatform(0);
    }

    void Update()
    {
        // Check if camera boundary reaches outside the length of the platform
        // Debug.Log("is camera outside bound : "+IsCameraBoundaryOutsidePlatform());
        if (IsCameraBoundaryOutsidePlatform())
        {
            // Spawn platform
            SpawnPlatform(1);
        }
    }

    bool IsCameraBoundaryOutsidePlatform()
    {
        if (virtualCamera == null)
        {
            Debug.LogError("Cinemachine virtual camera not found.");
            return false;
        }

        // Calculate camera boundary position
        cameraBoundaryX = virtualCamera.transform.position.x + (virtualCamera.m_Lens.OrthographicSize * virtualCamera.m_Lens.Aspect);

        // Calculate spawn point boundary position
        spawnPointBoundaryX = spawnPoint.position.x - (platformLength / 2f);

        // Check if camera boundary is outside the spawn point boundary
        return cameraBoundaryX > spawnPointBoundaryX;
    }

    void SpawnPlatform(int index)
    {
        GameObject selectedPlatform;
        if(index == 0){
              selectedPlatform = platformPrefabs[0];
        }else{

         selectedPlatform = platformPrefabs[Random.Range(0, platformPrefabs.Length)];
        }
        // Randomly select a platform prefab

        // Spawn the platform at the spawn point
        GameObject newPlatform = Instantiate(selectedPlatform, spawnPoint.position, Quaternion.identity);

        
        // Move spawn point forward
        spawnPoint.position += Vector3.right * platformLength;

        // Destroy off-screen platforms
        DestroyOffScreenPlatforms();
    }

    void DestroyOffScreenPlatforms()
    {
        GameObject[] platforms = GameObject.FindGameObjectsWithTag("Platform");

        foreach (GameObject platform in platforms)
        {
            // Check if platform is off-screen
            if (platform.transform.position.x + platformLength < cameraBoundaryX - platformLength)
            {
                // Destroy platform
                Destroy(platform);
            }
        }
    }
}
