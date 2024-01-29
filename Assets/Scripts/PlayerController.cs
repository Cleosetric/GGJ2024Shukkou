using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float rotationSpeed = 5f;
    public float launchForce = 10f;
    public float zoomOutAmount = 5f;
    public float zoomSpeed = 1.5f;
    public float zoomDuration = 3f;
    public float raycastDistance;
    public GameObject sprite;
    public TextMeshProUGUI textSpeed;
    private float originalOrthographicSize;
    private Rigidbody2D rb;
    private CinemachineVirtualCamera virtualCamera;
    private Coroutine zoomCoroutine;
    float previousSpeed;

    void Awake()
    {
        virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
        originalOrthographicSize = virtualCamera.m_Lens.OrthographicSize;
        rb = GetComponent<Rigidbody2D>();
        textSpeed.SetText("");
    } 

    void Update()
    {
        RotateCylinder();
        // RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, raycastDistance);
        // Debug.DrawRay(transform.position, Vector2.down * raycastDistance, Color.white);
        // if (hit.collider != null && hit.collider.CompareTag("Platform"))
        // {
        //     Debug.DrawRay(transform.position, Vector2.down * raycastDistance, Color.red);
        //     virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(virtualCamera.m_Lens.OrthographicSize, zoomOutAmount, Time.deltaTime * zoomDuration);
        // } 

        previousSpeed = rb.velocity.magnitude;
        string speed = Mathf.FloorToInt(previousSpeed).ToString();
        textSpeed.SetText(speed);
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
        float previousSpeed = rb.velocity.magnitude;
        rb.velocity = Vector3.zero;
        float accumulatedVelocityMagnitude = previousSpeed + launchForce;
        Vector3 launchDirection = new Vector3(1, 1, 0).normalized;
        Vector3 finalForce = launchDirection * accumulatedVelocityMagnitude;
        rb.AddForce(finalForce, ForceMode2D.Impulse);
    }

    public void ApplyDownForce()
    {
        UpdateZoom();
        float previousSpeed = rb.velocity.magnitude;
        rb.velocity = Vector3.zero;
        float accumulatedVelocityMagnitude = previousSpeed + launchForce;
        Vector3 launchDirection = new Vector3(1, -1, 0).normalized;
        Vector3 finalForce = launchDirection * accumulatedVelocityMagnitude;
        rb.AddForce(finalForce, ForceMode2D.Impulse);
    }

    public void FreezeControl(){
        rb.isKinematic = true;
    }

    public void UnFreezeControl(){
        rb.isKinematic = false;
    }

    public bool IsPlayerDead(){
        if(rb.velocity.magnitude < GameManager.Instance.minDeathSpeed){
            textSpeed.SetText("");
            return true;
        }
        return false;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other != null && other.gameObject.CompareTag("Platform")){
            UpdateZoom();
            Debug.Log("Touch Platform");
            rb.velocity = Vector3.zero;
            Vector3 launchDirection = new Vector3(1, -1, 0).normalized;
            Vector3 finalForce = launchDirection * previousSpeed;
            rb.AddForce(finalForce, ForceMode2D.Impulse);
        }
    }
}
