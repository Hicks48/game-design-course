using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpaceStation : MonoBehaviour {
	public GameObject roof;
	public GameObject floor;
	public BoxCollider2D insideCollider;

	private List<GameObject> gameobjectsInside;
	private bool forceSee;

	void Start () {
		this.gameobjectsInside = new List<GameObject>();
		this.forceSee = false;
	}
	
	void Update () {
		/* If no one inside show or forceSee on */
		if (this.gameobjectsInside.Count == 0 || this.forceSee) {
			this.roof.SetActive (true);
			this.floor.SetActive (false);
		}

		/* If someone inside show floor */
		else {
			this.roof.SetActive (false);
			this.floor.SetActive (true);
		}
	}

	void OnTriggerEnter2D(Collider2D collider) {
		if(this.objectOfInHierarchy(collider.gameObject)) {
			return;
		}

		this.gameobjectsInside.Add (collider.gameObject);
	}

	void OnTriggerExit2D(Collider2D collider) {
		if(this.objectOfInHierarchy(collider.gameObject)) {
			return;
		}

		this.gameobjectsInside.Remove (collider.gameObject);
	}

	public void setForceSee(bool forceSee) {
		this.forceSee = forceSee;
	}

	private bool objectOfInHierarchy(GameObject go) {
		Component[] hierarchyComponents = this.gameObject.GetComponentsInChildren<Transform> ();

		for(int i = 0;i < hierarchyComponents.Length;i ++) {
			if(hierarchyComponents[i].gameObject.Equals(go)) {
				return true;
			}
		}

		return false;
	}
}
