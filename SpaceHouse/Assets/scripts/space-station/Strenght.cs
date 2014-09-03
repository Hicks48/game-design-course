using UnityEngine;
using System.Collections;

public class Strenght : MonoBehaviour {
	public float maxStrenght;
	public float strenght;

	void Start() {
		this.strenght = this.maxStrenght;
	}

	void Update() {
		this.gameObject.renderer.material.color = new Color(1.0f, (strenght / maxStrenght), (strenght / maxStrenght));
	}

	public void repair(GameObject engineer) {
		SpaceEngineer se = engineer.GetComponent<SpaceEngineer> ();
		float repair = se.repairTool.repaitSpeed;

		strenght = strenght + Time.deltaTime * repair;

		if(strenght > maxStrenght) {
			strenght = maxStrenght;
		}
	}

	public void damage(float ammount) {
		this.strenght = this.strenght - ammount;

		if (this.strenght < 0.0f) {
			this.strenght = 0.0f;
		}
	}

	public float getStrenght() {
		return this.strenght;
	}


}
