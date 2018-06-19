using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	private Transform ballTransform;
	public float distance = 50f ;
	public float xSpeed = 250f;
	public float ySpeed = 120f;
	public float yMinLimit = 0f;
	public float yMaxLimit = 80f;
	private Quaternion rotation;
	private Vector3 position;
	private float xAngle, yAngle; 
	private float angleMultiplier = 0.02f;
	private bool snapCameraPos ;
	void Awake () {
		ballTransform = GameObject.Find("ball").transform;
	}

	void Start() {
		Vector3 root = transform.eulerAngles;
		xAngle = root.y;
		yAngle = root.x;
		
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
		}
	
	void Update() {
		if(Input.GetMouseButtonDown(0)) {
			snapCameraPos = true;
		}
	}

	void LateUpdate () {
		if(ballTransform) {
			xAngle += Input.GetAxis("Mouse X") * xSpeed * angleMultiplier;
			yAngle += Input.GetAxis("Mouse Y") * ySpeed * angleMultiplier;

			yAngle = clampAngle(yAngle, yMinLimit, yMaxLimit);

			if(snapCameraPos) {
				if(( transform.eulerAngles.y <= 225 ) && ( transform.eulerAngles.y > 135f )) {
					xAngle = 180f;
				} else if (( transform.eulerAngles.y <= 135 ) && ( transform.eulerAngles.y > 45f )){
					xAngle = 90f;
				} else if (( transform.eulerAngles.y <= 315 ) && ( transform.eulerAngles.y > 225 )){
					xAngle = 270f;
				} else 
					xAngle = 0f;
				snapCameraPos = false;
			}

			rotation = Quaternion.Euler(yAngle, xAngle, 0);
			position = rotation * new Vector3(0, 0, -distance) + ballTransform.position;
			transform.rotation = rotation;
			transform.position = position;
		}
	}

	float clampAngle(float angle, float min, float max) {
		if(angle < -360) 
			angle += 360;
		if(angle > 360)
			angle -= 360;
		return Mathf.Clamp(angle, min, max);	
		
	}
}
