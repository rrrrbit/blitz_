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
        foreach (var i in Trajectories())
        {
            GAME.spawns.allTrajectories.Add(i);
        }
    }
}

public class Trajectory
{
    public Transform origin;
    public Vector3 startPos;
    public float maxDistX;

    public Trajectory(Transform Origin, Vector2 StartPos, float MaxDistX)
    {
        origin = Origin;
        startPos = StartPos;
        maxDistX = MaxDistX;
    }

    public Vector3 AbsPos()
    {
        if (origin == null)
        {
            return startPos;
        }
        else
        {
            return origin.position + startPos;
        }
    } 

    public Vector3 Evaluate(float x)
    {
        return AbsPos() + new Vector3(x * maxDistX, -GAME.plyrMvt.jumpHeight * Mathf.Pow(2 * x * maxDistX / GAME.plyrMvt.JumpLength(), 2), 0);
    }
    public float InverseEvaluate(float y)
    {
        return AbsPos().x + Mathf.Sqrt(-y / GAME.plyrMvt.jumpHeight) * GAME.plyrMvt.JumpLength() / 2;
    }

    public bool CanLandOn(TrajectoryAffectable trajectoryAffectable)
    {
        if (!trajectoryAffectable.solid)
        {
            return false;
        }
        return trajectoryAffectable.bounds.min.x <= InverseEvaluate(trajectoryAffectable.bounds.max.y) &&
               InverseEvaluate(trajectoryAffectable.bounds.max.y) <= trajectoryAffectable.bounds.max.x;
    }

    public void Draw(int resolution = 5)
    {
        for (int i = 0; i < resolution; i++)
        {
            Debug.DrawLine(Evaluate((float)i / resolution), Evaluate((i+1f) / resolution), Color.red);
        }
    }
}
