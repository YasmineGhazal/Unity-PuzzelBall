using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class BallMovement : MonoBehaviour {

	private string direction = "";
	private string directionLastFrame = "";

	[HideInInspector]
	public int onFloorTracker = 0;
	private bool fullSpeed ;

	//speed variables
	private int floorSpeed = 100;
	private int airSpeed = 20;
	private float airSpeedDiagonal = 5.858f;
	private float airDrag = 0.1f;
	private float floorDrag = 2.29f;
	private float delta = 50f;

	//camera variables
	private Vector3 cameraRelativeRight;
	private Vector3 cameraRelativeUp;
	private Vector3 cameraRelativeDown;
	private Vector3 cameraRelativeUpLeft;
	private Vector3 cameraRelativeUpRight;

	//velocity and magnitude variables
	private Vector3 xVelocity;
	private Vector3 zVelocity;
    private float xSpeed;
	private float zSpeed;
	
	//movements
	private string axisY = "Vertical";
	private string axisX = "Horizontal";
	private Rigidbody myBody;
	private Camera mainCamera;
	
	void Awake() {
		myBody = GetComponent<Rigidbody>();
		mainCamera = Camera.main;
	}

	void Update () {
		getDirection();
		fullSpeedController();
		updateCameraRelativePosition();
		dragAdjustmentAndAirSpeed();
		ballFell();
	}

	void FixedUpdate() {
		moveTheBall();
	}

	void LateUpdate() {
		directionLastFrame = direction;
	}
	void getDirection() {
		direction = "";

		if(Input.GetAxis(axisY) > 0 ){
			direction += "up";
		} else if(Input.GetAxis(axisY) < 0 ){
			direction += "down";
		} 
		if(Input.GetAxis(axisX) > 0 ){
			direction += "right";
		} else if(Input.GetAxis(axisX) < 0 ){
			direction += "left";
		} 
	}
	void moveTheBall() {

		switch (direction) {

			case "upright" :
				if ( onFloorTracker > 0 ) {
					if(fullSpeed) {
						myBody.AddForce(floorSpeed * cameraRelativeUpRight * Time.fixedDeltaTime * delta);
					} else {
						myBody.AddForce((floorSpeed - 75f) * cameraRelativeUpRight * Time.fixedDeltaTime * delta);
					}
				} else if( onFloorTracker == 0 ){
					if( zVelocity.normalized == cameraRelativeUp ) {
						if( zSpeed < (airSpeed - airSpeedDiagonal - 0.1f) ) {
							myBody.AddForce(10.6f * cameraRelativeUp * Time.fixedDeltaTime * delta);
						}
					} else {
						myBody.AddForce(10.6f * cameraRelativeUp * Time.fixedDeltaTime * delta);
					}
					if( xVelocity.normalized == cameraRelativeRight ) {
						if( xSpeed < (airSpeed - airSpeedDiagonal - 0.1f ) ) {
							myBody.AddForce(10.6f * cameraRelativeRight * Time.fixedDeltaTime * delta);
						}
					} else {
							myBody.AddForce(10.6f * cameraRelativeUp * Time.fixedDeltaTime * delta);
					    }
				}
				break;
			case "upleft" :
				if( onFloorTracker > 0 ) {
					if( fullSpeed ) {
						myBody.AddForce(floorSpeed * cameraRelativeUpLeft * Time.fixedDeltaTime * delta);
					} else {
						myBody.AddForce((floorSpeed - 75f) * cameraRelativeUpLeft * Time.fixedDeltaTime* delta);
					}
				} else if( onFloorTracker == 0 ) {
					if( zVelocity.normalized == cameraRelativeUp ) {
						if( zSpeed < (airSpeed - airSpeedDiagonal - 0.1f) ){
							myBody.AddForce(10.6f * cameraRelativeUp * Time.fixedDeltaTime * delta);
						}
					} else {
						myBody.AddForce(10.6f * cameraRelativeUp * Time.fixedDeltaTime * delta);
					}
					if( xVelocity.normalized == -cameraRelativeRight ) {
						if( xSpeed < (airSpeed - airSpeedDiagonal - 0.1f) ){
							myBody.AddForce(10.6f * -cameraRelativeRight * Time.fixedDeltaTime * delta);
						} 
					} else {
						myBody.AddForce(10.6f * -cameraRelativeRight * Time.fixedDeltaTime * delta);
					}
				}

				break;		
			case "downright" :
				if( onFloorTracker > 0 ) {
						if( fullSpeed ) {
							myBody.AddForce(floorSpeed * -cameraRelativeUpLeft * Time.fixedDeltaTime * delta);
						} else {
							myBody.AddForce((floorSpeed - 75f) * -cameraRelativeUpLeft * Time.fixedDeltaTime* delta);
						}
					} else if( onFloorTracker == 0 ) {
						if( zVelocity.normalized == -cameraRelativeUp ) {
							if( zSpeed < (airSpeed - airSpeedDiagonal - 0.1f) ){
								myBody.AddForce(10.6f * -cameraRelativeUp * Time.fixedDeltaTime * delta);
							}
						} else {
							myBody.AddForce(10.6f * -cameraRelativeUp * Time.fixedDeltaTime * delta);
						}
						if( xVelocity.normalized == cameraRelativeRight ) {
							if( xSpeed < (airSpeed - airSpeedDiagonal - 0.1f) ){
								myBody.AddForce(10.6f * cameraRelativeRight * Time.fixedDeltaTime * delta);
							} 
						} else {
							myBody.AddForce(10.6f * cameraRelativeRight * Time.fixedDeltaTime * delta);
						}
					}
					break;
		    case "downleft" :
				if( onFloorTracker > 0 ) {
						if( fullSpeed ) {
							myBody.AddForce(floorSpeed * -cameraRelativeUpLeft * Time.fixedDeltaTime * delta);
						} else {
							myBody.AddForce((floorSpeed - 75f) * -cameraRelativeUpLeft * Time.fixedDeltaTime* delta);
						}
					} else if( onFloorTracker == 0 ) {
						if( zVelocity.normalized == -cameraRelativeUp ) {
							if( zSpeed < (airSpeed - airSpeedDiagonal - 0.1f) ){
								myBody.AddForce(10.6f * -cameraRelativeUp * Time.fixedDeltaTime * delta);
							}
						} else {
							myBody.AddForce(10.6f * -cameraRelativeUp * Time.fixedDeltaTime * delta);
						}
						if( xVelocity.normalized == -cameraRelativeRight ) {
							if( xSpeed < (airSpeed - airSpeedDiagonal - 0.1f) ){
								myBody.AddForce(10.6f * -cameraRelativeRight * Time.fixedDeltaTime * delta);
							} 
						} else {
							myBody.AddForce(10.6f * -cameraRelativeRight * Time.fixedDeltaTime * delta);
						}
					}		
				break;
			case "up" :
					if( onFloorTracker > 0 ) {
						if( fullSpeed ) {
							myBody.AddForce(floorSpeed * cameraRelativeUp * Time.fixedDeltaTime * delta);
						} else {
							myBody.AddForce((floorSpeed - 75f) * cameraRelativeUp * Time.fixedDeltaTime* delta);
						}
					} else if( onFloorTracker == 0 ) { 
						if( zSpeed < airSpeed ) {
							myBody.AddForce( airSpeed * 0.75f * cameraRelativeUp * Time.fixedDeltaTime * delta);
						}
						if( xSpeed  > 0.1f) {
							if( xVelocity.normalized == cameraRelativeRight ){
								myBody.AddForce( airSpeed * 0.75f * -cameraRelativeRight * Time.fixedDeltaTime * delta);
							} else if (xVelocity.normalized == -cameraRelativeRight) { 
								myBody.AddForce( airSpeed * 0.75f * cameraRelativeRight * Time.fixedDeltaTime * delta);
							}
						}
					}
				break;
			case "down" :
					if( onFloorTracker > 0 ) {
						if( fullSpeed ) {
							myBody.AddForce(floorSpeed * -cameraRelativeUp * Time.fixedDeltaTime * delta);
						} else {
							myBody.AddForce((floorSpeed - 75f) * -cameraRelativeUp * Time.fixedDeltaTime* delta);
						}
					} else if( onFloorTracker == 0 ) { 
						if( zSpeed < airSpeed ) {
							myBody.AddForce( airSpeed * 0.75f * -cameraRelativeUp * Time.fixedDeltaTime * delta);
						}
						if( xSpeed  > 0.1f) {
							if( xVelocity.normalized == cameraRelativeRight ){
								myBody.AddForce( airSpeed * 0.75f * -cameraRelativeRight * Time.fixedDeltaTime * delta);
							} else if (xVelocity.normalized == -cameraRelativeRight) { 
								myBody.AddForce( airSpeed * 0.75f * cameraRelativeRight * Time.fixedDeltaTime * delta);
							}
						}
					}			
				break;	
			case "right" :
					if( onFloorTracker > 0 ) {
						if( fullSpeed ) {
							myBody.AddForce(floorSpeed * cameraRelativeRight * Time.fixedDeltaTime * delta);
						} else {
							myBody.AddForce((floorSpeed - 75f) * cameraRelativeRight * Time.fixedDeltaTime* delta);
						}
					} else if( onFloorTracker == 0 ) { 
						if( zSpeed < airSpeed ) {
							myBody.AddForce( airSpeed * 0.75f * cameraRelativeRight * Time.fixedDeltaTime * delta);
						}
						if( zSpeed  > 0.1f) {
							if( xVelocity.normalized == cameraRelativeUp ){
								myBody.AddForce( airSpeed * 0.75f * -cameraRelativeUp * Time.fixedDeltaTime * delta);
							} else if (xVelocity.normalized == -cameraRelativeUp) { 
								myBody.AddForce( airSpeed * 0.75f * cameraRelativeUp * Time.fixedDeltaTime * delta);
							}
						}
					}
				break;
			case "left" :
					if( onFloorTracker > 0 ) {
						if( fullSpeed ) {
							myBody.AddForce(floorSpeed * -cameraRelativeRight * Time.fixedDeltaTime * delta);
						} else {
							myBody.AddForce((floorSpeed - 75f) * -cameraRelativeRight * Time.fixedDeltaTime* delta);
						}
					} else if( onFloorTracker == 0 ) { 
						if( zSpeed < airSpeed ) {
							myBody.AddForce( airSpeed * 0.75f * -cameraRelativeRight * Time.fixedDeltaTime * delta);
						}
						if( zSpeed  > 0.1f) {
							if( xVelocity.normalized == cameraRelativeUp ){
								myBody.AddForce( airSpeed * 0.75f * -cameraRelativeUp * Time.fixedDeltaTime * delta);
							} else if (xVelocity.normalized == -cameraRelativeUp) { 
								myBody.AddForce( airSpeed * 0.75f * cameraRelativeUp * Time.fixedDeltaTime * delta);
							}
						}
					}
				break;
				
		}
	
	}

	void updateCameraRelativePosition() {
		cameraRelativeRight = mainCamera.transform.TransformDirection(Vector3.right);
		cameraRelativeUp = mainCamera.transform.TransformDirection(Vector3.forward);
		cameraRelativeUp.y = 0f;
		cameraRelativeUp = cameraRelativeUp.normalized;
		cameraRelativeUpRight = cameraRelativeUp + cameraRelativeRight;
		cameraRelativeUpRight = cameraRelativeUpRight.normalized;
		cameraRelativeUpLeft = cameraRelativeUpLeft + cameraRelativeUp;
		cameraRelativeUpLeft = cameraRelativeUpLeft.normalized;
	}

	void fullSpeedController() {
		if(directionLastFrame != direction) {
			if(direction == "") {
				StopCoroutine("fullSpeedTimer");
				fullSpeed = false;
			} else if (directionLastFrame == ""){
				StartCoroutine("fullSpeedTimer");
			}
		}	
	}

	void dragAdjustmentAndAirSpeed() {
		if(onFloorTracker > 0) {
			myBody.drag = floorDrag;
		} else {
			xVelocity = Vector3.Project(myBody.velocity, cameraRelativeRight);
			zVelocity = Vector3.Project(myBody.velocity, cameraRelativeUp);

			xSpeed = xVelocity.magnitude;
			zSpeed = zVelocity.magnitude;

			myBody.drag = airDrag;
		}
	}

	IEnumerator fullSpeedTimer() {
		yield return new WaitForSeconds(0.07f);
		fullSpeed = true;
	}

	void OnCollisionEnter(Collision target){
		if(target.gameObject.tag == "Floor") {
			onFloorTracker++;
		}
	}

	void OnCollisionExit(Collision target){
		if(target.gameObject.tag == "floor") {
			onFloorTracker--;
		}
	}

	void ballFell() {
		if(transform.position.y < -30f) {
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}	
	}

}
