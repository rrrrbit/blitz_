using UnityEngine;

public class OBJ_node : Destructible
{
	[SerializeField] ParticleSystem particle;
	public override void Slash(GameObject context)
	{
		var p = Instantiate(particle);
		p.transform.position = transform.position;
		p.transform.right = transform.position - context.transform.position;
		base.Slash(context);
	}
}
