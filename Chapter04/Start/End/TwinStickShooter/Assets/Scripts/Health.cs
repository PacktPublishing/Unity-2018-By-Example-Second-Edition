//Programmed by Alan Thorn 2015
//------------------------------
using UnityEngine;
using System.Collections;
//------------------------------
public class Health : MonoBehaviour
{
	public GameObject DeathParticlesPrefab = null;
	private Transform ThisTransform = null;
	public bool ShouldDestroyOnDeath = true;
	//------------------------------
	void Start()
	{
		ThisTransform = GetComponent<Transform>();
	}
	//------------------------------
	public float HealthPoints
	{
		get
		{
			return _HealthPoints;
		}

		set
		{
			_HealthPoints = value;

			if(_HealthPoints <= 0)
			{
				SendMessage("Die", SendMessageOptions.DontRequireReceiver);

				if(DeathParticlesPrefab != null)
					Instantiate(DeathParticlesPrefab, ThisTransform.position, ThisTransform.rotation);

				if(ShouldDestroyOnDeath)Destroy(gameObject);
			}
		}
	}
	//------------------------------
	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Space))
			HealthPoints = 0;
	}
	//------------------------------
	[SerializeField]
	private float _HealthPoints = 100f;
}
//------------------------------