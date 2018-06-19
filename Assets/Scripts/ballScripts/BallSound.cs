using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSound : MonoBehaviour {

	[SerializeField]
	private AudioSource audioSource;
	[SerializeField]
	private AudioSource ballRoll;
	[SerializeField]
	private AudioClip pickUp, wallHit;
	private Rigidbody myBody;
	private BallMovement ballMovement;
	private Vector3 lastFrameVelocity;
	private Vector3 collisionNormal;
	private float xAxisAngle, xFactor;
	private float yAxisAngle, yFactor;
	private float zAxisAngle, zFactor;
	
	void Awake () {
		myBody = GetComponent<Rigidbody>();
		ballMovement = GetComponent<BallMovement>();
	}
	
	void Update () {
		ballRollSoundController();
	}

	void LateUpdate() {
		lastFrameVelocity = myBody.velocity;
	}

	void ballRollSoundController() {
		if( ballMovement.onFloorTracker > 0 && myBody.velocity.sqrMagnitude > 0 ) {
				ballRoll.volume = myBody.velocity.sqrMagnitude * 0.0009f;
				ballRoll.pitch = 0.4f + ballRoll.volume;
				ballRoll.mute = false;
		} else {
			ballRoll.mute = true;
		}
	}

	public void playPickUpsound() {
		audioSource.volume = 0.7f;
		audioSource.PlayOneShot(pickUp);
	}

	void setSoundVolumeOnCollision(Collision collision) {
		collisionNormal = collision.contacts[0].normal;
		xAxisAngle = Vector3.Angle(Vector3.right, collisionNormal);
		xFactor = (1.0f / 8100f) * xAxisAngle * xAxisAngle + (-1/45) + 1f;
		yAxisAngle = Vector3.Angle(Vector3.up, collisionNormal);
		yFactor = (1.0f / 8100f) * yAxisAngle * yAxisAngle + (-1/45) + 1f;
		zAxisAngle = Vector3.Angle(Vector3.forward, collisionNormal);
		zFactor = (1.0f / 8100f) * zAxisAngle * zAxisAngle + (-1/45) + 1f;
		audioSource.volume = (Mathf.Abs(lastFrameVelocity.x) * xFactor * 0.001f) +
							 (Mathf.Abs(lastFrameVelocity.y) * yFactor * 0.001f) +
							 (Mathf.Abs(lastFrameVelocity.z) * zFactor * 0.001f) ;
	}
	public void OnCollisionEnter(Collision target) {
		if( target.gameObject.tag == "Wall" ){
			setSoundVolumeOnCollision(target);
			audioSource.PlayOneShot(wallHit);
		}
	}
}
