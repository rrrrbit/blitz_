using UnityEngine;
using System.Collections.Generic;

public class GAME_relativeParticles : MonoBehaviour
{
    [SerializeField] float multiplier = 1;
    ParticleSystem.VelocityOverLifetimeModule p;

    void Start()
    {
        p = GetComponent<ParticleSystem>().velocityOverLifetime;
        p.x = -GAME.mgr.speed * multiplier;
    }

    
    void Update()
    {
        p.x = -GAME.mgr.speed * multiplier;
    }
}
