//--------------------------------
using UnityEngine;
using System.Collections;
//--------------------------------
[System.Serializable]
public class Quest
{
	//Quest completed status
	public enum QUESTSTATUS {UNASSIGNED=0,ASSIGNED=1,COMPLETE=2};
	public QUESTSTATUS Status = QUESTSTATUS.UNASSIGNED;
	public string QuestName = string.Empty;
}
//--------------------------------
public class QuestManager : MonoBehaviour
{
	//--------------------------------
	//All quests in game
	public Quest[] Quests;
	private static QuestManager SingletonInstance = null;
	public static QuestManager ThisInstance
	{
		get{
				if(SingletonInstance==null)
				{
					GameObject QuestObject = new GameObject ("Default");
					SingletonInstance = QuestObject.AddComponent<QuestManager>();
				}
				return SingletonInstance;
			}
	}
	//--------------------------------
	void Awake()
	{
		//If there is an existing instance, then destory
		if(SingletonInstance)
		{
			DestroyImmediate(gameObject);
			return;
		}

		//This is only instance
		SingletonInstance = this;
		DontDestroyOnLoad(gameObject);
	}
	//--------------------------------
	public static Quest.QUESTSTATUS GetQuestStatus(string QuestName)
	{
		foreach(Quest Q in ThisInstance.Quests)
		{
			if(Q.QuestName.Equals(QuestName))
				return Q.Status;
		}

		return Quest.QUESTSTATUS.UNASSIGNED;
	}
	//--------------------------------
	public static void SetQuestStatus(string QuestName, Quest.QUESTSTATUS NewStatus)
	{
		foreach(Quest Q in ThisInstance.Quests)
		{
			if(Q.QuestName.Equals(QuestName))
			{
				Q.Status = NewStatus;
				return;
			}
		}
	}
	//--------------------------------
	//Resets quests back to unassigned state
	public static void Reset()
	{
		if(ThisInstance==null)return;

		foreach(Quest Q in ThisInstance.Quests)
			Q.Status = Quest.QUESTSTATUS.UNASSIGNED;
		
	}
	//--------------------------------
}
//--------------------------------