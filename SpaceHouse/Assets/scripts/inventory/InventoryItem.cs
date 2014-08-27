using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InventoryItem : MonoBehaviour {
	public string itemName;
	public string id;
	private List<Stat> stats;

	void Start () {
		this.stats = new List<Stat> ();
		intiStats ();
	}
	
	void Update () {
		
	}

	public List<Stat> getStats() {
		return this.stats;
	}

	private void intiStats() {
		Component[] components = this.GetComponents<Stat>();
		for (int i = 0; i < components.GetLength(0); i ++) {
			this.stats.Add((Stat)components.GetValue((long)i));
		}
	}
}
