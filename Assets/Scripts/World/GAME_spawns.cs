using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Unity.VisualScripting;
using System.Collections;

public class GAME_spawns : MonoBehaviour
{
	/*
    REWRITE PLAN OVERVIEW

	DONE -replace class Interactable with interface IInteractable and Destructible with IDestructible (extends IInteractable).

	DONE -TrajectoryAffectable
		a class for objects that affect the platforming of the player.
		-bool solid
		-IEnumerable<Trajectory> Trajectories()
			returns all possible trajectories (jump/no jump, interact/no interact, etc.) from an object.
		-Start()
			add this objs trajectories to the list in GAME_spawns.

	DONE -class Trajectory
		a class describing a trajectory (+x half of a parabola) on which an object can spawn.
		-Transform origin
		-Vector2 startPos
		-float maxDistX
			maximum distance an obj can spawn on this trajectory. it can still be evaluated outside this.
		-Vector3 Evaluate(float x)
		-float InverseEvaluate(float y)
			returns the x coordinate on this trajectory for a given y or null. 
			not ambiguous because this is only 1 half of the parabola.
		-bool CanLandOn(TrajectoryAffectable)
			checks whether the player can land on a given solid TrajectoryAffectable from this trajectory.
			does this by checking if the InverseEvaluate() of the top of the object is between the x-bounds of the object.
	
	-GAME_spawns:
		DONE -List<Trajectory> allTrajectories
		-void Spawn()
			DONE -select a random object
			DONE -select only trajectories that start off camera. pick a random one from these.
			-if a platform, randomise the size of the object
			-evaluate along the Trajectory, reroll until it doesnt collide with any object+headroom.
			-place object.
			-remove every Trajectory that CanLandOn the new object from the list.
     */


	public float deleteThreshhold;
    public float start;
	public float grace;

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

    public GameObject player;
	public PLAYER_baseMvt mvt;

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

		var offCamTrajs = allTrajectories.Where(x => x.AbsPos().x <= start).ToList();
		Trajectory traj = null;
        if (offCamTrajs.Count > 0)
		{
            traj = offCamTrajs[Random.Range(0, offCamTrajs.Count)];
        }
		else
		{
            traj = allTrajectories[Random.Range(0, allTrajectories.Count)];

        }

		obj = Instantiate(obj);
		var plat = obj.GetComponent<OBJ_window>();

        if (plat != null)
		{
			plat.size = new(Random.Range(5f, 30f), Random.Range(5f, 30f));
		}
        obj.transform.position = traj.Evaluate(Random.value);
		obj.GetComponent<GAME_obj>().SetBounds();
		objs.Add(obj);
		UpdateTrajectories();

    }

	void UpdateTrajectories()
	{
        foreach(var obj in objs.Where(x => x.GetComponent<TrajectoryAffectable>() != null).ToList())
		{
            var t = obj.GetComponent<TrajectoryAffectable>();
			foreach (var i in allTrajectories.ToList())
            {
				Debug.DrawLine(t.bounds.center, new(i.InverseEvaluate(t.bounds.max.y), t.bounds.max.y));
				if (i.CanLandOn(t))
                {
					allTrajectories.Remove(i);
                }
            }
        }
    }

    private void Start()
    {
		mvt = player.GetComponent<PLAYER_baseMvt>();
    }
	
    private void Update()
    {


        if (objs.Count < maxObjs)
        {
            Spawn();
        }
		//return;
        foreach (var traj in allTrajectories.ToList())
		{
			if (traj.origin == null)
			{
				allTrajectories.Remove(traj);
				continue;
			} 
			//GLOBAL.DrawCross(traj.origin.position + traj.startPos, 10, Color.purple);
			Debug.DrawLine(traj.origin.position, traj.AbsPos(), Color.green);
			traj.Draw();
		}

        Debug.DrawLine(new Vector3(deleteThreshhold, 100, 0), new Vector3(deleteThreshhold, -100, 0), Color.red);
		Debug.DrawLine(new Vector3(start, 100, 0), new Vector3(start, -100, 0), Color.green);
    }

}
