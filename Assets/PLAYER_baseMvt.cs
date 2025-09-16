using Unity.Burst;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.InputSystem;

public class PLAYER_baseMvt : MonoBehaviour
{
    [SerializeField] Collider2D groundCheck;
    
    [SerializeField] float maxSpeed = 100f;
    [SerializeField] float accel = 50f;

    [SerializeField] float jumpHeight = 10f;
    [SerializeField] float jumpTime = 0.75f;

    [SerializeField] float jumpForce;
    [SerializeField] float grav;

    Rigidbody2D rb;

    InputAction lr;
    InputAction jump;
    InputAction brake;
    InputAction boost;
    InputAction down;
    float lrControl;
    [SerializeField] bool grounded;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Setup();
    }

    void Setup()
    {
        jumpForce = 4f / jumpTime * jumpHeight;
        grav = -8f / jumpTime / jumpTime * jumpHeight;

        lr = InputSystem.actions.FindAction("lr");
        jump = InputSystem.actions.FindAction("jump");

        rb = GetComponent<Rigidbody2D>();

        rb.gravityScale = grav / Physics2D.gravity.y;
    }


    private void FixedUpdate()
    {
        float targetVelX = lrControl * maxSpeed;
        grounded = groundCheck.IsTouchingLayers(8);

        if (rb.linearVelocityX < targetVelX)
        {
            rb.linearVelocityX += Mathf.Min(
                accel * Time.fixedDeltaTime,
                targetVelX - rb.linearVelocityX
                );
        }
        else if (rb.linearVelocityX > targetVelX)
        {
            rb.linearVelocityX -= Mathf.Max(
                accel * Time.fixedDeltaTime,
                targetVelX - rb.linearVelocityX
                );

        }

    }

    // Update is called once per frame
    void Update()
    {
        lrControl = lr.ReadValue<float>();
        if (jump.WasPressedThisFrame() && grounded)
        {
            rb.linearVelocityY = jumpForce;
        }
    }
}
