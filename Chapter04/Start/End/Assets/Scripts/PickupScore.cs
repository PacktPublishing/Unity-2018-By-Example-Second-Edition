//Programmed by Alan Thorn 2015
//------------------------------
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
//------------------------------
public class PickupScore : MonoBehaviour 
{
	public int ScorePoints = 100;
	private Text BonusText = null;
	public float MessageDelay = 2f;
	public string MessageTag = string.Empty;
	//------------------------------
	void Awake()
	{
		GameObject BonusObject = GameObject.FindGameObjectWithTag(MessageTag);
		BonusText = BonusObject.GetComponent<Text>();
	}
	//------------------------------
	// Update is called once per frame
	void PowerupCollect () 
	{
		GameController.Score += ScorePoints;

		//Show score text
		BonusText.enabled=true;

		Invoke("HideText", MessageDelay);
	}
	//------------------------------
	public void HideText()
	{
		BonusText.enabled=false;
	}
	//------------------------------
}
//------------------------------