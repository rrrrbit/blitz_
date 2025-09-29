using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
public class TrajectoryAffectable : GAME_obj
{
    public bool solid;
    public virtual IEnumerable<Trajectory> Trajectories()
    {
        return Trajectories(); 
    }
    public virtual void Start()
    {
        GAME.spawns.allTrajectories.AddRange(Trajectories());
    }
}

public class Trajectory
{
    public Transform origin;
    public Vector2 startPos;
    public float maxDistX;

    public Vector3 Evaluate(float x)
    {
        return new(x, -GAME.plyrMvt.jumpHeight * Mathf.Pow(2 * x / GAME.plyrMvt.JumpLength(), 2), 0);
    }
    public float InverseEvaluate(float y)
    {
        return Mathf.Sqrt(-y / GAME.plyrMvt.jumpHeight) * GAME.plyrMvt.JumpLength() / 2;
    }

    public bool CanLandOn(TrajectoryAffectable trajectoryAffectable)
    {
        if (!trajectoryAffectable.solid)
        {
            return false;
        }
        return CanLandOn(trajectoryAffectable);
    }
}
