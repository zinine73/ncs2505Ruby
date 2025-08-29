using UnityEngine;

public class PlayerController : MonoBehaviour
{   
    void Start()
    {
        
    }

    void Update()
    {
        Vector2 position = transform.position;
        position.y += 0.001f;
        position.x -= 0.001f;
        transform.position = position;
    }
}
