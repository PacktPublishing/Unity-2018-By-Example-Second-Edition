using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//---------------------------------
public class ObjectPool : MonoBehaviour
{
    private Transform ThisTransform = null;
    public GameObject ObjectPrefab = null;
    public int PoolSize = 10;
    //---------------------------------
    private void Awake()
    {
        ThisTransform = GetComponent<Transform>();
    }
    //---------------------------------
    private void Start()
    {
        GeneratePool();
    }
    //---------------------------------
    //Generates initial object pool
    public void GeneratePool()
    {
        for (int i = 0; i < PoolSize; i++)
        {
            //Generate child object
            GameObject Obj = Instantiate(ObjectPrefab, Vector3.zero, Quaternion.identity, ThisTransform);
            Obj.SetActive(false);
        }
    }
    //---------------------------------
    //Function to spawn a new object in the level at the specified position, rotation and scale
    public Transform Spawn(Transform Parent, 
                      Vector3 Position = new Vector3(), 
                      Quaternion Rotation = new Quaternion(), 
                      Vector3 Scale = new Vector3())
    {
        //No object available
        if (ThisTransform.childCount <= 0) return null;

        //Get first child
        Transform Child = ThisTransform.GetChild(0);

        //Activate
        Child.SetParent(Parent);
        Child.position = Position;
        Child.rotation = Rotation;
        Child.localScale = Scale;
        Child.gameObject.SetActive(true);
        return Child;
    }
    //---------------------------------
    public void DeSpawn(Transform ObjectToDespawn)
    {
        //Deactivate
        ObjectToDespawn.gameObject.SetActive(false);
        ObjectToDespawn.SetParent(ThisTransform);
        ObjectToDespawn.position = Vector3.zero;
    }
    //---------------------------------
}
