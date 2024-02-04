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

    public float raycastDistance;
    public GameObject sprite;
    public TextMeshProUGUI textSpeed;
    private Rigidbody2D rb;
    private float previousSpeed;
    private bool touchingGround;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        textSpeed.SetText("");
    } 

    void Update()
    {
        RotateCylinder();
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, raycastDistance);
        Debug.DrawRay(transform.position, Vector2.down * raycastDistance, Color.white);
        if (hit.collider != null && hit.collider.CompareTag("Platform"))
        {
            Debug.DrawRay(transform.position, Vector2.down * raycastDistance, Color.red);
            // virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(virtualCamera.m_Lens.OrthographicSize, zoomOutAmount, Time.deltaTime * zoomDuration);
            touchingGround = true;
        }else{
            touchingGround = false;
        }

        previousSpeed = rb.velocity.magnitude;
        string speed = Mathf.FloorToInt((previousSpeed -5)/10).ToString() + " m/s";
        textSpeed.SetText(speed);
    }

    public bool IsTouchingGround(){
        return touchingGround;
    }


    void RotateCylinder()
    {
        sprite.transform.Rotate(transform.forward, rotationSpeed * Time.deltaTime);
    }

    public void ApplyUpForce()
    {
        GameManager.Instance.UpdateZoom();
        float previousSpeed = rb.velocity.magnitude;
        rb.velocity = Vector3.zero;
        float accumulatedVelocityMagnitude = previousSpeed + launchForce;
        Vector3 launchDirection = new Vector3(1, 1, 0).normalized;
        Vector3 finalForce = launchDirection * accumulatedVelocityMagnitude;
        rb.AddForce(finalForce, ForceMode2D.Impulse);
    }

    public void ApplyDownForce()
    {
        GameManager.Instance.UpdateZoom();
        float previousSpeed = rb.velocity.magnitude;
        rb.velocity = Vector3.zero;
        float accumulatedVelocityMagnitude = previousSpeed + launchForce;
        Vector3 launchDirection = new Vector3(1, -1, 0).normalized;
        Vector3 finalForce = launchDirection * accumulatedVelocityMagnitude;
        rb.AddForce(finalForce, ForceMode2D.Impulse);
    }

    public void FreezeControl(){
        if(rb != null){
            rb.isKinematic = true;  
        }else{
            rb = GetComponent<Rigidbody2D>();
            rb.isKinematic = true;  
        }
    }

    public void UnFreezeControl(){
        if(rb != null){
            rb.isKinematic = false;
        } else{
            rb = GetComponent<Rigidbody2D>();
            rb.isKinematic = false;  
        }
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
            if(rb != null){
                rb.velocity = Vector3.zero;
            }else{
                rb = GetComponent<Rigidbody2D>();
                rb.velocity = Vector3.zero;
            }
            Vector3 launchDirection = new Vector3(1, -1, 0).normalized;
            Vector3 finalForce = launchDirection * previousSpeed;
            rb.AddForce(finalForce, ForceMode2D.Impulse);
        }
    }
}
