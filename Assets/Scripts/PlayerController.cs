using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Speed")]
    [SerializeField] private float movementSpeed = 10f;

    [Header("Controls")]
    [SerializeField] private KeyCode forwardKey;
    [SerializeField] private KeyCode rightKey;
    [SerializeField] private KeyCode leftKey;
    [SerializeField] private KeyCode backKey;
    [SerializeField] private KeyCode pickupKey; // New key to pick up and drop the box

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
            playerInputIndex = 1;
        }
        if (gameObject.CompareTag("Player2"))
        {
            playerInputIndex = 2;
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
            Debug.Log(movementDirection);
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

        // Handle box pickup
        if (Input.GetKeyDown(pickupKey))
        {
            if (heldBox == null)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.forward, out hit, 3f))
                {
                    if (hit.collider.CompareTag("Box"))
                    {
                        Debug.Log("Box hit by raycast!");
                        heldBox = hit.collider.gameObject;
                        heldBox.GetComponent<Rigidbody>().isKinematic = true;
                        heldBox.transform.SetParent(transform);
                        heldBox.transform.localPosition = Vector3.forward * 1.5f;
                    }
                }
            }
            else
            {
                heldBox.GetComponent<Rigidbody>().isKinematic = false;
                heldBox.transform.SetParent(null);
                heldBox = null;
            }
            Debug.Log("Box picked up or dropped!");
        }
    }

    private void OnMove(InputValue moveDirection)
    {
        Debug.Log(moveDirection.Get<Vector2>().normalized);
        Debug.Log(movementDirection);

        Vector2 controllerMove = moveDirection.Get<Vector2>().normalized;
        
        movementDirection = Vector3.zero;
        if (controllerMove != Vector2.zero) 
        {
            if (moveDirection.Get<Vector2>().y >= 0)
            {
                movementDirection += Vector3.forward;
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            if (moveDirection.Get<Vector2>().y <= 0)
            {
                movementDirection += Vector3.back;
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            if (moveDirection.Get<Vector2>().x <= 0)
            {
                movementDirection += Vector3.left;
                transform.rotation = Quaternion.Euler(0, 270, 0);
            }
            if (moveDirection.Get<Vector2>().x >= 0)
            {
                movementDirection += Vector3.right;
                transform.rotation = Quaternion.Euler(0, 90, 0);
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

        movementDirection.Normalize();
        transform.position += movementDirection * Time.deltaTime * movementSpeed;
    }
}