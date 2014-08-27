using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpaceStation : MonoBehaviour {
	public GameObject roof;
	public GameObject floor;
	public PolygonCollider2D insideCollider;

	private List<GameObject> gameobjectsInside;
	private bool forceSee;

	void Start () {
		this.gameobjectsInside = new List<GameObject>();
		this.forceSee = false;
	}
	
	void Update () {
		SpriteRenderer roofRender = (SpriteRenderer)this.roof.GetComponent<SpriteRenderer>();
		SpriteRenderer floorRender = (SpriteRenderer)this.floor.GetComponent<SpriteRenderer>();

		/* If no one inside show or forceSee on */
		if (this.gameobjectsInside.Count == 0 || this.forceSee) {
			roofRender.enabled = true;
			floorRender.enabled = false;
		}

		/* If someone inside show floor */
		else {
			roofRender.enabled = false;
			floorRender.enabled = true;
		}
	}

	void OnTriggerEnter2D(Collider2D collider) {
		if(collider.gameObject.tag.Equals("player")) {
			this.gameobjectsInside.Add (collider.gameObject);
		}
	}

	void OnTriggerExit2D(Collider2D collider) {
		if(collider.gameObject.tag.Equals("player")) {
			this.gameobjectsInside.Remove (collider.gameObject);
		}
	}

	public void setForceSee(bool forceSee) {
		this.forceSee = forceSee;
	}
}
