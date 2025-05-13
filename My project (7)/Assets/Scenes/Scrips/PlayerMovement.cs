using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 4f;
    public float runSpeed = 8f;
    public float jumpHeight = 1.5f;
    public float gravity = -20f;

    public Transform cameraHolder;

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;

    private float originalWalkSpeed;
    private float originalRunSpeed;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        originalWalkSpeed = walkSpeed;
        originalRunSpeed = runSpeed;
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Verificar si está en el suelo
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // Movimiento WASD
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;

        float speed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;
        controller.Move(move * speed * Time.deltaTime);

        // Salto
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Gravedad
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    // Métodos públicos para manipular desde habilidades
    public void MultiplicarVelocidad(float factor)
    {
        walkSpeed *= factor;
        runSpeed *= factor;
    }

    public void RestaurarVelocidad()
    {
        walkSpeed = originalWalkSpeed;
        runSpeed = originalRunSpeed;
    }
}