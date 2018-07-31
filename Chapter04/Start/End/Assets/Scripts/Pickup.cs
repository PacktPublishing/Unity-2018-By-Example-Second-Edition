//Programmed by Alan Thorn 2015
//------------------------------
using UnityEngine;
using System.Collections;
//------------------------------
public class Pickup : MonoBehaviour 
{
	//------------------------------
	public Transform PickupParent = null;
	public string ColliderTag = string.Empty;
	public bool HideOnCollection = true;
	//------------------------------
	void OnTriggerEnter(Collider Col)
	{
		if(!Col.gameObject.CompareTag(ColliderTag))return;

		//Get collision transform
		Transform PickupTransform = Col.gameObject.GetComponent<Transform>();
		PickupTransform.parent = PickupParent;
		PickupTransform.SendMessage("PowerupCollect", SendMessageOptions.DontRequireReceiver);

		if(HideOnCollection)PickupTransform.gameObject.SetActive(false);
	}
	//------------------------------
}
//------------------------------