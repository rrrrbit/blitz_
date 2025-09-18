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

    [SerializeField] float boostForce;
    [Space]
    [SerializeField] float jumpForce;
    [SerializeField] float grav;


    int maxJumps = 1;

    Rigidbody2D rb;
	PLAYER_anim anim;

    InputSystem_Actions.PlayerActions actions;

    float lrControl;

    public bool grounded;
    [SerializeField] int jumps = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Setup();
    }

    void Setup()
    {
        jumpForce = 4f / jumpTime * jumpHeight;
        grav = -8f / jumpTime / jumpTime * jumpHeight;

        actions = new InputSystem_Actions().player;
        actions.Enable();

        rb = GetComponent<Rigidbody2D>();
		anim = GetComponent<PLAYER_anim>();

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

        if (grounded) { jumps = maxJumps; }
    }

    // Update is called once per frame
    void Update()
    {
        lrControl = actions.lr.ReadValue<float>();
        if (actions.jump.WasPressedThisFrame() && jumps > 0)
        {
            rb.linearVelocityY = jumpForce;
            jumps -= 1;
        }
		
        if (actions.brake.WasPressedThisFrame()) { rb.linearVelocityX -= boostForce; }
        if (actions.boost.WasPressedThisFrame()) { rb.linearVelocityX += boostForce; }
        if (actions.down.WasPressedThisFrame()) { rb.linearVelocityY = -jumpForce; }

		anim.state = grounded ? PLAYER_anim.States.ground : PLAYER_anim.States.air;
    }
}
