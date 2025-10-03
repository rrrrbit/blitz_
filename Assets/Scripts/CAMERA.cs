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
		
	}

	void DebugDraw(float lookahead)
	{
		var p = transform.position;
		Debug.DrawLine(new(p.x - 100, p.y, 0), new(p.x + 100, p.y, 0), Color.red);
		Debug.DrawLine(new(p.x-100, target.transform.position.y, 0), new(p.x + 100, target.transform.position.y, 0), Color.blue);
		Debug.DrawLine(new(p.x-100, target.transform.position.y + lookahead, 0), new(p.x + 100, target.transform.position.y + lookahead, 0), Color.green);

	}
}
