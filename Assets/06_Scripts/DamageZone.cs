using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
    [SerializeField] int damageValue;

    void OnTriggerStay2D(Collider2D collision)
    {
        PlayerController controller =
            collision.GetComponent<PlayerController>();
        if (controller != null)
        {
            // 체력감소값 변수 전달
            controller.ChangeHealth(damageValue);
        }
    }
}
