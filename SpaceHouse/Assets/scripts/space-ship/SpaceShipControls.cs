using UnityEngine;
using System.Collections;

public class SpaceShipControls : MonoBehaviour {
	public KeyCode board;
	public KeyCode unboard;
	public CircleCollider2D enterArea;

	private GameObject pasenger;

	void Start () {
		this.pasenger = null;
	}
	
	void Update () {
		if(this.pasenger != null && Input.GetKey(this.unboard)) {
			this.disembark();
		}
	}

	void OnTriggerStay2D(Collider2D collider) {
		if (Input.GetKey (this.board)) {
			this.attempToBoard(collider.gameObject);
		}
	}

	private void attempToBoard(GameObject go) {
		if (this.pasenger == null && go.name.Equals ("space-engineer")) {
			this.pasenger = go;
			this.pasenger.SetActive(false);
		}
	}

	private void disembark() {
			this.pasenger.transform.position = this.getDisembarkLocation();
			this.pasenger.SetActive(true);
			this.pasenger = null;
	}

	/* Needs improvement */
	private Vector2 getDisembarkLocation() {
		return new Vector2 (this.transform.position.x + this.enterArea.radius, this.transform.position.y + this.enterArea.radius);
	}
}
