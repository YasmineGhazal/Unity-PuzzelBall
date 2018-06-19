using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostScript : MonoBehaviour {

	public float force = 175f;
	void OnTriggerEnter(Collider target) {
		if(target.tag == "ball") {
			target.gameObject.GetComponent<Rigidbody>().AddForce (transform.forward * -force, ForceMode.Impulse);
		}
	} 
	void Start () {
		
	}
	
	void Update () {
		
	}
}
