using UnityEngine;
using System.Collections;

public class AI_Movement : Movement
{
	public Transform target;

	public Vector3 subtarget;

	
	private NavMeshAgent agent;

	private float sqrTargetDist;
	private float stopDist = 9.0f;
	private Vector3 attraction;
	private Vector3 repulsion;
	private Vector3 direction;

	public override void Awake()
	{
		base.Awake();
		agent = GetComponent<NavMeshAgent>();
		InvokeRepeating("AIUpdate",0.5f,0.5f);
	}

	void AIUpdate()
	{	
		agent.SetDestination(target.position);
		NavMeshPath path = new NavMeshPath();
		agent.CalculatePath(target.position, path);
		
		Debug.DrawLine(transform.position, path.corners[0],Color.red,0.5f);
		for(int i = 1; i < path.corners.Length; i++)
		{
			Debug.DrawLine(path.corners[i-1], path.corners[i],Color.red,0.5f);
		}
	}

	void FixedUpdate()
	{

		if(target != null)
		{
			direction = agent.velocity.normalized;
		}
		float h = direction.x;
		float v = direction.z;

		Debug.DrawRay(transform.position + new Vector3(0.0f, 0.5f, 0.0f), direction * 3,Color.red);

		agent.Resume();

		if (playerAttacks.getIsStronging() || blockMovement
		    || getKnockdown())
		{
			agent.Stop();
			//Debug.Log("stopped");
			h = 0;
			v = 0;
		}

		bool walking = h != 0f || v != 0f;
		if(onFloor)
		{
			if(!agent.enabled)
				agent.enabled = true;
			Animating (h, v, walking);
		}
		else
		{
			agent.enabled = false;
			Fall();
		}

	}

	Vector3 Seek()
	{

		direction = target.position - transform.position;
		direction = new Vector3(direction.x, 0.0f, direction.z);

		sqrTargetDist = direction.sqrMagnitude;


		return Vector3.Normalize(direction);

	}

	
	public void setTarget(Transform in_Target)
	{
		target = in_Target;
	}
}
