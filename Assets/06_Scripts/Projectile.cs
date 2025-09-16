using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Projectile : MonoBehaviour
{
    Rigidbody2D rb2d;
    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }
    public void Launch(Vector2 direction, float force)
    {
        rb2d.AddForce(direction * force);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger with " + collision.gameObject);
        Destroy(gameObject);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision with " + collision.gameObject);
        Destroy(gameObject);
    }
}
