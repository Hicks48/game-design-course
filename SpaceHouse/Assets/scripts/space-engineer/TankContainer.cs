using UnityEngine;
using System.Collections;

public class TankContainer : MonoBehaviour {
	public float max;
	public bool inUse;
	private float current;
	
	void Start () {
		this.current = this.max;
	}
	
	void Update () {
		if(this.inUse) {
			this.current = this.current - Time.deltaTime;
			
			if(this.current <= 0.0f) {
				this.current = 0.0f;
			}
		}
	}
	
	public void setInUse(bool use) {
		this.inUse = use;
	}
	
	public void fill() {
		this.current = this.max;
	}
	
	public float getCurrent() {
		return this.current;
	}
	
	public bool isEmpty() {
		return this.current <= 0.0f;
	}

	public bool isFull() {
		return this.current == this.max;
	}
}
