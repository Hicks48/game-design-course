using UnityEngine;
using System.Collections;

public class Meteorite : MonoBehaviour {

	public float damage = 5f;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

	}

	public void OnRayMovementHit(RaycastHit2D hit) {
		if(hit.collider != null && hit.transform.tag == "wall") {
			Strenght strength = hit.transform.GetComponent<Strenght>();
			strength.damage(damage);
		}
	}
}
