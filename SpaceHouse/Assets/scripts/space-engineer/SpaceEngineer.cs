using UnityEngine;
using System.Collections;

public class SpaceEngineer : MonoBehaviour {

	/* Movement variables */
	public Camera mainCamera;
	public float trustPowerMultiplier;
	public KeyCode thrust;

	/* Initial value setters for variables */
	public float initFuel;
	public float initAir;

	/* Space Engineer variables */
	private float fuel;
	private float air;
	private bool alive;

	void Start () {
		this.fuel = initFuel;
		this.air = initAir;
		this.alive = true;
	}
	
	void Update () {

		/* Check if alive */
		if (!this.alive)
		{
			return;
		}

		/* Heading = mouse normalized vector to position */
		Vector2 heading = getHeadingVector ();

		/* Turn towards heading */
		if(this.alive)
		{

		}

		/* Thrust */
		if(Input.GetKey(this.thrust) && this.fuel > 0.0f)
		{
			Vector2 foward = getFoward (heading);

			this.fuel = this.fuel - Time.deltaTime;

			if(this.fuel < 0.0f)
			{
				this.fuel = 0.0f;
			}

			rigidbody2D.AddForce(foward * trustPowerMultiplier);
		}

		/* Reduce air */
		if (this.air > 0.0f)
		{
			this.air = this.air - Time.deltaTime;
		}

		else 
		{
			this.air = 0.0f;
			this.alive = false;
		}
	}

	void OnGUI()
	{
		GUI.Box (new Rect(Screen.width - 225, Screen.height - 75, 225, 75), "Life Support");
		GUI.Label (new Rect (Screen.width - 200, Screen.height - 50, 100, 50), "Fuel level:\n" + this.fuel);
		GUI.Label (new Rect (Screen.width - 100, Screen.height - 50, 100, 50), "Air level:\n" + this.air);

		/* Check if palayer is alive */
		if (!this.alive)
		{
			GUI.Label (new Rect (Screen.width / 2.0f, Screen.height / 2.0f, 100, 50), "Game over\nYou ran out of oxygen");
		}
	}

	private Vector2 getFoward(Vector2 heading)
	{
		/* Normalized vector that points from engineers position to given heading */
		Vector2 foward = new Vector2 (heading.x - transform.position.x, heading.y - transform.position.y);
		return new Vector2 ((1.0f/foward.magnitude) * foward.x, (1.0f/foward.magnitude) * foward.y);
	}

	private Vector2 getHeadingVector()
	{
		Vector3 mousePosition3D = mainCamera.ScreenToWorldPoint (Input.mousePosition);
		return new Vector2 (mousePosition3D.x, mousePosition3D.y);
	}
}
