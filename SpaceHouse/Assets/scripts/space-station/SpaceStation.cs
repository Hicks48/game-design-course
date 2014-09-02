using UnityEngine;
using System.Collections;

public class SpaceStation : MonoBehaviour {
	public GameObject roof;
	public GameObject floor;
	private bool forceSee;

	void Start () {
		this.forceSee = false;
	}
	
	void Update () {
		SpriteRenderer roofRender = (SpriteRenderer)this.roof.GetComponent<SpriteRenderer>();
		SpriteRenderer floorRender = (SpriteRenderer)this.floor.GetComponent<SpriteRenderer>();

		/* If no one inside show or forceSee on */
		if (!this.floor.GetComponent<Floor>().isEngineerInside() || this.forceSee) {
			roofRender.enabled = true;
			floorRender.enabled = false;
		}

		/* If someone inside show floor */
		else {
			roofRender.enabled = false;
			floorRender.enabled = true;
		}
	}

	public void setForceSee(bool forceSee) {
		this.forceSee = forceSee;
	}
}
