//Programmed by Alan Thorn 2015
//------------------------------
//------------------------------
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//------------------------------
public class AmmoManager : MonoBehaviour
{
	//------------------------------
	//Reference to ammo prefab
	public GameObject AmmoPrefab = null;

	//Ammo pool count
	public int PoolSize = 100;

	public Queue<Transform> AmmoQueue = new Queue<Transform>();

	//Array of ammo objects to generate
	private GameObject[] AmmoArray;

	public static AmmoManager AmmoManagerSingleton = null;
	//------------------------------
	// Use this for initialization
	void Awake ()
	{
	}
	//------------------------------
}
//------------------------------