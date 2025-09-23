using Unity.VisualScripting;
using UnityEngine;

public class CAMERA : MonoBehaviour
{
	[SerializeField] GameObject target;

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
		var p = transform.position;

		var lookahead = targRb.linearVelocity.y * lkahdMult;

		p.y = GLOBAL.Lerpd(transform.position.y, target.transform.position.y + lookahead, k, t, Time.deltaTime);
		transform.position = p;
		return;
		Debug.DrawLine(new(p.x - 100, p.y, 0), new(p.x + 100, p.y, 0), Color.red);
		Debug.DrawLine(new(p.x-100, target.transform.position.y, 0), new(p.x + 100, target.transform.position.y, 0), Color.blue);
		Debug.DrawLine(new(p.x-100, target.transform.position.y + lookahead, 0), new(p.x + 100, target.transform.position.y + lookahead, 0), Color.green);
		
	}
}
