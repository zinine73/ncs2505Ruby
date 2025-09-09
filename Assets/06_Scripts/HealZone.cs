using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealZone : MonoBehaviour
{   
    [SerializeField] int healValue;

    void OnTriggerStay2D(Collider2D collision)
    {
        PlayerController controller =
            collision.GetComponent<PlayerController>();
        if ((controller != null) &&
            (controller.maxHealth > controller.Health))
        {
            // 체력회복값 변수 전달
            controller.ChangeHealth(healValue);
        }
    }
}
