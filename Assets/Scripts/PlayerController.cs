using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class PlayerController : MonoBehaviour
{
    [Header("Character Settings")]
    [SerializeField] private float movementSpeed = 10f;
    [SerializeField] public float kickForce = 2f;
    
    private Rigidbody characterRB;
    public bool isSliding;

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
    private Vector3 movementDirection;

    [Header("Controller Settings")]
    [SerializeField] private PlayerInput playerControllerInput;
    public Gamepad playerGamepad;

    public InputControl playerInputControl;
    [SerializeField] private int playerInputIndex;

    private void Start()
    {
        kickForce = LobySettings.characterKickForce;
        
        originalPosition = transform.localPosition;
        characterRB = GetComponent<Rigidbody>();

        if (gameObject.CompareTag("Player1"))
        {
            playerInputIndex = 0;
        }
        if (gameObject.CompareTag("Player2"))
        {
            playerInputIndex = 1;
        }
        Debug.Log(playerInputIndex);
    }

    private void Update()
    {
        if (Input.GetKey(forwardKey) || Input.GetKey(backKey) || Input.GetKey(leftKey) || Input.GetKey(rightKey))
        {
            isMoving = true;
            characterRB.velocity = Vector3.zero;

            // Handling the movement input
            if (Input.GetKey(forwardKey))
            {
                transform.rotation = Quaternion.Euler(0,0,0);
                characterRB.velocity = Vector3.forward * movementSpeed;
            }
            if (Input.GetKey(backKey))
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
                characterRB.velocity = Vector3.back * movementSpeed;
            }
            if (Input.GetKey(leftKey))
            {
                transform.rotation = Quaternion.Euler(0, 270, 0);
                characterRB.velocity = Vector3.left * movementSpeed;
            }
            if (Input.GetKey(rightKey))
            {
                transform.rotation = Quaternion.Euler(0, 90, 0);
                characterRB.velocity = Vector3.right * movementSpeed;
            }
        }
        else
        {
            isMoving = false;
            if (!isSliding)
            {
                characterRB.velocity = Vector3.zero;
            }
        }

        // Bobbing motion only when moving
        if (isMoving)
        {
            float newY = originalPosition.y + Mathf.Sin(Time.time * bobbingSpeed) * bobbingHeight;
            transform.localPosition = new Vector3(transform.localPosition.x, newY, transform.localPosition.z);
        }

        if (Gamepad.all.Count != 0)
        {
            playerGamepad = Gamepad.all[playerInputIndex];
            if (playerGamepad == null)
                return;

            if (playerGamepad.rightTrigger.wasPressedThisFrame)
            {
                // 'Use' code here
                Debug.Log("Kick");
            }

            if (playerGamepad.leftStick.IsActuated())
            {
                isMoving = true;
                Vector2 controllerMove = playerGamepad.leftStick.ReadValue();
                //transform.position += new Vector3(controllerMove.x, 0, controllerMove.y) * Time.deltaTime * movementSpeed;

                characterRB.velocity = new Vector3(controllerMove.x, 0, controllerMove.y) * movementSpeed;

                Debug.Log(playerGamepad.leftStick.ReadValue().x);
                Debug.Log(playerGamepad.leftStick.ReadValue().y);

                if (playerGamepad.leftStick.ReadValue().x <= 0)
                {
                    transform.rotation = Quaternion.Euler(0, 270, 0);
                }
                if (playerGamepad.leftStick.ReadValue().x >= 0)
                {
                    transform.rotation = Quaternion.Euler(0, 90, 0);
                }
                if (playerGamepad.leftStick.ReadValue().y <= 0)
                {
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                }
                if (playerGamepad.leftStick.ReadValue().y >= 0)
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
}