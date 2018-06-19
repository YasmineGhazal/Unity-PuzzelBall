using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResizeBallScript : MonoBehaviour {

	private Vector3 smallBallScale = new Vector3(1.5f, 1.5f, 1.5f);
	private Vector3 largeBallScale = new Vector3(8f, 8f, 8f);
	private Vector3 mediumBallScale = new Vector3(3f, 3f, 3f);
	private float smallBallMass = 0.7f;
	private float mediumBallMass = 1f;
	private float largeBallMass = 1.2f;
	private bool ballResized;
	private bool removeResizer;
	private bool resizerCollided;
	private string SmallBall = "SmallBall";
	private string MediumBall = "MediumBall";
	private string LargeBall = "LargeBall";
	void Awake () {
		if( gameObject.name == SmallBall || gameObject.name == MediumBall || gameObject.name == LargeBall ) {
			removeResizer = false;
		} else {
			removeResizer = true;
		}
	}
	
	void OnTriggerEnter (Collider target) {
		if( target.gameObject.tag == "ball" ) {
			if( gameObject.tag == SmallBall ) {
				if ( target.gameObject.transform.localScale != smallBallScale ) {
					target.gameObject.transform.localScale = smallBallScale;
					target.gameObject.GetComponent<Rigidbody> ().mass = smallBallMass;
					ballResized = true;
				}
			} else if ( gameObject.tag == MediumBall ) {
				if ( target.gameObject.transform.localScale != mediumBallScale ) {
					target.gameObject.transform.localScale = mediumBallScale;
					target.gameObject.GetComponent<Rigidbody> ().mass = mediumBallMass;
					ballResized = true;
				} 
			} else if ( gameObject.tag == LargeBall ) {
				if ( target.gameObject.transform.localScale != largeBallScale ) {
					target.gameObject.transform.localScale = largeBallScale;
					target.gameObject.GetComponent<Rigidbody> ().mass = largeBallMass;
					ballResized = true;
				}

			}
		}

		if ( ballResized ) {
			if(removeResizer)
				resizerCollided = true;
			ballResized = false;
			target.gameObject.GetComponent<BallSound>().playPickUpsound();
		}

		if ( resizerCollided ) {
			gameObject.SetActive(false);
		}
	}
}
