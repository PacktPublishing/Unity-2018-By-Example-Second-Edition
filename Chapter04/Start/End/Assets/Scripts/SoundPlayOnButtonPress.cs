//Programmed by Alan Thorn 2015
//------------------------------
using UnityEngine;
using System.Collections;
//------------------------------
public class SoundPlayOnButtonPress : MonoBehaviour
{
	//------------------------------
	public string ButtonDown = string.Empty;
	private AudioSource ThisAudio = null;
	//------------------------------
	void Awake()
	{
		ThisAudio = GetComponent<AudioSource>();
	}
	//------------------------------
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetButtonDown(ButtonDown))
			ThisAudio.PlayOneShot(ThisAudio.clip);
	}
	//------------------------------
}
//------------------------------