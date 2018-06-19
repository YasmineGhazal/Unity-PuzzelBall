using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitScript : MonoBehaviour {

	void OnTriggerEnter(Collider target) {

		if( target.tag == "ball" ) {
			SceneManager.LoadScene("MainMenu");	
		}

	}


}
