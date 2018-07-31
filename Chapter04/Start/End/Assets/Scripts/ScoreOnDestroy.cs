//Programmed by Alan Thorn 2015
//------------------------------
using UnityEngine;
using System.Collections;
//------------------------------
public class ScoreOnDestroy : MonoBehaviour
{
	//------------------------------
	public int ScoreValue = 50;
	//------------------------------
	void OnDestroy()
	{
		GameController.Score += ScoreValue;
	}
	//------------------------------
}
//------------------------------