using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using Unity.VisualScripting;

public class GAME_spawns : MonoBehaviour
{
    /*
     NON PLATFORMING OBJECT SPAWNING
    spawn them along safe trajectories
     
     */
    
    
    [SerializeField] bool debugDraw;
    
    public float deleteThreshhold;
    public float start;
	public float grace;
	public float padding;

    public int maxObjs;
    public int maxNpObjs;

    public List<GAME_objType> objTypes;

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
    public List<GameObject> npObjs = new();

	public List<Trajectory> allTrajectories = new();
    public List<Trajectory> newTrajectories = new();
    public List<Trajectory> trajsWithLandings = new();

	void Spawn()
	{
        var pObjTypes = objTypes.Where(x => x.category == GAME_objType.Category.p).ToList();
        GAME_objType objType = null;
        float p = Random.Range(0, pObjTypes.Select(x => x.frequency).Sum());
		for (int i = 0; i < pObjTypes.Count; i++)
		{
			if (p <= pObjTypes[i].frequency)
			{
                objType = pObjTypes[i];
				break;
			}
			p -= pObjTypes[i].frequency;
		}
        
        GameObject obj = Instantiate(objType.obj);
        obj.GetComponent<GAME_obj>().SetBounds();

        var plat = obj.GetComponent<OBJ_window>();
        if (plat) { plat.size = new(Random.Range(5f, 30f) + grace, Random.Range(5f, 30f)); }
        obj.transform.position = SelectTrajectory().Evaluate(Random.value);
        unresolvedObjs.Add(obj);

    }

    void SpawnNonPlatformable()
    {
        var npObjTypes = objTypes.Where(x => x.category == GAME_objType.Category.np).ToList();
        
        GAME_objType objType = null;
        float p = Random.Range(0, npObjTypes.Select(x => x.frequency).Sum());
        for (int i = 0; i < npObjTypes.Count; i++)
        {
            if (p <= npObjTypes[i].frequency)
            {
                objType = npObjTypes[i];
                break;
            }
            p -= npObjTypes[i].frequency;
        }


        GameObject obj = Instantiate(objType.obj);
        obj.GetComponent<GAME_obj>().SetBounds();

        obj.transform.position = SelectSafeTrajectory().EvaluateWithLanding(Random.value, objs.Select(y => y.GetComponent<TrajectoryAffectable>()).Where(y => y).ToList());
        unresolvedObjs.Add(obj);
    }

    Trajectory SelectSafeTrajectory()
    {
        var safeTrajs = allTrajectories.Where(x => x.IsSafe(objs.Select(y => y.GetComponent<TrajectoryAffectable>()).Where(y => y).ToList())).ToList();
        var offCamTrajs = safeTrajs.Where(x => x.AbsPos().x >= start).ToList();

        Trajectory traj = safeTrajs[Random.Range(0, safeTrajs.Count)];
        if (offCamTrajs.Count > 0) { traj = offCamTrajs[Random.Range(0, offCamTrajs.Count)]; }
        return traj;
    }
	Trajectory SelectTrajectory()
	{
        var offCamTrajs = newTrajectories.Where(x => x.AbsPos().x >= start).ToList();
        Trajectory traj = newTrajectories[Random.Range(0, newTrajectories.Count)];
        if (offCamTrajs.Count > 0) { traj = offCamTrajs[Random.Range(0, offCamTrajs.Count)]; }
		return traj;
    }

	void UpdateTrajectories()
	{
        foreach(var obj in objs.Select(x => x.GetComponent<TrajectoryAffectable>()).Where(x => x != null))
		{
            newTrajectories.RemoveAll(x => x.CanLandOnWithRange(obj));
        }
    }
	
	bool OverlapCheck(Bounds a, Bounds b, bool headroom, float padding)
	{
        a.Encapsulate(a.max + Vector3.up * (headroom?GAME.plyrMvt.jumpHeight:0));
        a.Expand(padding);
        b.Encapsulate(b.max + Vector3.up * (headroom ? GAME.plyrMvt.jumpHeight : 0));
		b.Expand(padding);

        GLOBAL.DrawBounds(a);
        GLOBAL.DrawBounds(b);

		return a.Intersects(b);
	}

    private void Update()
    {

        foreach (var obj in unresolvedObjs.ToList())
        {
            bool isPlatformable = obj.GetComponent<GAME_obj>().typeEntry.category == GAME_objType.Category.p;
            if ((isPlatformable ? objs : objs.Concat(npObjs)).Where(x => x != null).Where(x => 
                OverlapCheck(obj.GetComponent<GAME_obj>().bounds.bounds, x.GetComponent<GAME_obj>().bounds.bounds, isPlatformable, isPlatformable?padding:padding/2)
                ).Count() == 0) // if no objects intersect this
			{
				unresolvedObjs.Remove(obj);
                (isPlatformable?objs:npObjs).Add(obj);
                UpdateTrajectories();
				obj.GetComponent<GAME_obj>().Ready();
				//print("resolved");
                continue;
			}
            if(isPlatformable)
            {
                obj.transform.position = SelectTrajectory().Evaluate(Random.value);
            } 
            else
            {
                obj.transform.position = SelectSafeTrajectory().Evaluate(Random.value);
            }
            
        }

        if (objs.Count < maxObjs)
        {
            Spawn();
        }
        if(npObjs.Count < maxNpObjs)
        {
            SpawnNonPlatformable();
        }
        allTrajectories.RemoveAll(x => x.origin == null);

        if (debugDraw) { DebugDraw(); }
    }

	void DebugDraw()
	{
        foreach (var traj in allTrajectories.ToList())
        {
            if(traj.IsSafe(objs.Select(x => x.GetComponent<TrajectoryAffectable>()).Where(x => x).ToList()))
            {
                traj.Draw(Color.white);
            }
        }

        Debug.DrawLine(new Vector3(deleteThreshhold, 1000, 0), new Vector3(deleteThreshhold, -1000, 0), Color.red);
        Debug.DrawLine(new Vector3(start, 1000, 0), new Vector3(start, -1000, 0), Color.green);
    }
}
