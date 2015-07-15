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

	private Vector3 subDirection;
	private float sqrSubDist;
	private int subtargetIndex = 1;

	private NavMeshPath path;
	private bool hasArrived = false;

	public override void Awake()
	{
		base.Awake();
		path = new NavMeshPath();
		agent = GetComponent<NavMeshAgent>();
		InvokeRepeating("AIUpdate",0.5f,0.5f);
	}

	//this is the Update Method in which the AI gets to think
		//it is called every half-second
	void AIUpdate()
	{	
		//Use Unitys NavMeshAgent to calculate a Path on the NavMesh
		path.ClearCorners();//Clear the Old Path(necessary?)
		agent.enabled = true;
		agent.CalculatePath(target.position, path);
		agent.enabled = false;


		//Debug.draw the path in red
		Debug.DrawLine(transform.position, path.corners[0],Color.red,0.5f);
		for(int i = 1; i < path.corners.Length; i++)
		{
			Debug.DrawLine(path.corners[i-1], path.corners[i],Color.red,0.5f);
		}

		//the subtarget is the point on the Path the AI wants to move towards
		//it is resetted to the first pathcorner when a new path is calculated
		subtargetIndex = 1;
		subtarget = path.corners[subtargetIndex];
	}

	void FixedUpdate()
	{

		if(target != null)
		{

			//if you have almost reached your subgoal, move on to the next one to avoid stuttering
			if(sqrSubDist < 3 && subtargetIndex <= path.corners.Length)
			{
				subtargetIndex++;
				subtarget = path.corners[subtargetIndex];
			}
			
			sqrTargetDist = (target.position - transform.position).sqrMagnitude;
			hasArrived  = (sqrTargetDist < stopDist);

			if(sqrTargetDist < stopDist)
			{
				//Debug.Log("Arrived" + sqrTargetDist);
			}

			//calculate the direction the character should move in 
			SeekSubtarget(); //seek your target
			//TODO: walk around obstacles
			//TODO: evade dangerous positions (Does this belong here?)
		}

		//split the direction Vector in a horizontal and vertical component
		//this is mainly a relict of the PlayerMovement  axis-implementation
		float h = subDirection.x;
		float v = subDirection.z;

		//Debug.draw the direction you want to move in
		Debug.DrawRay(transform.position + new Vector3(0.0f, 0.5f, 0.0f), subDirection.normalized * 3,Color.red);


		//reset the movement-values if the character is Attacking(strong), his movement is blocked or he is knocked Down
		if (playerAttacks.getIsStronging() || blockMovement
		    || getKnockdown() || hasArrived)
		{
			//Debug.Log("stopped");
			h = 0;
			v = 0;
		}

		//if either one of the movement values is not zero the character should be walking
		bool walking = h != 0f || v != 0f;

		//Move, Turn and Animate the character if he is on solid ground
		if(onFloor)
		{
			Move (h, v);
			Turning(h, v, walking);
			Animating (h, v, walking);
		}
		//otherwise let the character Fall
		else
		{
			Fall();
		}

	}

	//Calculate the direction to the subtarget and calculate the squared Distance
	Vector3 SeekSubtarget()
	{

		subDirection = subtarget - transform.position;
		subDirection = new Vector3(subDirection.x, 0.0f, subDirection.z);

		sqrSubDist = subDirection.sqrMagnitude;


		return Vector3.Normalize(subDirection);

	}

	
	public void SetTarget(Transform in_Target)
	{
		target = in_Target;
	}

	public float getSqrTargetDistance()
	{
		return sqrTargetDist;
	}

	public bool getHasArrived()
	{
		return hasArrived;
	}
}
