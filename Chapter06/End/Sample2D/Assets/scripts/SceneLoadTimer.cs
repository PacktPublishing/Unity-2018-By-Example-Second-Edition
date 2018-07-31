using UnityEngine;
using System.Collections;

public class SceneLoadTimer : MonoBehaviour 
{
	//Scene to load after time
	public int SceneID = 0;
	public float TimeDelay = 5f;

	// Use this for initialization
	void Start () 
	{
		Invoke("LoadScene", TimeDelay);
	}
	
	// Update is called once per frame
	void LoadScene () 
	{
		Application.LoadLevel(SceneID);
	}
}
