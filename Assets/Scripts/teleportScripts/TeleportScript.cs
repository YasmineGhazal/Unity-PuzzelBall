using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportScript : MonoBehaviour {

	[SerializeField]
	private Vector3 teleportPos;
	[SerializeField]
	private bool getArrows;

	private GameObject[] pathArrows;
	private GameObject[] wrongArrows;

	void Awake() {
		if(getArrows) {
			pathArrows = GameObject.FindGameObjectsWithTag("PathArrow");
			wrongArrows = GameObject.FindGameObjectsWithTag("WrongPathArrow");

			foreach (GameObject obj in wrongArrows) {
				obj.SetActive(false);
			}
		}

	}
	void OnTriggerEnter(Collider target) {
		if( target.tag == "ball" ) {
			target.transform.position = teleportPos;
		}
		if(getArrows) {
			foreach (GameObject obj in pathArrows) {
				obj.SetActive(false);
			}
			foreach (GameObject obj in wrongArrows) {
				obj.SetActive(true);
			}
		}
	}



}
