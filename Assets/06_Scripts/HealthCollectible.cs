using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    // 회복시킬 체력값을 저장하는 변수
    // 값은 유니티에디터에서 변경 가능하도록
    //public int healthValue;
    [SerializeField] int healthValue;

    void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log($"Trigger:{collision}");
        PlayerController controller =
            collision.GetComponent<PlayerController>();
        if ((controller != null) &&
            (controller.maxHealth > controller.Health))
        {
            // 체력회복값 변수 전달
            controller.ChangeHealth(healthValue);
            Destroy(gameObject);
        }
    }
}
