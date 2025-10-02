using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using Unity.VisualScripting;

public class GAME_spawns : MonoBehaviour
{
    [SerializeField] bool debugDraw;
    
    public float deleteThreshhold;
    public float start;
	public float grace;
	public float padding;

    public int maxObjs;

    public List<GameObject> objTypes;
	public List<float> objTypeProbs;

	[Header("object type refs")]
	public GameObject window;
	public GameObject relay;
	public GameObject burst;
	public GameObject proxy;
	public GameObject kblitz;
	public GameObject node;
    public GameObject wall;
	public GameObject empty;
	[Space]

    public List<GameObject> objs = new();
	public List<GameObject> unresolvedObjs = new();

	public List<Trajectory> allTrajectories = new();

	void Spawn()
	{
		GameObject obj = null;
		float p = Random.Range(0, objTypeProbs.Sum());
		for (int i = 0; i < objTypeProbs.Count; i++)
		{
			if (p <= objTypeProbs[i])
			{
				obj = objTypes[i];
				break;
			}
			p -= objTypeProbs[i];
		}

        


        obj.GetComponent<GAME_obj>().SetBounds();
		obj = Instantiate(obj);
		var plat = obj.GetComponent<OBJ_window>();

        if (plat != null) { plat.size = new(Random.Range(5f, 30f) + grace, Random.Range(5f, 30f)); }


        obj.transform.position = SelectTrajectory().Evaluate(Random.value);
		unresolvedObjs.Add(obj);



	}

	void UpdateTrajectories()
	{
        foreach(var obj in objs.Where(x => x.GetComponent<TrajectoryAffectable>() != null).Select(x => x.GetComponent<TrajectoryAffectable>()))
		{
			foreach (var i in allTrajectories.Where(x => x.CanLandOn(obj)).ToList())
            {
				allTrajectories.Remove(i);
            }
        }
    }
	
	Trajectory SelectTrajectory()
	{
        var offCamTrajs = allTrajectories.Where(x => x.AbsPos().x >= start).ToList();
        Trajectory traj = allTrajectories[Random.Range(0, allTrajectories.Count)];
        if (offCamTrajs.Count > 0) { traj = offCamTrajs[Random.Range(0, offCamTrajs.Count)]; }
		return traj;
    }
	bool OverlapCheck(Bounds a, Bounds b)
	{
        a.Encapsulate(a.max + Vector3.up * (GAME.plyrMvt.jumpHeight));
        a.Expand(padding);
        b.Encapsulate(b.max + Vector3.up * (GAME.plyrMvt.jumpHeight) );
		b.Expand(padding);
		return a.Intersects(b);
	}

    private void Update()
    {

        foreach (var obj in unresolvedObjs.ToList())
        {

			if (objs.Where(x => OverlapCheck(obj.GetComponent<GAME_obj>().bounds.bounds, x.GetComponent<GAME_obj>().bounds.bounds)).Count() == 0) // if no platforms intersect this
			{
				unresolvedObjs.Remove(obj);
                objs.Add(obj);
                UpdateTrajectories();
				obj.GetComponent<GAME_obj>().Ready();
				print("resolved");
                continue;
			}
            obj.transform.position = SelectTrajectory().Evaluate(Random.value);
        }

        if (objs.Count < maxObjs)
        {
            Spawn();
        }

        if (debugDraw) { DebugDraw(); }
    }

	void DebugDraw()
	{
        foreach (var traj in allTrajectories.ToList())
        {
            if (traj.origin == null)
            {
                allTrajectories.Remove(traj);
                continue;
            }


            foreach (TrajectoryAffectable obj in objs.Where(x => x.GetComponent<TrajectoryAffectable>() != null).Select(x => x.GetComponent<TrajectoryAffectable>()))
            {
                if (traj.InverseEvaluate(obj.bounds.bounds.max.y) == null) { continue; }

                if (traj.AbsPos().x < start)
                {
                    traj.Draw(Color.red);
                }
                else if (traj.WouldHit(obj))
                {
                    traj.Draw(Color.yellow);

                }
                else
                {
                    traj.Draw(Color.green);
                }

                //Debug.DrawLine(new(obj.bounds.bounds.min.x, obj.bounds.bounds.max.y), new(obj.bounds.bounds.max.x, obj.bounds.bounds.max.y), Color.purple);
                Debug.DrawLine(new(obj.bounds.bounds.min.x, obj.bounds.bounds.min.y), new(obj.bounds.bounds.min.x, obj.bounds.bounds.max.y), Color.purple);

                if (traj.WouldHit(obj))
                {

                    GLOBAL.DrawCross((Vector3)traj.EvaluateAbs(obj.bounds.bounds.min.x));
                }

                Debug.DrawLine(traj.origin.GetComponent<GAME_obj>().bounds.bounds.center, traj.AbsPos(), Color.green);
            }
        }

        Debug.DrawLine(new Vector3(deleteThreshhold, 1000, 0), new Vector3(deleteThreshhold, -1000, 0), Color.red);
        Debug.DrawLine(new Vector3(start, 1000, 0), new Vector3(start, -1000, 0), Color.green);
    }
}
