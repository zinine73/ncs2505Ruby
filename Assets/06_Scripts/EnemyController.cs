using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    readonly int hashMoveX = Animator.StringToHash("MoveX");
    readonly int hashMoveY = Animator.StringToHash("MoveY");
    readonly int hashFixed = Animator.StringToHash("Fixed");

    [SerializeField] float speed;
    [SerializeField] bool vertical;
    [SerializeField] float changeTime = 3.0f;
    [SerializeField] AudioClip fixedClip;
    [SerializeField] AudioClip[] gotHitClip;
    [SerializeField] int enemyHealth = 3;
    [SerializeField] PlayerController playerCtrl;
    [SerializeField] ParticleSystem smokeEffect;
    Rigidbody2D rb2d;
    float timer;
    int direction = 1;
    Animator animator;
    bool broken = true;
    AudioSource audioSource;
    void Start()
    {
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        timer = changeTime;
    }
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            direction = -direction;
            timer = changeTime;
            if (direction > 0)
            {
                vertical = Random.Range(0, 100) > 50;
            }
        }
    }
    void FixedUpdate()
    {
        if (!broken)
        {
            return;
        }

        Vector2 position = rb2d.position;
        if (vertical)
        {
            position.y += speed * direction * Time.deltaTime;
            animator.SetFloat(hashMoveX, 0);
            animator.SetFloat(hashMoveY, direction);
        }
        else
        {
            position.x += speed * direction * Time.deltaTime;
            animator.SetFloat(hashMoveX, direction);
            animator.SetFloat(hashMoveY, 0);
        }
        rb2d.MovePosition(position);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player
        = collision.gameObject.GetComponent<PlayerController>();
        if (player != null)
        {
            player.ChangeHealth(-1);
            player.PlayGotHitSound();
        }
    }

    public void Fix()
    {
        if (--enemyHealth > 0)
        {
            int index = Random.Range(0, gotHitClip.Length);
            audioSource.PlayOneShot(gotHitClip[index]);
            return;
        }
        // 적이 파괴되는 경우
        //Destroy(gameObject);
        broken = false;
        rb2d.simulated = false;
        animator.SetTrigger(hashFixed);
        audioSource.Stop();
        audioSource.PlayOneShot(fixedClip);
        smokeEffect.Stop();
        playerCtrl.FixedEnemy--;
    }
}
