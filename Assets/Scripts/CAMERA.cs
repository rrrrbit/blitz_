using Unity.VisualScripting;
using UnityEditor.PackageManager.UI;
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


	// Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        targRb = target.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

		var lookahead = targRb.linearVelocity.y * lkahdMult; 
		transform.position = GLOBAL.Lerpd(transform.position, new Vector3(0, target.transform.position.y, 0) + Vector3.up * lookahead + offset, k, t, Time.deltaTime);

		if (debugDraw) { DebugDraw(lookahead); }
	}

	void DebugDraw(float lookahead)
	{
		var p = transform.position;
		Debug.DrawLine(new(p.x - 100, p.y, 0), new(p.x + 100, p.y, 0), Color.red);
		Debug.DrawLine(new(p.x-100, target.transform.position.y, 0), new(p.x + 100, target.transform.position.y, 0), Color.blue);
		Debug.DrawLine(new(p.x-100, target.transform.position.y + lookahead, 0), new(p.x + 100, target.transform.position.y + lookahead, 0), Color.green);

		var cam  = GetComponent<Camera>();
		Bounds bounds = new();
		bounds.SetMinMax(cam.ScreenToWorldPoint(new(0,0, target.transform.position.z - transform.position.z)), cam.ScreenToWorldPoint(new(Screen.width, Screen.height, target.transform.position.z - transform.position.z)));

		GLOBAL.DrawBounds(bounds, Color.purple);
	}

	public void GetBounds()
	{
	}
}
