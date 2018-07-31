using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageAffectorParticles : MonoBehaviour
{
    public string TagDamage = "Enemy";
    private Health ThisHealth = null;
    public float DamageAmount = 2f;

    private void Awake()
    {
        ThisHealth = GetComponent<Health>();
    }

    private void OnParticleCollision(GameObject other)
    {
        if (!other.CompareTag(TagDamage)) return;
        ThisHealth.HealthPoints -= DamageAmount;
    }
}
