using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    #region Const
    const int MIN_HEALTH = 0;
    const int MAX_HEALTH = 20;
    const int START_HEALTH = 5;
    const float MIN_SPEED = 1.0f;
    const float MAX_SPEED = 10.0f;
    const float START_SPEED = 3.0f;
    #endregion
    #region public
    public InputAction MoveAction;
    [Range(MIN_HEALTH, MAX_HEALTH)] public int maxHealth = START_HEALTH;
    [Range(MIN_SPEED, MAX_SPEED)] public float moveSpeed = START_SPEED;
    public float timeInvicible = 2.0f;
    public float timeHeal = 2.0f;
    #endregion

    #region Property
    // public int Health
    // {
    //     get { return currentHealth; }
    // }
    public int Health => currentHealth;
    #endregion

    #region private
    int currentHealth;
    Rigidbody2D rb2d;
    Vector2 move;
    bool isInvicible;
    float damageCooldown;
    bool isHealing;
    float healCooldown;
    Animator animator;
    Vector2 moveDirection = new Vector2(1, 0);
    #endregion

    #region Method
    void Start()
    {
        //QualitySettings.vSyncCount = 0;
        //Application.targetFrameRate = 10;
        MoveAction.Enable();
        rb2d = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // if (Keyboard.current.upArrowKey.isPressed)

        move = MoveAction.ReadValue<Vector2>();
        //if (move.x != 0.0f || move.y != 0.0f)
        if (!Mathf.Approximately(move.x, 0.0f)
        || !Mathf.Approximately(move.y, 0.0f))
        {
            moveDirection.Set(move.x, move.y);
            moveDirection.Normalize();
        }
        animator.SetFloat("Look X", moveDirection.x);
        animator.SetFloat("Look Y", moveDirection.y);
        animator.SetFloat("Speed", move.magnitude);
        
        //Debug.Log(move);
        if (isInvicible)
        {
            damageCooldown -= Time.deltaTime;
            if (damageCooldown < 0)
            {
                isInvicible = false;
            }
        }
        if (isHealing)
        {
            healCooldown -= Time.deltaTime;
            if (healCooldown < 0)
            {
                isHealing = false;
            }
        }
    }

    void FixedUpdate()
    {
        Vector2 position =
            (Vector2)rb2d.position
            + move * moveSpeed * Time.deltaTime;
        rb2d.MovePosition(position);
    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            if (isInvicible)
            {
                return;
            }
            isInvicible = true;
            damageCooldown = timeInvicible;
            animator.SetTrigger("Hit");
        }

        if (isHealing)
        {
            return;
        }
        else
        {
            isHealing = true;
            healCooldown = timeHeal;
        }

        currentHealth =
            Mathf.Clamp(currentHealth + amount,
            MIN_HEALTH, maxHealth);
        //Debug.Log($"{currentHealth}/{maxHealth}");
        UIHandler.instance.SetHealthValue(
            currentHealth / (float)maxHealth
        );
    }
    #endregion
}
