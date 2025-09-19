using UnityEngine;

public class GAME_obj : MonoBehaviour
{
    protected virtual void FixedUpdate()
    {
        transform.position += Vector3.left * GAME_manager.manager.speed * Time.fixedDeltaTime;
    }
}
