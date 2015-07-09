using UnityEngine;
using System.Collections;

public class AI_Movement : Movement
{
	public Transform target;
	private float sqrTargetDist;
	private float stopDist = 9.0f;


	public override void Awake()
	{
		base.Awake();
	}

	void FixedUpdate()
	{
		Vector3 direction = Seek();

		float h = direction.x;
		float v = direction.z;

		if (playerAttacks.getIsStronging() || blockMovement
		    || getKnockdown() || sqrTargetDist < stopDist)
		{
			h = 0;
			v = 0;
		}

		bool walking = h != 0f || v != 0f;
		if(onFloor)
		{
			Move (h, v);
			Turning (h, v, walking);
			Animating (h, v, walking);
		}
		else
		{
			Fall();
		}

	}

	Vector3 Seek()
	{
		Vector3 direction = target.position - transform.position;
		direction = new Vector3(direction.x, 0.0f, direction.z);

		sqrTargetDist = direction.sqrMagnitude;


		return Vector3.Normalize(direction);

	}

	
	public void setTarget(Transform in_Target)
	{
		target = in_Target;
	}
}
