using Unity.Burst;
using UnityEngine;
using UnityEngine.InputSystem;

public class PLAYER_baseMvt : MonoBehaviour
{
    [SerializeField] float lrSpeed = 100f;
    
    InputAction lr;
    InputAction jump;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lr = InputSystem.actions.FindAction("lr");
        jump = InputSystem.actions.FindAction("jump");
    }

    // Update is called once per frame
    void Update()
    {
        print(lr.ReadValue<float>());

        GetComponent<Rigidbody2D>().AddForce(new Vector2(lrSpeed*lr.ReadValue<float>()*Time.deltaTime, 0f));
    }
}
