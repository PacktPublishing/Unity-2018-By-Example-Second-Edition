using UnityEngine;
using System.Collections;
//--------------------------------
public class PingPongMotion : MonoBehaviour 
{
	//--------------------------------
	//This transformation
	private Transform ThisTransform = null;

	//Original position
	private Vector3 OrigPos = Vector3.zero;

	//Axes to move on
	public Vector3 MoveAxes = Vector2.zero;

	//Speed
	public float Distance = 3f;
	//--------------------------------
	// Use this for initialization
	void Awake ()
	{
		//Get transform component
		ThisTransform = GetComponent<Transform>();

		//Copy original position
		OrigPos = ThisTransform.position;
	}
	//--------------------------------
	// Update is called once per frame
	void Update () 
	{
		//Update platform position with ping pong
		ThisTransform.position = OrigPos + MoveAxes * Mathf.PingPong(Time.time, Distance);
	}
	//--------------------------------
}
//--------------------------------