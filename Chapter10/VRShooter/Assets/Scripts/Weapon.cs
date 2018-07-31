using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour 
{
    private ParticleSystem PS;

	// Use this for initialization
	void Awake () 
    {
        PS = GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        if(Input.GetButtonDown("Fire1") || OVRInput.GetDown(OVRInput.Button.One))
        {
            PS.Play();
            return;
        }

        if (Input.GetButtonUp("Fire1") || OVRInput.GetUp(OVRInput.Button.One))
        {
            PS.Stop();
            return;
        }
	}
}
