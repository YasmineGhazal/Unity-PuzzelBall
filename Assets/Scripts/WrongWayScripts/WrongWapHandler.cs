using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WrongWapHandler : MonoBehaviour {

	void Awake () {
		GetComponent<MeshRenderer>().enabled = false;
	}
	

	void OnTriggerEnter(Collider target) {
		if(target.tag == "ball") {
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
	}
}
