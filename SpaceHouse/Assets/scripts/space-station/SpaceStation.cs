using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpaceStation : MonoBehaviour {
	public GameObject roof;
	public GameObject floor;
	public BoxCollider2D insideCollider;

	private List<GameObject> gameobjectsInside;

	void Start () {
		this.gameobjectsInside = new List<GameObject>();
	}
	
	void Update () {
		/* If no one inside show */
		if (this.gameobjectsInside.Count == 0) {
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
		Debug.Log("here :D");
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

	/* Needs to optimazation */
	private bool objectOfInHierarchy(GameObject go) {
		Component[] hierarchyComponents = this.gameObject.GetComponentsInChildren<Transform> ();

		for(int i = 0;i < hierarchyComponents.Length;i ++) {
			if(hierarchyComponents[i].gameObject.Equals(go)) {
				return true;
			}
		}

		return false;
	}

	private string toString() {
		string s = "";
		for (int i = 0; i < this.gameobjectsInside.Count; i ++) {
			s = " " + this.gameobjectsInside[i];
		}

		return s;
	}
}
