using UnityEngine;
using System.Collections;

public class Wall : MonoBehaviour {
	public float strenght;
	public bool broken;
	public KeyCode repair;

	void Start () {
		broken = false;
	}
	
	void Update () {
		if(this.strenght <= 25.0f) {
			/* Show flashing light */
		}

		if (this.strenght <= 0.0f) {
			this.strenght = 0.0f;
			this.broken = true;
		}
	}

	void OnCollision2D(Collider2D collider) {
		/* brake */
		if (collider.gameObject.tag.Equals ("meteorite")) {
			/* Minus from meteorite strenght */
			this.strenght = this.strenght - 10.0f;
		}

		/* repair */
		else if (collider.gameObject.tag.Equals ("player") && Input.GetKey(repair)) {
			this.strenght = this.strenght + 10.0f * Time.deltaTime;
		}
	}
}
