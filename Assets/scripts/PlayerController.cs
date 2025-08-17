using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using Unity.VisualScripting;

public class PlayerController : MonoBehaviour
{
    public delegate void PlayerPositionHandler(float yPos, int sortOrder);
    public static PlayerPositionHandler playerPositionHandler;

    [SerializeField] float moveSpeed = 10;
    public InputAction playerControls;

    bool isTouchingDummy = false;
    Animator anim;
    int sortOrder;

    enum Direction
    {
        UP, DOWN, LEFT, RIGHT,
        UP_LEFT, DOWN_LEFT, UP_RIGHT, DOWN_RIGHT,
        IDLE
    }
    Direction currentDirection = Direction.IDLE;

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
        return Direction.IDLE;
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

    void OnEnable()
    {
        playerControls.Enable();
    }

    void Awake()
    {
        anim = GetComponent<Animator>();
        sortOrder = GetComponent<SpriteRenderer>().sortingOrder;
    }

    void Update()
    {
        Vector2 input = playerControls.ReadValue<Vector2>();
        Direction thisDirection = InputDirection(input);
        if (thisDirection != currentDirection)
        {
            currentDirection = thisDirection;
            //Debug.Log("new direction: " + currentDirection.ToString());
            anim.SetTrigger(currentDirection.ToString());
        }
        transform.position += new Vector3(
            input.x * moveSpeed * Time.deltaTime,
            input.y * moveSpeed * Time.deltaTime, 0);
        if (currentDirection != Direction.IDLE)
        {
            playerPositionHandler?.Invoke(transform.position.y, sortOrder);
        } 
    }

    void UpdateRotation(Direction direction)
    {
        Vector2 currentEulers = transform.eulerAngles;
        float angle = angles[direction];
        transform.localEulerAngles = new Vector3(currentEulers.x, currentEulers.y, angle); 
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Dummy"))
        {
            isTouchingDummy = true;
            ISabotagable dummy = collision.gameObject.GetComponent<ISabotagable>();
            dummy?.IsTouching(true);
        }
        else if (collision.gameObject.CompareTag("PowerUp"))
        {
            collision.gameObject.GetComponent<PowerUp>().Die();
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        ISabotagable dummy = collision.gameObject.GetComponent<ISabotagable>();
        dummy?.IsTouching(currentDirection != Direction.IDLE);
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Dummy"))
        {
            isTouchingDummy = false;
            ISabotagable dummy = collision.gameObject.GetComponent<ISabotagable>();
            dummy?.IsTouching(false);
        }
    }

    void OnDisable()
    {
        playerControls.Disable();
    }
}
