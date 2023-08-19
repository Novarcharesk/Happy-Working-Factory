using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class PlayerController : MonoBehaviour
{
    [Header("Character Settings")]
    [SerializeField] private float movementSpeed = 10f;
    [SerializeField] public float kickForce;
    
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
    }

    private void Update()
    {
        kickForce = LobySettings.characterKickForce;

        if (Input.GetKey(forwardKey) || Input.GetKey(backKey) || Input.GetKey(leftKey) || Input.GetKey(rightKey))
        {
            isMoving = true;
            characterRB.velocity = Vector3.zero;

            characterRB.velocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * movementSpeed;
            Quaternion toRotation = Quaternion.LookRotation(characterRB.velocity, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, movementSpeed);
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
                gameObject.GetComponentInChildren<CharacterKick>().rightTriggerPressed = true;
            }
            else
            {
                gameObject.GetComponentInChildren<CharacterKick>().rightTriggerPressed = false;
            }

            if (playerGamepad.leftStick.IsActuated())
            {
                isMoving = true;
                Vector2 controllerMove = playerGamepad.leftStick.ReadValue();

                characterRB.velocity = new Vector3(controllerMove.x, 0, controllerMove.y) * movementSpeed;

                Debug.Log(playerGamepad.leftStick.ReadValue().x);
                Debug.Log(playerGamepad.leftStick.ReadValue().y);

                Quaternion toRotation = Quaternion.LookRotation(characterRB.velocity, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, movementSpeed);
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