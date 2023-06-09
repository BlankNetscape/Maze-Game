using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    [HideInInspector] public float playerSpeed = 2.0f;
    [HideInInspector] public float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;
    private Vector3 move;

    public bool isMovable = true;

    private void Start()
    {
        // controller = gameObject.AddComponent<CharacterController>();
    }

    void Update()
    {
        if (!isMovable) return;

        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));


        /**
         * TODO: if (... &&  groundedPlayer)
         */
        // Changes the height position of the player..
        if (Input.GetButtonDown("Jump"))
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }


    }
    private void FixedUpdate()
    {
        if (!isMovable) return;

        controller.Move(move * Time.deltaTime * playerSpeed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
}