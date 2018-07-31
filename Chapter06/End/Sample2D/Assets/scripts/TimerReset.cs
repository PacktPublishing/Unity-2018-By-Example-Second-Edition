using UnityEngine;
using System.Collections;

public class TimerReset : MonoBehaviour 
{
	public float ResetTime = 5f;

	void Start()
	{
		Invoke ("Reset", ResetTime);
	}

	void Reset()
	{
		PlayerControl.Reset();
		QuestManager.Reset();
		Application.LoadLevel(1);
	}
}
