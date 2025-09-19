using UnityEngine;

public class GAME_relativeParticles : MonoBehaviour
{
    [SerializeField] float multiplier = 1;
    ParticleSystem.MainModule c;


    void Start()
    {
        c = GetComponent<ParticleSystem>().main;
        
    }

    
    void Update()
    {
        c.startSpeed = GAME_manager.manager.speed * multiplier;
    }
}
