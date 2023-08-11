using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Android;

public class PlayerController : MonoBehaviour
{
    [Header("Character Settings")]
    [SerializeField] private float movementSpeed = 10f;
    [SerializeField] public float kickForce = 2f;

    [Header("Controls")]
    [SerializeField] private KeyCode forwardKey;
    [SerializeField] private KeyCode rightKey;
    [SerializeField] private KeyCode leftKey;
    [SerializeField] private KeyCode backKey;
    [SerializeField] public KeyCode kickKey;

    [Header("Bobbing")]
    [SerializeField] private float bobbingSpeed = 2f;
    [SerializeField] private float bobbingHeight = 0.1f;
    private Vector3 originalPosition;
    private bool isMoving = false;
    private GameObject heldBox; // Reference to the currently held box, if any
    private Vector3 movementDirection;

    [Header("Controller Settings")]
    [SerializeField] private PlayerInput playerControllerInput;
    private Vector2 controllerMoveDirection;
    private Gamepad playerGamepad;

    public InputControl playerInputControl;
    [SerializeField] private int playerInputIndex;

    private void Start()
    {
        originalPosition = transform.localPosition;

        if (gameObject.CompareTag("Player1"))
        {
            playerInputIndex = 0;
        }
        if (gameObject.CompareTag("Player2"))
        {
            playerInputIndex = 1;
        }

    }

    private void Update()
    {
        // Input handling for movement
        movementDirection = Vector3.zero;

        if (Input.GetKey(forwardKey) || Input.GetKey(backKey) || Input.GetKey(leftKey) || Input.GetKey(rightKey))
        {
            isMoving = true;
            // Handling the movement input
            if (Input.GetKey(forwardKey))
            {
                movementDirection += Vector3.forward;
                transform.rotation = Quaternion.Euler(0,0,0);
            }
            if (Input.GetKey(backKey))
            {
                movementDirection += Vector3.back;
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            if (Input.GetKey(leftKey))
            {
                movementDirection += Vector3.left;
                transform.rotation = Quaternion.Euler(0, 270, 0);
            }
            if (Input.GetKey(rightKey))
            {
                movementDirection += Vector3.right;
                transform.rotation = Quaternion.Euler(0, 90, 0);
            }

            // Normalize the movement direction to ensure consistent speed in all directions
            movementDirection.Normalize();
            transform.position += movementDirection * Time.deltaTime * movementSpeed;
        }
        else
        {
            isMoving = false;
        }

        var gamepad = Gamepad.all[playerInputIndex];
        if (gamepad == null)
            return; // No gamepad connected.

        if (gamepad.rightTrigger.wasPressedThisFrame)
        {
            // 'Use' code here
            Debug.Log("Kick");
        }

        if (gamepad.leftStick.IsActuated())
        {
            isMoving = true;
            Vector2 controllerMove = gamepad.leftStick.ReadValue();
            transform.position += new Vector3(controllerMove.x, 0, controllerMove.y) * Time.deltaTime * movementSpeed;

            Debug.Log(gamepad.leftStick.ReadValue().x);
            Debug.Log(gamepad.leftStick.ReadValue().y);

            if (gamepad.leftStick.ReadValue().x <= 0)
            {
                transform.rotation = Quaternion.Euler(0, 270, 0);
            }
            if (gamepad.leftStick.ReadValue().x >= 0)
            {
                transform.rotation = Quaternion.Euler(0, 90, 0);
            }
            if (gamepad.leftStick.ReadValue().y <= 0)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            if (gamepad.leftStick.ReadValue().y >= 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
        else
        {
            isMoving = false;
        }

        // Bobbing motion only when moving
        if (isMoving)
        {
            float newY = originalPosition.y + Mathf.Sin(Time.time * bobbingSpeed) * bobbingHeight;
            transform.localPosition = new Vector3(transform.localPosition.x, newY, transform.localPosition.z);
        }
    }
}