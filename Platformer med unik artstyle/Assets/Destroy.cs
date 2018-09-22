using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour {

    private float duration;

	// Use this for initialization
	void Start () {

        //finder particleSystemet, lav det om til en variabel, lav duration om til particlesystemet
        ParticleSystem ps = GetComponent<ParticleSystem>();
        var main = ps.main;
        duration = main.duration;

        //destroy after time
        Destroy(gameObject, duration);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
