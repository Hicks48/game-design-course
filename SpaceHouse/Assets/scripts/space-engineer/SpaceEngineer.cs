using UnityEngine;
using System.Collections;

public class SpaceEngineer : MonoBehaviour {

	public Camera mainCamera;
	public MovementMath movementMath;

	public float trustFowardPowerMultiplier;
	public float trustBackwardPowerMultiplier;

	public float rotationSpeed;
	public float walkingSpeed;

	public KeyCode foward;
	public KeyCode back;

	public TankContainer fuelTank;
	public TankContainer airTank;
	public RepairTool repairTool;

	private bool alive;
	private bool outside;

	void Start () {
		this.alive = true;
		this.outside = true;
	}
	
	void Update () {

		/* Check if alive */
		if (!this.alive) {
			return;
		}

		/* Movement */
		if (this.outside) {
			doMovementOutside ();
		} 

		else {
			doMovementInside();
		}

		/* Air */
		if (this.airTank.isEmpty ()) {
			this.alive = false;
		}
	}

	void OnGUI() {
		GUI.Box (new Rect(Screen.width - 225, Screen.height - 75, 225, 75), "Life Support");
		GUI.Label (new Rect (Screen.width - 200, Screen.height - 50, 100, 50), "Fuel level:\n" + this.fuelTank.getCurrent());
		GUI.Label (new Rect (Screen.width - 100, Screen.height - 50, 100, 50), "Air level:\n" + this.airTank.getCurrent());

		/* Check if palayer is alive */
		if (!this.alive) {
			GUI.Label (new Rect (Screen.width / 2.0f, Screen.height / 2.0f, 100, 50), "Game over\nYou ran out of oxygen");
		}
	}

	public void setOutside(bool isOutside) {
		this.outside = isOutside;
	}

	public void kill() {
		this.alive = false;
	}

	/* Privates */

	private void doMovementInside() {
		/* Reset velocity */
		rigidbody2D.velocity = Vector2.zero;
		rigidbody2D.angularVelocity = 0.0f;

		/* Heading = mouse normalized vector to position */
		Vector2 towardsHeading = movementMath.getToWardsHeading (movementMath.getHeadingVector (this.mainCamera));
		Vector2 foward = movementMath.getFoward ();

		/* Turn towards heading */
		movementMath.turnTowardsHeading (towardsHeading, foward, this.rotationSpeed);

		if (Input.GetKey(this.foward)) {
			rigidbody2D.AddForce(foward * this.walkingSpeed);
		}
		
		if (Input.GetKey (this.back)) {
			rigidbody2D.AddForce((-1) * foward * this.walkingSpeed);
		}
	}

	private void doMovementOutside() {
		if(this.fuelTank.isEmpty()) {
			return;
		}

		bool moved = false;

		/* Heading = mouse normalized vector to position */
		Vector2 towardsHeading = movementMath.getToWardsHeading (movementMath.getHeadingVector (this.mainCamera));
		Vector2 foward = movementMath.getFoward ();

		/* Turn towards heading */
		movementMath.turnTowardsHeading (towardsHeading, foward, this.rotationSpeed);
		
		if (Input.GetKey(this.foward)) {
			rigidbody2D.AddForce(foward * trustFowardPowerMultiplier);
			moved = true;
		}
		
		if (Input.GetKey (this.back)) {
			rigidbody2D.AddForce((-1) * towardsHeading * trustBackwardPowerMultiplier);
			moved = true;
		}

		/* Fuel */
		if (moved) {
			this.fuelTank.setInUse(true);
		}

		else {
			this.fuelTank.setInUse(false);
		}
	}
}
