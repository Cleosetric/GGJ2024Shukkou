using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Unity.VisualScripting;

public class PlayerController : MonoBehaviour
{
    public GameObject sprite;
    private Rigidbody2D rb;
    public float rotationSpeed = 5f;
    public float launchForce = 10f;
    public float zoomOutAmount = 5f;
    public float zoomSpeed = 1.5f;
    public float zoomDuration = 3f;
    private float originalOrthographicSize;
    private CinemachineVirtualCamera virtualCamera;
    private Coroutine zoomCoroutine;
    // public float raycastDistance = 1f;
 
    void Awake()
    {
        virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
        originalOrthographicSize = virtualCamera.m_Lens.OrthographicSize;
        rb = GetComponent<Rigidbody2D>();
    } 

    void Update()
    {
        RotateCylinder();

        
        //     RaycastHit2D hit = Physics2D.Raycast(transform.position, -transform.up, raycastDistance);
        //     Debug.DrawRay(transform.position, -transform.up * raycastDistance, Color.white);
        //     if (hit.collider != null)
        //     { 
        //         // Debug.Log(hit.collider.name + " hit by raycast");
        //         Debug.DrawRay(transform.position, -transform.up * raycastDistance, Color.red);

                // if (Input.GetMouseButtonDown(0)) // Left mouse button
                // {
                //     UpdateZoom();
                //     ApplyUpForce();
                // }
        // } 
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
            virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(virtualCamera.m_Lens.OrthographicSize, targetSize, elapsedTime / zoomDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        virtualCamera.m_Lens.OrthographicSize = targetSize;

        // Wait for a short duration
        yield return new WaitForSeconds(1f); // Adjust this value as needed

        // Zoom back in
        elapsedTime = 0f;
        while (elapsedTime < zoomDuration)
        {
            virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(virtualCamera.m_Lens.OrthographicSize, originalOrthographicSize, elapsedTime / zoomDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        virtualCamera.m_Lens.OrthographicSize = originalOrthographicSize;
    }

    void RotateCylinder()
    {
        sprite.transform.Rotate(transform.forward, rotationSpeed * Time.deltaTime);
    }

    public void ResetRotation(){
        sprite.transform.Rotate(new Vector3(0,0,-65), Space.Self);
    }

    public void ApplyUpForce()
    {
        UpdateZoom();
        rb.velocity = Vector2.zero;
        rb.AddForce(new Vector3(1,1,0) * launchForce, ForceMode2D.Impulse);
    }

    public void ApplyDownForce()
    {
        UpdateZoom();
        rb.velocity = Vector2.zero;
        rb.AddForce(new Vector3(1,-1,0) * launchForce, ForceMode2D.Impulse);
    }

    public void FreezeControl(){
        rb.isKinematic = true;
    }

    public void UnFreezeControl(){
        rb.isKinematic = false;
    }

    public bool IsPlayerDead(){
        if(rb.velocity.magnitude <= 1){
            return true;
        }
        return false;
    }
}
