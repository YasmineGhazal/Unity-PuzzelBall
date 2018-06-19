using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScripts : MonoBehaviour {

	private Transform ballTarget;
	private Vector3 ballPosDirection;
	private bool canAttack, readyToAttack;

	[HideInInspector]
	public bool stunned;
	private Rigidbody myBody;
	private RaycastHit ballHit;
	void Awake () {
		myBody = GetComponent<Rigidbody>() ;
	}
	
	void Update() {
		checkIfCanAttack();
	}

	void FixedUpdate () {
		attack();
	}

	void getBallTarget(Transform target) {
		ballTarget = target;
	}

	void canAttackToggle(bool canAttak) {
		this.canAttack = canAttak;
	}

	void checkIfCanAttack() {
		if(canAttack && !stunned && (myBody.velocity.sqrMagnitude <= 0.11f)) {
			ballPosDirection = ballTarget.position-transform.position;
			if(Physics.Raycast(transform.position, ballPosDirection, out ballHit, 25)) {
				if(ballHit.transform.tag == "ball")
					readyToAttack = true;
			}
		}
	}

	void attack() {
		if(readyToAttack) {
			myBody.AddForce(ballPosDirection * 200f);
			readyToAttack = false;
		}
	}

}
