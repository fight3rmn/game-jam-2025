using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10;
    public InputAction playerControls;

    enum Direction
    {
        UP, DOWN, LEFT, RIGHT,
        UP_LEFT, DOWN_LEFT, UP_RIGHT, DOWN_RIGHT,
        NONE
    }

    Dictionary<Direction, float> angles = new()
    {
        {Direction.UP, 90.0f},
        {Direction.DOWN, -90.0f},
        {Direction.LEFT, -180.0f},
        {Direction.RIGHT, 0.0f},
        {Direction.UP_LEFT, -45.0f},
        {Direction.DOWN_LEFT, -135.0f},
        {Direction.UP_RIGHT, 45.0f},
        {Direction.DOWN_RIGHT, 135.0f},
    };

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        playerControls.Enable();
    }

    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 input = playerControls.ReadValue<Vector2>();
        if (input != Vector2.zero)
        {
            transform.position += new Vector3(
                input.x * moveSpeed * Time.deltaTime,
                input.y * moveSpeed * Time.deltaTime, 0);

            UpdateRotation(InputDirection(input));
            //transform.rotation = Quaternion.LookRotation(input);
        }

        //Debug.Log(InputDirection(input));
    }

    void UpdateRotation(Direction direction)
    {
        Vector2 currentEulers = transform.eulerAngles;
        float angle = angles[direction];
        transform.localEulerAngles = new Vector3(currentEulers.x, currentEulers.y, angle); 
    }    

    Direction InputDirection(Vector2 input)
    {
        if (input.x == 0 && input.y > 0) return Direction.UP;
        if (input.x == 0 && input.y < 0) return Direction.DOWN;
        if (input.x < 0 && input.y == 0) return Direction.LEFT;
        if (input.x > 0 && input.y == 0) return Direction.RIGHT;
        if (input.x < 0 && input.y > 0) return Direction.UP_LEFT;
        if (input.x < 0 && input.y < 0) return Direction.DOWN_LEFT;
        if (input.x > 0  && input.y > 0) return Direction.UP_RIGHT;
        if (input.x > 0 && input.y < 0) return Direction.DOWN_RIGHT;
        return Direction.NONE;
    }

    void OnDisable()
    {
        playerControls.Disable();
    }
}