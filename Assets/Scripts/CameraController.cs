using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public float cameraYPos = 1f;
    public float zoomOutAmount = 5f;
    public float zoomSpeed = 1.5f;
    public float zoomDuration = 3f;

    // public Transform cameraTarget;

    private Coroutine zoomCoroutine;
    private CinemachineVirtualCamera cam;
    private Transform player;
    private float originalOrthographicSize;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<CinemachineVirtualCamera>();
        player = GameManager.Instance.GetPlayer().transform;
        originalOrthographicSize = cam.m_Lens.OrthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.IsGameStarted()){
            transform.position = new Vector3(player.position.x, cameraYPos, -10);;
        }
    }

    public void UpdateZoom(){
        if (zoomCoroutine != null)
        {
            StopCoroutine(zoomCoroutine);
        }
        zoomCoroutine = StartCoroutine(ZoomInOut());
    }

    IEnumerator ZoomInOut()
    {
        // Zoom out
        float targetSize = zoomOutAmount;
        float elapsedTime = 0f;
        while (elapsedTime < zoomDuration)
        {
            cam.m_Lens.OrthographicSize = Mathf.Lerp(cam.m_Lens.OrthographicSize, targetSize, elapsedTime / zoomDuration);
            // cameraYPos = Mathf.Lerp(6, 14, elapsedTime / zoomDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        // cameraYPos = 14;
        cam.m_Lens.OrthographicSize = targetSize;

        // Wait for a short duration
        yield return new WaitForSeconds(1f); // Adjust this value as needed

        // Zoom back in
        elapsedTime = 0f;
        while (elapsedTime < zoomDuration)
        {
            cam.m_Lens.OrthographicSize = Mathf.Lerp(cam.m_Lens.OrthographicSize, originalOrthographicSize, elapsedTime / zoomDuration);
            // cameraYPos = Mathf.Lerp(14, 6, elapsedTime / zoomDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        cam.m_Lens.OrthographicSize = originalOrthographicSize;
        // cameraYPos = 6;
    }

    public void ResetCamera(){
        // if(cam == null){
        //     cam = GetComponent<Camera>();
        //     camPos = cam.transform;
        // }
        // cam.transform.position = new Vector3(14, cameraYPos, camPos.position.z)
        Vector3 newPosition = new Vector3(20, cameraYPos,-10);
        transform.position = newPosition;
    }

    public void BeginCamera(){
        // if(cam == null){
        //     cam = GetComponent<Camera>();
        //     camPos = cam.transform;
        // }
        // cam.transform.position = new Vector3(player.position.x,cameraYPos, camPos.position.z);
        Vector3 newPosition = new Vector3(player.position.x, cameraYPos,-10);
        transform.position = newPosition;
    }
}
