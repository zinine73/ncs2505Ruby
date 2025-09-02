using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public InputAction MoveAction;
    void Start()
    {
        //QualitySettings.vSyncCount = 0;
        //Application.targetFrameRate = 10;
        MoveAction.Enable();
    }

    void Update()
    {
        // if (Keyboard.current.upArrowKey.isPressed)

        Vector2 move = MoveAction.ReadValue<Vector2>();
        Debug.Log(move);
        Vector2 position =
            (Vector2)transform.position
            + move * 3.0f * Time.deltaTime;
        transform.position = position;
    }
}
