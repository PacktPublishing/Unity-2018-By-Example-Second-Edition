using UnityEngine;
using System.Collections;
//--------------------------------
public class Mover : MonoBehaviour 
{
	//--------------------------------
	public float Speed = 10f;
	private Transform ThisTransform = null;
	//--------------------------------
	// Use this for initialization
	void Awake() 
	{
		ThisTransform = GetComponent<Transform>();
	}
	//--------------------------------
	// Update is called once per frame
	void Update () 
	{
		//Update object position
		ThisTransform.position += ThisTransform.forward * Speed * Time.deltaTime;
	}
	//--------------------------------
}
//--------------------------------