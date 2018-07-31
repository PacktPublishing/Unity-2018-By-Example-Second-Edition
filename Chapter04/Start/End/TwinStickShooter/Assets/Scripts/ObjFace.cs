//Programmed by Alan Thorn 2015
//------------------------------
//------------------------------
using UnityEngine;
using System.Collections;
//------------------------------
public class ObjFace : MonoBehaviour
{
	//------------------------------
	public Transform ObjToFollow = null;
	public bool FollowPlayer = false;
	private Transform ThisTransform = null;
	//------------------------------
	// Use this for initialization
	void Awake () 
	{
		//Get local transform
		ThisTransform = GetComponent<Transform>();

		//Should face player?
		if(!FollowPlayer)return;

		//Get player transform
		GameObject PlayerObj = GameObject.FindGameObjectWithTag("Player");
		if(PlayerObj != null) ObjToFollow = PlayerObj.GetComponent<Transform>();
	}
	//------------------------------
	// Update is called once per frame
	void Update ()
	{
		//Follow destination object
		if(ObjToFollow==null)return;

		//Get direction to follow object
		Vector3 DirToObject = ObjToFollow.position - ThisTransform.position;

		if(DirToObject != Vector3.zero)
			ThisTransform.localRotation = Quaternion.LookRotation(DirToObject.normalized,Vector3.up);
	}
	//------------------------------
}
//------------------------------