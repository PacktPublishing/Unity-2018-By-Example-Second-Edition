using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTimer : MonoBehaviour
{
    public string SpawnPoolTag = "EnemyPool";
    private ObjectPool Pool = null;
    public float SpawnInterval = 5f;
    private Transform ThisTransform = null;

    private void Awake()
    {
        Pool = GameObject.FindWithTag(SpawnPoolTag).GetComponent<ObjectPool>();
        ThisTransform = GetComponent<Transform>();
    }

    private void Start()
    {
        InvokeRepeating("Spawn", SpawnInterval, SpawnInterval);
    }

    public void Spawn()
    {
        Pool.Spawn(null, ThisTransform.position, ThisTransform.rotation, Vector3.one);
    }
}
