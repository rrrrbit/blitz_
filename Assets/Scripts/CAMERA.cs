using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class CAMERA : MonoBehaviour
{
	[SerializeField] GameObject target;

	[SerializeField] Vector3 offset;

	[SerializeField] float lkahdMult = 0;

	[SerializeField] float k, t;

	Rigidbody2D targRb;
	public bool debugDraw;

	[SerializeField] Vector3 quickTransitionPos;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        targRb = target.GetComponent<Rigidbody2D>();

		if (GAME_globalData.instance.quickGameTransition)
		{
			transform.position = quickTransitionPos;
		}
    }

    // Update is called once per frame
    void Update()
    {

		var lookahead = targRb.linearVelocity.y * lkahdMult;
		var p = new Vector3(
            GLOBAL.Lerpd(transform.position.x, offset.x, 0.5f, 0.25f, Time.deltaTime),
            GLOBAL.Lerpd(transform.position.y, offset.y + target.transform.position.y + lookahead, k, t, Time.deltaTime),
            GLOBAL.Lerpd(transform.position.z, offset.z, k, t, Time.deltaTime)

            );
		transform.position = p;

		if (debugDraw) { DebugDraw(lookahead); }
	}

	void DebugDraw(float lookahead)
	{
		var p = transform.position;
		Debug.DrawLine(new(p.x - 100, p.y, 0), new(p.x + 100, p.y, 0), Color.red);
		Debug.DrawLine(new(p.x-100, target.transform.position.y, 0), new(p.x + 100, target.transform.position.y, 0), Color.blue);
		Debug.DrawLine(new(p.x-100, target.transform.position.y + lookahead, 0), new(p.x + 100, target.transform.position.y + lookahead, 0), Color.green);

		

		GLOBAL.DrawBounds(GetBounds(), Color.purple);
	}

	public Bounds GetBounds()
	{
        var cam = GetComponent<Camera>();
        Bounds bounds = new();
		var z = target.transform.position.z - transform.position.z;
        bounds.SetMinMax(cam.ScreenToWorldPoint(new(0, 0, z)), cam.ScreenToWorldPoint(new(Screen.width, Screen.height, z)));
		return bounds;
	}
}
