using UnityEngine;
using System.Collections;

public class BanditBullet : MonoBehaviour {

	public float damage = 1f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnRayMovementHit(RaycastHit2D hit) {
		if(hit.collider != null) {
			if(hit.transform.tag == "wall") {
				Strenght strength = hit.transform.GetComponent<Strenght>();
				strength.damage(damage);
			}
		}
	}
}
