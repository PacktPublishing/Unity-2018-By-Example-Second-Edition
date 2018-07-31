using UnityEngine;
using System.Collections;
//------------------------------------------
public class LineSight : MonoBehaviour
{
	//------------------------------------------
	//How sensitive should we be to sight
	public enum SightSensitivity {STRICT, LOOSE};

	//Sight sensitivity
	public SightSensitivity Sensitity = SightSensitivity.STRICT;

	//Can we see target
	public bool CanSeeTarget = false;

	//FOV
	public float FieldOfView = 45f;

	//Reference to target
	private Transform Target = null;

	//Reference to eyes
	public Transform EyePoint = null;

	//Reference to transform component
	private Transform ThisTransform = null;

	//Reference to sphere collider
	private SphereCollider ThisCollider = null;

	//Reference to last know object sighting, if any
	public Vector3 LastKnowSighting = Vector3.zero;
	//------------------------------------------
	void Awake()
	{
		ThisTransform = GetComponent<Transform>();
		ThisCollider = GetComponent<SphereCollider>();
		LastKnowSighting = ThisTransform.position;
		Target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
	}
	//------------------------------------------
	bool InFOV()
	{
		//Get direction to target
		Vector3 DirToTarget = Target.position - EyePoint.position;

		//Get angle between forward and look direction
		float Angle = Vector3.Angle(EyePoint.forward, DirToTarget);

		//Are we within field of view?
		if(Angle <= FieldOfView)
			return true;

		//Not within view
		return false;
	}
	//------------------------------------------
	bool ClearLineofSight()
	{
		RaycastHit Info;
	
		if(Physics.Raycast(EyePoint.position, (Target.position - EyePoint.position).normalized, out Info, ThisCollider.radius))
		{
			//If player, then can see player
			if(Info.transform.CompareTag("Player"))
				return true;
		}

		return false;
	}
	//------------------------------------------
	void UpdateSight()
	{
		switch(Sensitity)
		{
			case SightSensitivity.STRICT:
				CanSeeTarget = InFOV() && ClearLineofSight();
			break;

			case SightSensitivity.LOOSE:
				CanSeeTarget = InFOV() || ClearLineofSight();
			break;
		}
	}
	//------------------------------------------
	void OnTriggerStay(Collider Other)
	{
		UpdateSight();

		//Update last known sighting
		if(CanSeeTarget)
			LastKnowSighting =  Target.position;
	}
	//------------------------------------------
	void OnTriggerExit(Collider Other)
	{
		if(!Other.CompareTag("Player"))return;

		CanSeeTarget = false;
	}
	//------------------------------------------
}
//------------------------------------------