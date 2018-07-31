//--------------------------------
using UnityEngine;
using System.Collections;
//--------------------------------
public class AmmoSpawner : MonoBehaviour 
{
	//--------------------------------
	//Reference to ammo prefab
	public GameObject AmmoPrefab = null;

	//Reference to transform
	private Transform ThisTransform = null;

	//Vector for time range
	public Vector2 TimeDelayRange = Vector2.zero;

	//Lifetime for ammo spawned
	public float AmmoLifeTime = 2f;

	//Ammo Speed
	public float AmmoSpeed = 4f;

	//Ammo Damage
	public float AmmoDamage = 100f;
	//--------------------------------
	void Awake()
	{
		ThisTransform = GetComponent<Transform>();
	}
	//--------------------------------
	void Start()
	{
		FireAmmo();
	}
	//--------------------------------
	public void FireAmmo()
	{
		GameObject Obj = Instantiate(AmmoPrefab, ThisTransform.position, ThisTransform.rotation) as GameObject;
		Ammo AmmoComp = Obj.GetComponent<Ammo>();
		Mover MoveComp = Obj.GetComponent<Mover>();
		AmmoComp.LifeTime = AmmoLifeTime;
		AmmoComp.Damage = AmmoDamage;
		MoveComp.Speed = AmmoSpeed;

		//Wait until next random interval
		Invoke("FireAmmo", Random.Range(TimeDelayRange.x, TimeDelayRange.y));
	}
	//--------------------------------
}
//--------------------------------