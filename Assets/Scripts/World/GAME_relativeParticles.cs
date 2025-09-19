using UnityEngine;
using System.Collections.Generic;

public class GAME_relativeParticles : MonoBehaviour
{
    [SerializeField] float multiplier = 1;
    ParticleSystem.VelocityOverLifetimeModule p;

    ParticleSystem.Particle[] particles;

    void Start()
    {
        p = GetComponent<ParticleSystem>().velocityOverLifetime;
        p.x = -GAME_manager.manager.speed * multiplier;
    }

    
    void Update()
    {
        p.x = -GAME_manager.manager.speed * multiplier;
    }
}
