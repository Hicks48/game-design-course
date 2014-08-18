using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpaceShipControls : MonoBehaviour {
	public KeyCode board;
	public KeyCode unboard;

	public BoxCollider2D enterArea;
	public int seats;

	private List<GameObject> pasengers;

	void Start () {
		this.pasengers = new List<GameObject>();
	}
	
	void Update () {
		
	}

	void OnTriggerStay2D(Collider2D collider) {
		if (Input.GetKey (this.board)) {
			this.attempToBoard(collider.gameObject);
		}
	}

	private void attempToBoard(GameObject go) {
		if (this.pasengers.Count < seats) {
			this.pasengers.Add(go);
			go.SetActive(false);
		}
	}

	private void disembark(GameObject go) {
		if(Input.GetKey(this.unboard) && this.pasengers.Count > 0) {
			go.SetActive(true);
			this.pasengers.RemoveAt(0);
		}
	}
}
