using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyBallTrigger : MonoBehaviour {

	private Rigidbody myBody;
	[SerializeField]
	private AudioSource ballRollAudio, audioSource, stunnedAudio;
	[SerializeField]
	private AudioClip wallHit, stunned;
	private Vector3 lastFrameVelocity;
	private Vector3 collisionNormal;
	private float xAxisAngle, xFactor;
	private float yAxisAngle, yFactor;
	private float zAxisAngle, zFactor;
	private EnemyScripts enemy;
	private MeshRenderer myRenderer;

	void Awake () {
		myBody = GetComponent<Rigidbody>();
		enemy = GetComponent<EnemyScripts>();
		myRenderer = GetComponent<MeshRenderer>();
	}
	
	void Update() {
		ballRollSoundController();
	}

	void LateUpdate () {
		lastFrameVelocity = myBody.velocity;
	}

	void ballRollSoundController() {
		if( myBody.velocity.sqrMagnitude > 0 ) {
				ballRollAudio.volume = myBody.velocity.sqrMagnitude * 0.0009f;
				ballRollAudio.pitch = 0.4f + ballRollAudio.volume;
				ballRollAudio.mute = false;
		} else {
			ballRollAudio.mute = true;
		}
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

	void OnCollisionEnter(Collision target) {
		if(target.gameObject.tag == "Wall") {
			setSoundVolumeOnCollision(target);
			if(!enemy.stunned && (Mathf.Abs(lastFrameVelocity.x) * xFactor +
								   Mathf.Abs(lastFrameVelocity.y) * yFactor +
								   Mathf.Abs(lastFrameVelocity.z) * zFactor ) > 15f ) {
				enemy.stunned = true;
				myRenderer.material.color = Color.yellow;
				stunnedAudio.PlayOneShot(stunned);
				StartCoroutine (ballStunned());
			}
		}
		if(target.gameObject.tag == "ball") {
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
	}

	IEnumerator ballStunned() {
		yield return new WaitForSeconds(2f);
		myRenderer.material.color = Color.gray;
		enemy.stunned = false;
	}

	void OnTriggerEnter(Collider target) {
		if(target.tag == "ball") {
			gameObject.SendMessage("getBallTarget", target.transform);
			gameObject.SendMessage("canAttackToggle", true);	
		}
	}

	void OnTriggerExit(Collider target) {
		if(target.tag == "ball") {
			gameObject.SendMessage("canAttackToggle", false);
		}
	}

}
