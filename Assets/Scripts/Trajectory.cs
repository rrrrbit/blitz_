using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting.FullSerializer;
public class TrajectoryAffectable : GAME_obj
{
    public bool solid;
    public virtual IEnumerable<Trajectory> Trajectories()
    {
        return Trajectories(); 
    }
    public override void Ready()
    {
        foreach (var i in Trajectories())
        {
            GAME.spawns.allTrajectories.Add(i);
            GAME.spawns.newTrajectories.Add(i);
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

    public Vector3? EvaluateAbs(float x)
    {
        if (x < AbsPos().x)
        {
            return null;
        }
        return new Vector3(x, AbsPos() .y -GAME.plyrMvt.jumpHeight * Mathf.Pow(2 * (x- AbsPos().x) / GAME.plyrMvt.JumpLength(), 2), 0);
    }
    public float? InverseEvaluate(float y)
    {
        if (y > AbsPos().y)
        {
            return null;
        }
        return AbsPos().x + Mathf.Sqrt((AbsPos().y-y) / GAME.plyrMvt.jumpHeight) * GAME.plyrMvt.JumpLength() / 2;
    }

    public bool CanLandOn(TrajectoryAffectable trajectoryAffectable)
    {
        var iey = InverseEvaluate(trajectoryAffectable.bounds.bounds.max.y);
        if (!trajectoryAffectable.solid || iey == null || trajectoryAffectable.transform == origin)
        {
            return false;
        }
        return trajectoryAffectable.bounds.bounds.min.x <= (float)iey &&
               (float)iey <= trajectoryAffectable.bounds.bounds.max.x;
    }

    public bool CanLandOnWithRange(TrajectoryAffectable trajectoryAffectable)
    {
        var iey = InverseEvaluate(trajectoryAffectable.bounds.bounds.max.y);
        if (!trajectoryAffectable.solid || iey == null || trajectoryAffectable.transform == origin)
        {
            return false;
        }
        return trajectoryAffectable.bounds.bounds.min.x <= (float)iey &&
               (float)iey <= trajectoryAffectable.bounds.bounds.max.x && 
               (iey < AbsPos().x + maxDistX);
    }

    public TrajectoryAffectable Landing(List<TrajectoryAffectable> world)
    {
        return world.Where(x => CanLandOnWithRange(x)).First();
    }

    public Vector3 EvaluateWithLanding(float x, List<TrajectoryAffectable>  world)
    {
        var landingXDist = InverseEvaluate(Landing(world).bounds.bounds.max.y) - AbsPos().x;

        if (landingXDist == null) { return Evaluate(x); }

        return AbsPos() + new Vector3(x * (float)landingXDist, -GAME.plyrMvt.jumpHeight * Mathf.Pow(2 * x * (float)landingXDist / GAME.plyrMvt.JumpLength(), 2), 0);
    }

    public bool WouldHit(TrajectoryAffectable trajectoryAffectable)
    {
        var ex = EvaluateAbs(trajectoryAffectable.bounds.bounds.min.x);
        if (!trajectoryAffectable.solid || ex == null || trajectoryAffectable.transform == origin)
        {
            return false;
        }
        return trajectoryAffectable.bounds.bounds.min.y <= ((Vector3)ex).y &&
               ((Vector3)ex).y <= trajectoryAffectable.bounds.bounds.max.y;
    }

    public bool IsSafe(List<TrajectoryAffectable> world)
    {
        bool hasLanding = false;
        foreach (var obj in world)
        {
            if (WouldHit(obj)) { return false; }
            if (!hasLanding && CanLandOnWithRange(obj)) { hasLanding = true; }
        }
        return hasLanding;
    }

    public void Draw(Color color, int resolution = 5)
    {
        for (int i = 0; i < resolution; i++)
        {
            Debug.DrawLine(Evaluate((float)i / resolution), Evaluate((i+1f) / resolution), color);
        }
    }
}
