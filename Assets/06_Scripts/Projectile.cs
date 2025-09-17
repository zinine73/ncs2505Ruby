using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Projectile : MonoBehaviour
{
    const float DESTROY_TIME = 5.0f;
    const float DESTROY_POS = 30.0f;
    float timer;
    Rigidbody2D rb2d;
    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        timer = DESTROY_TIME;
    }
    void Update()
    {
        // if (transform.position.magnitude > DESTROY_POS)
        // {
        //     Destroy(gameObject);
        // }
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            Destroy(gameObject);
        }
    }
    public void Launch(Vector2 direction, float force)
    {
        rb2d.AddForce(direction * force);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyController enemy =
            collision.GetComponent<EnemyController>();
        // if (enemy != null)
        // {
        //     enemy.Fix();
        // }
        enemy?.Fix();
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
