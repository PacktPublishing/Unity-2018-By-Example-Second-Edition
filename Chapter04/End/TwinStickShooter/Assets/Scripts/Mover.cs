//Programmed by Alan Thorn 2015
//------------------------------
//------------------------------
using UnityEngine;
using System.Collections;
//------------------------------
public class Mover : MonoBehaviour
{
	//------------------------------
	private Transform ThisTransform = null;
	public float MaxSpeed = 10f;
	//------------------------------
	// Use this for initialization
	void Awake () 
	{
		ThisTransform = GetComponent<Transform>();
	}
	//------------------------------
	// Update is called once per frame
	void Update () 
	{
		ThisTransform.position += ThisTransform.forward * MaxSpeed * Time.deltaTime;
	}
	//------------------------------
}
//------------------------------