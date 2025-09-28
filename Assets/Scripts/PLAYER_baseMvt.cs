using Unity.Burst;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.InputSystem;

public class PLAYER_baseMvt : MonoBehaviour
{
    [SerializeField] Collider2D groundCheck;

    [Header("Speed & Accel")]
    [SerializeField] float maxSpeed = 100f;
    [SerializeField] float accel = 50f;
    [Header("Jump")]
    [SerializeField] public float jumpHeight = 10f;
    [SerializeField] public float jumpTime = 0.75f;
    [SerializeField] float coyoteTime;
    [Header("Other Mvt")]
    public float boostForce;
    [Header("(Internal)")]
    [SerializeField] float jumpForce;
    [SerializeField] float grav;

    public float gravityMult = 1f;

    Rigidbody2D rb;
	PLAYER_anim anim;

    public Camera cam;

    InputSystem_Actions.PlayerActions actions;

    float lrControl;

    public bool grounded;

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
    }

    // Update is called once per frame
    void Update()
    {
        lrControl = actions.lr.ReadValue<float>();
        if (actions.jump.IsPressed() && grounded)
        {
            Jump();
        }
		
        if (actions.brake.WasPressedThisFrame()) { rb.linearVelocityX -= boostForce; }
        if (actions.boost.WasPressedThisFrame()) { rb.linearVelocityX += boostForce; }
        if (actions.down.WasPressedThisFrame()) { rb.linearVelocityY = -jumpForce; }

        rb.gravityScale = grav * gravityMult / Physics2D.gravity.y;

        anim.state = grounded ? PLAYER_anim.States.ground : PLAYER_anim.States.air;
    }

    public void Jump()
    {
        rb.linearVelocityY = jumpForce;
    }

    public float JumpLength() => jumpTime * GAME.mgr.speed;
	public float Trajectory(float startFactor, float dist) => 4*jumpHeight*dist/JumpLength()*(1-2*startFactor-dist/JumpLength());
}