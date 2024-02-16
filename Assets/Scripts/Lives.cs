using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lives : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other != null && other.CompareTag("Player"))
        {
            AudioManager.instance.Play("Powerup");
            GameManager.Instance.AddLive();
            gameObject.SetActive(false);
        }
    }
}
