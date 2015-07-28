using UnityEngine;
using System.Collections;

public enum moveType{SEEK, FLEE, }

public class AI_Movement : Movement
{
	public Transform target;

	public Vector3 subtarget;

	private NavMeshAgent agent;

	private float sqrTargetDist;
	private float stopDist = 9.0f;

	private Vector3 pathAttraction;
	private Vector3 attraction;
	private Vector3 repulsion;
	private Vector3 dangerRepulsion;

	Vector3 direction;

	private float sqrSubDist;
	private int subtargetIndex = 1;

	private NavMeshPath path;
	private bool hasArrived = false;

	private bool isStalled = false;

	//should seekingmovement be updated?
	bool seeking = false;

	bool flanking = false;

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
		if(seeking)
		{
			CalculatePath();

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
	}

	void Update()
	{		
		//reset attraction and repulsion
		//attraction = new Vector3(0,0,0);
		//repulsion = new Vector3(0,0,0);

		//calculate dangerous object(navmeshborder) repulsion
		evadeDanger();

		//if you seek a target calculate seekattraction
		if(seeking)
		{
			SeekUpdate();
		}

	}

	void FixedUpdate()
	{
		float f = 5.0f;
		if(dangerRepulsion.sqrMagnitude > 0.75)
			f = 0.0f;
		direction = (pathAttraction * 2 + attraction) - (dangerRepulsion * dangerRepulsion.magnitude * 3 + repulsion * repulsion.magnitude * f);

		//split the direction Vector in a horizontal and vertical component
		//this is mainly a relict of the PlayerMovement  axis-implementation

		isStalled = direction.sqrMagnitude < 0.1? true: false;

		float h = direction.x;
		float v = direction.z;

		//Debug.draw the direction you want to move in
		
		Debug.DrawRay(transform.position + new Vector3(0.0f, 0.5f, 0.0f), pathAttraction.normalized * 3,Color.red);
		Debug.DrawRay(transform.position + new Vector3(0.0f, 0.5f, 0.0f), attraction.normalized * 3,Color.magenta);
		Debug.DrawRay(transform.position + new Vector3(0.0f, 0.5f, 0.0f), dangerRepulsion.normalized * 3,Color.blue);
		Debug.DrawRay(transform.position + new Vector3(0.0f, 0.5f, 0.0f), repulsion.normalized * 3,Color.cyan);
		Debug.DrawRay(transform.position + new Vector3(0.0f, 0.5f, 0.0f), direction.normalized * 3,Color.yellow);


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
		else if(!blockMovement)
		{
			Fall();
		}

	}

	void CalculatePath()
	{
		//Use Unitys NavMeshAgent to calculate a Path on the NavMesh
		path.ClearCorners();//Clear the Old Path(necessary?)
		agent.enabled = true;
		agent.CalculatePath(target.position, path);
		agent.enabled = false;
	}


	//Calculate the distance to the subtarget and calculate the squared Distance
	void SeekUpdate()
	{
		if(target != null)
		{
			
			//if you have almost reached your subgoal, move on to the next one to avoid stuttering
			if(sqrSubDist < 3 && subtargetIndex < path.corners.Length-1)
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
		}
	}

	/***ADD MOVEINPUT METHODS***/
	//These Methods are called from the behaviour trees tasks and change the attraction /repulsion vectors
	//Attraction and Repulsion are then combined into the AIs next direction vector

	public void attr_SeekSubtarget()
	{

		Vector3 subDirection = subtarget - transform.position;
		subDirection = new Vector3(subDirection.x, 0.0f, subDirection.z);

		sqrSubDist = subDirection.sqrMagnitude;


		pathAttraction = Vector3.Normalize(subDirection);

	}

	public void attr_SeekPosition(Transform t)
	{
		
		Vector3 seek = t.position - transform.position;
		seek = new Vector3(seek.x, 0.0f, seek.z);
		
		
		pathAttraction = Vector3.Normalize(seek);
		
	}

	public void attr_flank(bool right)
	{
		float circleFactor = 0.3f;
		Vector3 rel_Right = Vector3.Cross((target.position - transform.position), transform.up);

		if(right)
		{
			attraction = Vector3.Normalize(rel_Right + (target.position - transform.position) * circleFactor);
		}
		else
		{
			attraction = -Vector3.Normalize(rel_Right + (target.position - transform.position) * circleFactor);
		}
	}

	public void turnToTarget()
	{
		Vector3 lookAtTarget = target.position-transform.position;
		pathAttraction = lookAtTarget.normalized;
		Quaternion newRotation = Quaternion.LookRotation(lookAtTarget);
		playerRigidbody.MoveRotation(newRotation);
	}

	public void rep_evadePosition(Transform t, float maxEvasion)
	{
		float maxEvasionDist = maxEvasion;
		Vector3 toPosition = t.position - transform.position;
		Vector3 result = new Vector3();


		if(toPosition.sqrMagnitude < maxEvasionDist)
		{
			result = toPosition.normalized * (maxEvasionDist - toPosition.sqrMagnitude)/maxEvasionDist;
		}

		float angle = Random.Range(-45.0f, 45.0f);
		Quaternion q = Quaternion.AngleAxis(angle, transform.up);
		Quaternion p = Quaternion.LookRotation(result);

		p = p * q;

		repulsion = result;
	}


	void evadeDanger()
	{
		float maxEvasionDist = 2.0f;
		Vector3 toClosestDanger = new Vector3();

		Vector3 result = new Vector3();

		NavMeshHit hit;

		if (NavMesh.FindClosestEdge(transform.position, out hit, NavMesh.AllAreas)) 
		{
			toClosestDanger = hit.position - transform.position;
		}

		if(hit.distance < maxEvasionDist)
		{
			result = toClosestDanger.normalized * (maxEvasionDist - hit.distance)/maxEvasionDist;
		}
		
		agent.enabled = true;
		if(agent.isOnNavMesh)
			dangerRepulsion = result;
		else
			dangerRepulsion = -result;
		agent.enabled = false;
	}

	public void stop()
	{
		attraction = new Vector3();
		pathAttraction = new Vector3();
		repulsion = new Vector3();
		dangerRepulsion = new Vector3();
	}

	bool whoIsInDanger(Transform opponentT)
	{

		NavMeshHit opponentHit;
		NavMesh.FindClosestEdge(opponentT.position, out opponentHit, NavMesh.AllAreas);
		
		NavMeshHit myHit;
		NavMesh.FindClosestEdge(transform.position, out myHit, NavMesh.AllAreas);

		return true;

	}

	public void SetTarget(Transform in_Target)
	{
		target = in_Target;
		hasArrived = false;
	}

	public float getSqrTargetDistance()
	{
		return sqrTargetDist;
	}

	public bool getHasArrived()
	{
		return hasArrived;
	}

	public void setSeeking(bool b)
	{
		seeking = b;
	}
	public void setstopDist(float f)
	{
		stopDist = f;
	}

	public void setFlanking(bool b)
	{
		flanking = b;
	}

	public bool getIsStalled()
	{
		return isStalled;
	}

	void OnGUI ()
	{ 
		GUI.TextArea (new Rect (10, 60, 200, 50), "Direction SqrMag: " + direction.sqrMagnitude 
		              + "\n isStalled: " + isStalled); 
		
	}

}
